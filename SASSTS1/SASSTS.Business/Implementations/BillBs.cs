using Infrastructure.ApiResponses;
using Infrastructure.Mail.Interfaces;
using Infrastructure.Models;
using Microsoft.AspNetCore.Http;
using SASSTS.Business.CustomExceptions;
using SASSTS.Business.Interfaces;
using SASSTS.Business.MessageServices.Interfaces;
using SASSTS.DataAccess.UnitOfWork;
using SASSTS.Model.Dtos.Bill;
using SASSTS.Model.Dtos.BillAllModel;
using SASSTS.Model.Entities;

namespace SASSTS.Business.Implementations
{
    public class BillBs : BusinessMap, IBillBs
    {
        private readonly IUnitWork _unitWork;
        private readonly IMailService _mailService;
        private readonly IMessageService _messageServices;
        public BillBs(IUnitWork unitWork, IMailService mailService, IMessageService messageService) { _unitWork = unitWork; _mailService = mailService; _messageServices = messageService; }
        public async Task<ApiResponse<NoData>> CreateAsync(BillModelItem billModelItem)
        {
            try
            {
                var bill = await _unitWork.GetRepository<Bill>().InsertAsync(new Bill
                {
                    RequestId = billModelItem.RequestId,
                    SupplierId = billModelItem.SupplierId,
                    BillType = billModelItem.BillType,
                    BillDate = billModelItem.BillDate,
                    BillNumber = billModelItem.BillNumber,
                    Discount = billModelItem.Discount,
                    ProductAmount = billModelItem.ProductAmount,
                    TotalKDV = billModelItem.TotalKDV,
                    BillTotalPrice = billModelItem.BillTotalPrice,
                    Currency = billModelItem.Currency,
                    CompanyId = billModelItem.CompanyId
                });

                foreach (var detail in billModelItem.BillDetailItem)
                {
                    await _unitWork.GetRepository<BillDetail>().InsertAsync(new BillDetail
                    {
                        BillId = (int)bill.Id,
                        ProductId = detail.ProductId,
                        ProductKdv = detail.ProductKdv,
                        UnitPrice = detail.UnitPrice,
                        ProductQuantity = detail.ProductQuantity
                    });


                    Stock s = await _unitWork.GetRepository<Stock>().GetAsync(x => x.ProductId == detail.ProductId && x.CompanyId == billModelItem.CompanyId);

                    if (s != null)
                    {
                        var quantity = s.Quantity;
                        s.Quantity = quantity + detail.ProductQuantity;
                        await _unitWork.GetRepository<Stock>().UpdateAsync(s);
                    }

                    else
                    {
                        s = await _unitWork.GetRepository<Stock>().InsertAsync(new Stock
                        {
                            ProductId = detail.ProductId,
                            CreateTime = DateTime.Now,
                            Quantity = detail.ProductQuantity,
                            CompanyId = (byte)billModelItem.CompanyId
                        });
                    }

                    await _unitWork.GetRepository<StockOperation>().InsertAsync(new StockOperation
                    {
                        Quantity = detail.ProductQuantity,
                        Status = true,
                        OperationTime = DateTime.Now,
                        EmployeeId = billModelItem.EmployeeId,
                        StockId = (int)s.Id
                    });
                }

                //Offer iş bitti 5 e çek.  Offer çekili ondan atamalar yapılacak....
                var offer = await _unitWork.GetRepository<Offer>().GetAsync(x => x.RequestId == billModelItem.RequestId && x.SupplierId == billModelItem.SupplierId);
                offer.OfferStatus = 5;
                await _unitWork.GetRepository<Offer>().UpdateAsync(offer);

                //Request iş bitti 5 e çek.  Requestte çekili
                var request = await _unitWork.GetRepository<Request>().GetAsync(x => x.Id == billModelItem.RequestId);
                request.RequestStatus = 5;
                await _unitWork.GetRepository<Request>().UpdateAsync(request);


                var basket = await _unitWork.GetRepository<Basket>().GetAsync(x => x.Id == request.BasketId);
                var basketDetail = await _unitWork.GetRepository<BasketDetail>().GetAllAsync(x => x.BasketId == basket.Id);

                foreach (var detail in basketDetail)
                {
                    var supplier = await _unitWork.GetRepository<Supplier>().GetAsync(x => x.Id == offer.SupplierId);  //Supplier için atama yapılacakkk!
                    var employee = await _unitWork.GetRepository<Employee>().GetAsync(x => x.Id == request.EmployeeId);
                    var department = await _unitWork.GetRepository<Department>().GetAsync(x => x.Id == employee.DepartmentId);
                    var company = await _unitWork.GetRepository<Company>().GetAsync(x => x.Id == employee.CompanyId);
                    var product = await _unitWork.GetRepository<Product>().GetAsync(x => x.Id == detail.ProductId);
                    var category = await _unitWork.GetRepository<Category>().GetAsync(x => x.Id == product.CategoryId);


                    await _unitWork.GetRepository<ManagementReport>().InsertAsync(new ManagementReport
                    {
                        CompanyId = employee.CompanyId,
                        EmployeeName = employee.EmployeeName,
                        EmployeeSurname = employee.EmployeeSurname,
                        EmployeeCompanyName = company.CompanyName,
                        EmployeeDepartmentName = department.DepartmentName,
                        RequestNo = request.RequestNo,
                        ProductQuantity = detail.Quantity,
                        CategoryName = category.CategoryName,
                        ProductName = product.ProductName,
                        Price = offer.OfferPrice,
                        PriceCurrency = offer.PriceCurrency,
                        CompanyMail = supplier.CompanyMail,
                        SupplierCompanyName = supplier.CompanyName,
                        CompanyPhone = supplier.CompanyPhone,
                        CreateTime = DateTime.Now,
                        IsRead = false
                    });

                }

                var time = await _unitWork.GetRepository<TimeReport>().GetAsync(x => x.RequestId == billModelItem.RequestId);
                time.AccountDate = DateTime.Now;
                time.StockDate = DateTime.Now;
                await _unitWork.GetRepository<TimeReport>().UpdateAsync(time);


                var requestt = await _unitWork.GetRepository<Request>().GetAsync(c => c.Id == billModelItem.RequestId);
                var basketdetails = await _unitWork.GetRepository<BasketDetail>().GetAllAsync(x => x.BasketId == requestt.BasketId);
                List<string> products = new List<string>();
                foreach (var item in basketdetails)
                {
                    var product = await _unitWork.GetRepository<Product>().GetAsync(x => x.Id == item.ProductId);
                    products.Add(product.ProductName);
                }

                var employe = await _unitWork.GetRepository<Employee>().GetAsync(x => x.Id == requestt.EmployeeId);
                var productListString = string.Join(" , ", products);
                await _mailService.SendMessageAsync($"{employe.Email}", _messageServices.SuccessRequest(), "Talep ettiğiniz ürünler; <br>" +
                    $"{productListString} kabul edilip onaylanmış ve stoğa girişi başarılı bir şekilde yapılmıştır. <br>" +
                    $"İyi günler dileriz.", true);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }


            return ApiResponse<NoData>.Success(StatusCodes.Status200OK);
        }
        public async Task<ApiResponse<List<BillGetDto>>> GetAllbyCompanyIdAsync(int companyId)
        {
            if (companyId <= 0)
                throw new BadRequestException("id değeri 0 dan küçük veya eşit olamaz");

            try
            {
                var bills = await _unitWork.GetRepository<Bill>().GetAllAsync(x => x.CompanyId == companyId);

                var dtos = new List<BillGetDto>();

                foreach (var bill in bills)
                {
                    var supplier = await _unitWork.GetRepository<Supplier>().GetAsync(x => x.Id == bill.SupplierId);

                    dtos.Add(new BillGetDto
                    {
                        Id = (int)bill.Id,
                        BillDate = bill.BillDate,
                        BillNumber = bill.BillNumber,
                        ProductAmount = bill.ProductAmount,
                        TotalKDV = bill.TotalKDV,
                        Currency = bill.Currency,
                        Discount = bill.Discount,
                        BillTotalPrice = bill.BillTotalPrice,
                        CompanyName = supplier.CompanyName
                    });
                }
            return ApiResponse<List<BillGetDto>>.Success(StatusCodes.Status200OK, dtos);

            }
            catch(Exception ex)
            {
                throw new NoContentException(ex.Message == null ? "İşlem gerçeklşemedi ve hata mesajı boş" : "İşlem gerçekleşemedi Exception içeriğine bakınız");
            }
        }
    }
}