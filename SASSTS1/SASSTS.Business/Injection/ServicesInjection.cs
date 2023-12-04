using Infrastructure.Mail.Implementations;
using Infrastructure.Mail.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using SASSTS.Business.Controls.Dto;
using SASSTS.Business.Controls.Implementations;
using SASSTS.Business.Implementations;
using SASSTS.Business.Interfaces;
using SASSTS.Business.MessageServices.Implementations;
using SASSTS.Business.MessageServices.Interfaces;
using SASSTS.Business.Profiles.MapAll;
using SASSTS.Business.RandomSystems;
using SASSTS.DataAccess.EntityFramework.Context;
using SASSTS.DataAccess.UnitOfWork;
using SASSTS.Model.Dtos.Authority;
using SASSTS.Model.Dtos.Basket;
using SASSTS.Model.Dtos.BasketDetail;
using SASSTS.Model.Dtos.Category;
using SASSTS.Model.Dtos.Company;
using SASSTS.Model.Dtos.Department;
using SASSTS.Model.Dtos.Employee;
using SASSTS.Model.Dtos.ManagementReport;
using SASSTS.Model.Dtos.Message;
using SASSTS.Model.Dtos.ProcessHistory;
using SASSTS.Model.Dtos.Product;
using SASSTS.Model.Dtos.Role;
using SASSTS.Model.Dtos.Stock;
using SASSTS.Model.Dtos.Supplier;
using SASSTS.Model.Dtos.TimeReport;
using SASSTS.Model.Dtos.Todo;
using SASSTS.Model.Dtos.Unit;
using SASSTS.Model.Entities;

