using AutoMapper;
using SASSTS.Model.Dtos.Employee;
using SASSTS.Model.Entities;
using SASSTS.Model.RequestModels.AccountingVM;
using SASSTS.Model.RequestModels.AuthorityVM;
using SASSTS.Model.RequestModels.BasketDetailVM;
using SASSTS.Model.RequestModels.BasketVM;
using SASSTS.Model.RequestModels.BillVM;
using SASSTS.Model.RequestModels.CategoryVM;
using SASSTS.Model.RequestModels.CompanyVM;
using SASSTS.Model.RequestModels.DepartmentVM;
using SASSTS.Model.RequestModels.EmployeeVM;
using SASSTS.Model.RequestModels.OfferVM;
using SASSTS.Model.RequestModels.ProcessHistoryVM;
using SASSTS.Model.RequestModels.ProductImagesVM;
using SASSTS.Model.RequestModels.ProductVM;
using SASSTS.Model.RequestModels.RequestVM;
using SASSTS.Model.RequestModels.RoleVM;
using SASSTS.Model.RequestModels.StockVM;
using SASSTS.Model.RequestModels.SupplierVM;
using SASSTS.Model.RequestModels.UnitVM;

namespace SASSTS.Business.Profiles.MapAll
{
    public class ViewModelToDomain : Profile
    {
        public ViewModelToDomain()
        {
            //Personel oluşturma
            CreateMap<EmployeePostDto, Employee>()
               .ForMember(x => x.EmployeeName, y => y.MapFrom(e => e.EmployeeName.ToUpper().Trim()))
               .ForMember(x => x.EmployeeSurname, y => y.MapFrom(e => e.EmployeeSurname.ToUpper().Trim()))
               .ForMember(x => x.Email, y => y.MapFrom(e => e.Email.Trim()));
            CreateMap<EmployeePostDto, Accounting>()
                .ForMember(x => x.UserName, y => y.MapFrom(e => e.UserName.Trim()))
                .ForMember(x => x.Email, y => y.MapFrom(e => e.Email.Trim()));

            //Şifre işlemleri
            CreateMap<NewPasswordVM, Accounting>();

            //RandomNumber
            CreateMap<RandomNumberVM, Model.Entities.RandomNumber>()
                .ForMember<string>(x => x.UserName, y => y.MapFrom(e => e.UserName.ToUpper().Trim()))
                .ForMember<string>(x => x.Email, y => y.MapFrom(e => e.Email.Trim()));
            CreateMap<RandomNumberNoVM, Model.Entities.RandomNumber>()
                .ForMember(x => x.Number, y => y.MapFrom(e => e.RandomNumberNo));
            CreateMap<int, Model.Entities.RandomNumber>()
                .ForMember(x => x.Number, y => y.MapFrom(e => e));
            CreateMap<EmployeeMailVM, Model.Entities.RandomNumber>()
                .ForMember<string>(x => x.Email, y => y.MapFrom(e => e.UserData.Trim()));

            //Accounting
            CreateMap<LoginVM, Accounting>()
                .ForMember(x => x.UserName, y => y.MapFrom(e => e.UserData.ToUpper().Trim()))
                .ForMember(x => x.Email, y => y.MapFrom(e => e.UserData.Trim()));
            CreateMap<UpdateUserVM, Accounting>()
                .ForMember(x => x.UserName, y => y.MapFrom(e => e.UserName.ToUpper().Trim()))
                .ForMember(x => x.Email, y => y.MapFrom(e => e.Email.Trim()));
            CreateMap<UpdateUserVM, Employee>()
                .ForMember(x => x.Email, y => y.MapFrom(e => e.Email.Trim()));

            //Authority
            CreateMap<CreateAuthorityVM, Authority>()
                .ForMember(x => x.AuthorityName, y => y.MapFrom(e => e.AuthorityName.ToUpper().Trim()));
            CreateMap<DeleteAuthorityVM, Authority>();
            CreateMap<GetAuthorityByIdVM, Authority>();
            CreateMap<UpdateAuthorityVM, Authority>()
                .ForMember(x => x.AuthorityName, y => y.MapFrom(e => e.AuthorityName.ToUpper().Trim()));

            //BasketDetail
            CreateMap<CreateCategoryVM, BasketDetail>();
            CreateMap<DeleteCategoryVM, BasketDetail>();
            CreateMap<GetBasketDetailByIdVM, BasketDetail>();
            CreateMap<UpdateBasketDetailVM, BasketDetail>();

            //Basket
            CreateMap<CreateBasketVM, Basket>();
            CreateMap<DeleteBasketVM, Basket>();
            CreateMap<GetBasketByIdVM, Basket>();
            CreateMap<UpdateBasketVM, Basket>();

            //Bill
            CreateMap<CreateBillVM, Bill>()
                .ForMember(x => x.Currency, y => y.MapFrom(e => e.Currency.ToUpper().Trim()));
            CreateMap<GetBillByIdVM, Bill>();
            CreateMap<UpdateBillVM, Bill>()
                .ForMember(x => x.Currency, y => y.MapFrom(e => e.Currency.ToUpper().Trim()));

            //Category
            CreateMap<CreateCategoryVM, Category>()
                .ForMember(x => x.CategoryName, y => y.MapFrom(e => e.CategoryName.ToUpper().Trim()));
            CreateMap<DeleteCategoryVM, Category>();
            CreateMap<GetCategoryByIdVM, Category>();
            CreateMap<UpdateCategoryVM, Category>()
                .ForMember(x => x.CategoryName, y => y.MapFrom(e => e.CategoryName.ToUpper().Trim()));

            //Company
            CreateMap<CreateCompanyVM, Company>()
                .ForMember(x => x.CompanyName, y => y.MapFrom(e => e.CompanyName.ToUpper().Trim()))
                .ForMember(x => x.CompanyNo, y => y.MapFrom(e => e.CompanyNo.ToUpper().Trim()));
            CreateMap<DeleteCompanyVM, Company>();
            CreateMap<GetCompanyByIdVM, Company>();
            CreateMap<GetCompanyNoVM, Company>();
            CreateMap<UpdateCompanyVM, Company>()
                .ForMember(x => x.CompanyName, y => y.MapFrom(e => e.CompanyName.ToUpper().Trim()))
                .ForMember(x => x.CompanyNo, y => y.MapFrom(e => e.CompanyNo.ToUpper().Trim()));

            //Department
            CreateMap<CreateDepartmentVM, Department>()
                .ForMember(x => x.DepartmentName, y => y.MapFrom(e => e.DepartmentName.ToUpper().Trim()));
            CreateMap<DeleteDepartmentVM, Department>();
            CreateMap<GetDepartmentByIdVM, Department>();
            CreateMap<UpdateDepartmentVM, Department>()
                .ForMember(x => x.DepartmentName, y => y.MapFrom(e => e.DepartmentName.ToUpper().Trim()));

            //Employee
            CreateMap<DeleteEmployeeVM, Employee>();
            CreateMap<GetEmployeeByIdVM, Employee>();
            CreateMap<UpdateEmployeeVM, Employee>();

            //Offer
            CreateMap<CreateOfferVM, Offer>()
                .ForMember(x => x.PriceCurrency, y => y.MapFrom(e => e.PriceCurrency.ToUpper().Trim()));
            CreateMap<GetOfferByIdVM, Offer>();
            CreateMap<GetOfferFilterRequestVM, Offer>();
            CreateMap<GetOfferFilterSupplierVM, Offer>();
            CreateMap<GetOfferFilterCurrencyVM, Offer>()
                .ForMember(x => x.PriceCurrency, y => y.MapFrom(e => e.PriceCurrency.ToUpper().Trim()));
            CreateMap<UpdateOfferVM, Offer>()
                .ForMember(x => x.PriceCurrency, y => y.MapFrom(e => e.PriceCurrency.ToUpper().Trim()));

            //ProcessHistory
            CreateMap<CreateProcessHistoryVM, ProcessHistory>();
            CreateMap<DeleteProcessHistoryVM, ProcessHistory>();
            CreateMap<GetProcessHistoryByIdVM, ProcessHistory>();
            CreateMap<UpdateProcessHistoryVM, ProcessHistory>();


            //Product
            CreateMap<CreateProductVM, Product>()
                .ForMember(x => x.ProductName, y => y.MapFrom(e => e.ProductName.ToUpper().Trim()));
            CreateMap<DeleteProductVM, Product>();
            CreateMap<GetProductByIdVM, Product>();
            CreateMap<UpdateProductVM, Product>()
                .ForMember(x => x.ProductName, y => y.MapFrom(e => e.ProductName.ToUpper().Trim()));

            //ProductImage
            CreateMap<CreateProductImageVM, ProductImage>();
            CreateMap<DeleteProductImageVM, ProductImage>();
            CreateMap<GetAllProductImageByProductVM, ProductImage>();

            //Request
            CreateMap<CreateRequestVM, Request>();
            CreateMap<DeleteRequestVM, Request>();
            CreateMap<GetRequestByIdVM, Request>();
            CreateMap<UpdateRequestVM, Request>();

            //Role
            CreateMap<CreateRoleVM, Role>()
                .ForMember(x => x.RoleName, y => y.MapFrom(e => e.RoleName.ToUpper().Trim()));
            CreateMap<DeleteRoleVM, Role>();
            CreateMap<GetRoleByIdVM, Role>();
            CreateMap<UpdateRoleVM, Role>()
                .ForMember(x => x.RoleName, y => y.MapFrom(e => e.RoleName.ToUpper().Trim()));

            //Stock
            CreateMap<CreateStockVM, Stock>();
            CreateMap<GetCategoryStockVM, Stock>();
            CreateMap<GetProductStockVM, Stock>();
            CreateMap<GetStockByIdVM, Stock>();
            CreateMap<UpdateStockVM, Stock>();

            //Supplier
            CreateMap<CreateSupplierVM, Supplier>()
                .ForMember(x => x.CompanyName, y => y.MapFrom(e => e.CompanyName.ToUpper().Trim()))
                .ForMember(x => x.CompanyMail, y => y.MapFrom(e => e.CompanyMail.ToUpper().Trim()));
            CreateMap<DeleteSupplierVM, Supplier>();
            CreateMap<GetSupplierByIdVM, Supplier>();
            CreateMap<UpdateSupplierVM, Supplier>()
                .ForMember(x => x.CompanyName, y => y.MapFrom(e => e.CompanyName.ToUpper().Trim()))
                .ForMember(x => x.CompanyMail, y => y.MapFrom(e => e.CompanyMail.ToUpper().Trim()));

            //Unit
            CreateMap<CreateUnitVM, Unit>()
                .ForMember(x => x.UnitName, y => y.MapFrom(e => e.UnitName.ToUpper().Trim()));
            CreateMap<DeleteUnitVM, Unit>();
            CreateMap<GetUnitByIdVM, Unit>();
            CreateMap<UpdateUnitVM, Unit>()
                .ForMember(x => x.UnitName, y => y.MapFrom(e => e.UnitName.ToUpper().Trim()));

        }
    }
}