namespace SASSTS.Business.Injection
{
    public static class ServicesInjection
    {
        public static void AddBusinessServices(this IServiceCollection services)
        {
            services.AddAutoMapper(typeof(AllMaper));
            services.AddScoped<IManagementReportBs, ManagementReportBs>();
            services.AddScoped<IStockOperationBs, StockOperationBs>();
            services.AddScoped<IStockBs, StockBs>();
            services.AddScoped<IBillDetailBs, BillDetailBs>();
            services.AddScoped<IBillBs, BillBs>();
            services.AddScoped<ISupplierBs, SupplierBs>();
            services.AddScoped<IDepartmentBs, DepartmentBs>();
            services.AddScoped<IMessageBs, MessageBs>();
            services.AddScoped<IAuthorityBs, AuthorityBs>();
            services.AddScoped<IRoleBs, RoleBs>();
            services.AddScoped<IAccountingBs, AccountingBs>();
            services.AddScoped<IBasketDetailBs, BasketDetailBs>();
            services.AddScoped<IUnitBs, UnitBs>();
            services.AddScoped<IProductBs, ProductBs>();
            services.AddScoped<IRequestBs, RequestBs>();
            services.AddScoped<IBasketBs, BasketBs>();
            services.AddScoped<ISupportBs, SupportBs>();
            services.AddScoped<ICategoryBs, CategoryBs>();
            services.AddScoped<ICompanyBs, CompanyBs>();
            services.AddScoped<IEmployeeBs, EmployeeBs>();
            services.AddScoped<IOfferBs, OfferBs>();
            services.AddScoped<IRequestBs, RequestBs>();
            services.AddScoped<ITodosBs, TodoBs>();
            services.AddScoped<ITimeReportBs, TimeReportBs>();
            services.AddScoped<IProcessHistoryBs, ProcessHistoryBs>();
            services.AddScoped<IMessageService, MessageService>();
            services.AddScoped<IMailService, MailService>();
            services.AddScoped<IUnitWork, UnitWork>();
            services.AddScoped<SASSTSDataContext>();
            services.AddScoped<IRandomNumber, RandomSystems.RandomNumber>();
            //ExceptionValidDtolar için her bir kullanan sınıf bağımlılığını buraya bildirmelidir.
            services.AddScoped<IExceptionValidDto<Authority, AuthorityGetDto, AuthorityBs, AuthorityPostDto, AuthorityPutDto>,
                ExceptionValidDto<Authority, AuthorityGetDto, AuthorityBs, AuthorityPostDto, AuthorityPutDto>>();
            services.AddScoped<IExceptionValidDto<Unit, UnitGetDto, UnitBs, UnitPostDto, UnitPutDto>, 
                ExceptionValidDto<Unit, UnitGetDto, UnitBs, UnitPostDto, UnitPutDto>>();
            services.AddScoped<IExceptionValidDto<Category, CategoryGetDto, CategoryBs, CategoryPostDto, CategoryPutDto>,
                ExceptionValidDto<Category, CategoryGetDto, CategoryBs, CategoryPostDto, CategoryPutDto>>();
            services.AddScoped<IExceptionValidDto<Company, CompanyGetDto, CompanyBs, CompanyPostDto, CompanyPutDto>,
                ExceptionValidDto<Company, CompanyGetDto, CompanyBs, CompanyPostDto, CompanyPutDto>>();
            services.AddScoped<IExceptionValidDto<Basket, BasketGetDto, BasketBs, BasketPostDto, BasketGetDto>,
                ExceptionValidDto<Basket, BasketGetDto, BasketBs, BasketPostDto, BasketGetDto>>();
            services.AddScoped<IExceptionValidDto<Department, DepartmentGetDto, DepartmentBs, DepartmentPostDto, DepartmentPutDto>,
                ExceptionValidDto<Department, DepartmentGetDto, DepartmentBs, DepartmentPostDto, DepartmentPutDto>>();
            services.AddScoped<IExceptionValidDto<Message, MessageGetDto, MessageBs, MessagePostDto, MessageGetDto>,
                ExceptionValidDto<Message, MessageGetDto, MessageBs, MessagePostDto, MessageGetDto>>();
            services.AddScoped<IExceptionValidDto<ProcessHistory, ProcessHistoryGetDto, ProcessHistoryBs, ProcessHistoryPostDto, ProcessHistoryGetDto>,
                ExceptionValidDto<ProcessHistory, ProcessHistoryGetDto, ProcessHistoryBs, ProcessHistoryPostDto, ProcessHistoryGetDto>>();
            services.AddScoped<IExceptionValidDto<Role, RoleGetDto, RoleBs, RolePostDto, RolePutDto>,
                ExceptionValidDto<Role, RoleGetDto, RoleBs, RolePostDto, RolePutDto>>();
            services.AddScoped<IExceptionValidDto<Stock, StockGetDto, StockBs, StockPostDto, StockGetDto>,
                ExceptionValidDto<Stock, StockGetDto, StockBs, StockPostDto, StockGetDto>>();
            services.AddScoped<IExceptionValidDto<Supplier, SupplierGetDto, SupplierBs, SupplierPostDto, SupplierPutDto>,
                ExceptionValidDto<Supplier, SupplierGetDto, SupplierBs, SupplierPostDto, SupplierPutDto>>();
            services.AddScoped<IExceptionValidDto<TimeReport, TimeReportGetDto, TimeReportBs, TimeReportGetDto, TimeReportGetDto>,
                ExceptionValidDto<TimeReport, TimeReportGetDto, TimeReportBs, TimeReportGetDto, TimeReportGetDto>>();
            services.AddScoped<IExceptionValidDto<BasketDetail, BasketDetailGetDto, BasketDetailBs, BasketDetailPostDto, BasketDetailGetDto>,
                ExceptionValidDto<BasketDetail, BasketDetailGetDto, BasketDetailBs, BasketDetailPostDto, BasketDetailGetDto>>();
            services.AddScoped<IExceptionValidDto<ManagementReport, ManagementReportGetDto, ManagementReportBs, ManagementReportPostDto, ManagementReportGetDto>,
                ExceptionValidDto<ManagementReport, ManagementReportGetDto, ManagementReportBs, ManagementReportPostDto, ManagementReportGetDto>>();
            services.AddScoped<IExceptionValidDto<Product, ProductGetDto, ProductBs, ProductPostDto, ProductPutDto>,
                ExceptionValidDto<Product, ProductGetDto, ProductBs, ProductPostDto, ProductPutDto>>();
            services.AddScoped<IExceptionValidDto<Employee, EmployeeGetDto, EmployeeBs, EmployeePostDto, EmployeePutDto>,
                ExceptionValidDto<Employee, EmployeeGetDto, EmployeeBs, EmployeePostDto, EmployeePutDto>>();
            services.AddScoped<IExceptionValidDto<Todo, TodoGetDto, TodoBs, TodoPostDto, TodoPutDto>,
                ExceptionValidDto<Todo, TodoGetDto, TodoBs, TodoPostDto, TodoPutDto>>();
        }
    }
}