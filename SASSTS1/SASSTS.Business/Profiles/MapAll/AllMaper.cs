using AutoMapper;
using SASSTS.Model.Dtos.Accounting;
using SASSTS.Model.Dtos.Authority;
using SASSTS.Model.Dtos.Basket;
using SASSTS.Model.Dtos.BasketDetail;
using SASSTS.Model.Dtos.Bill;
using SASSTS.Model.Dtos.Category;
using SASSTS.Model.Dtos.Company;
using SASSTS.Model.Dtos.Department;
using SASSTS.Model.Dtos.Employee;
using SASSTS.Model.Dtos.ManagementReport;
using SASSTS.Model.Dtos.Message;
using SASSTS.Model.Dtos.Offer;
using SASSTS.Model.Dtos.ProcessHistory;
using SASSTS.Model.Dtos.Product;
using SASSTS.Model.Dtos.Role;
using SASSTS.Model.Dtos.Stock;
using SASSTS.Model.Dtos.StockOperation;
using SASSTS.Model.Dtos.Supplier;
using SASSTS.Model.Dtos.Support;
using SASSTS.Model.Dtos.TimeReport;
using SASSTS.Model.Dtos.Todo;
using SASSTS.Model.Dtos.Unit;
using SASSTS.Model.Entities;
using SASSTS.Model.RequestModels.AccountingVM;

namespace SASSTS.Business.Profiles.MapAll
{
    public class AllMaper : Profile
    {
        public AllMaper()
        {
            CreateMap<Department, DepartmentGetDto>();

            CreateMap<Accounting, AccountingGetDto>()
                .ForMember(dest => dest.EmployeeName, opt => opt.MapFrom(src => src.Employee.EmployeeName))
                .ForMember(dest => dest.EmployeeSurname, opt => opt.MapFrom(src => src.Employee.EmployeeSurname))
                .ForMember(dest => dest.Image, opt => opt.MapFrom(src => src.Employee.Image));

            CreateMap<Accounting, AccountingPutDto>();

            CreateMap<Basket, BasketGetDto>();
            CreateMap<Basket, BasketPostDto>().ReverseMap();

            CreateMap<BasketDetail, BasketDetailGetDto>()
                .ForMember(dest => dest.ProductName, opt => opt.MapFrom(src => src.Product.ProductName))
                .ForMember(dest => dest.CreatedTime, opt => opt.MapFrom(src => src.Basket.CreatedTime));
            CreateMap<BasketDetail, BasketDetailPostDto>().ReverseMap();

            CreateMap<Bill, BillPostDto>().ReverseMap();
            CreateMap<Bill, BillGetDto>();

            CreateMap<Employee, EmployeePostDto>().ReverseMap();
            CreateMap<Employee, EmployeeGetDto>();
            CreateMap<Employee, EmployeePutDto>().ReverseMap();
            CreateMap<Employee, EmployeeGetUIDto>()
                .ForMember(dest => dest.RoleName, opt => opt.MapFrom(src => src.Role.RoleName))
                .ForMember(dest => dest.AuthorityName, opt => opt.MapFrom(src => src.Authority.AuthorityName))
                .ForMember(dest => dest.CompanyName, opt => opt.MapFrom(src => src.Company.CompanyName))
                .ForMember(dest => dest.DepartmentName, opt => opt.MapFrom(src => src.Department.DepartmentName));

            CreateMap<Message, MessagePostDto>().ReverseMap();
            CreateMap<Message, MessageGetDto>();

            CreateMap<Offer, OfferGetDto>()
               .ForMember(dest => dest.CompanyName, opt => opt.MapFrom(src => src.Supplier.CompanyName));
            CreateMap<Offer, OfferPostDto>().ReverseMap();

            CreateMap<Product, ProductGetDto>()
                .ForMember(dest => dest.UnitName, opt => opt.MapFrom(src => src.Unit.UnitName));
            CreateMap<Product, ProductPutDto>().ReverseMap();
            CreateMap<Product, ProductPostDto>().ReverseMap();

            CreateMap<Stock, StockGetDto>()
                .ForMember(dest => dest.ProductName, opt => opt.MapFrom(src => src.Product.ProductName))
                .ForMember(dest => dest.CompanyName, opt => opt.MapFrom(src => src.Company.CompanyName))
                .ForMember(dest => dest.ProductImage, opt => opt.MapFrom(src => src.Product.ProductImage));

            CreateMap<StockOperation, StockOperationGetDto>()
              .ForMember(dest => dest.EmployeeName, opt => opt.MapFrom(src => src.Employee.EmployeeName))
              .ForMember(dest => dest.EmployeeSurname, opt => opt.MapFrom(src => src.Employee.EmployeeSurname));

            CreateMap<Supplier, SupplierGetDto>();

            CreateMap<Unit, UnitGetDto>();
            CreateMap<Unit, UnitPostDto>().ReverseMap();
            CreateMap<Unit, UnitPutDto>().ReverseMap();

            CreateMap<Authority, AuthorityGetDto>();
            CreateMap<Authority, AuthorityPutDto>().ReverseMap();

            CreateMap<Category, CategoryGetDto>();
            CreateMap<Category, CategoryPutDto>().ReverseMap();

            CreateMap<Company, CompanyGetDto>();
            CreateMap<Company, CompanyPostDto>().ReverseMap();
            CreateMap<Company, CompanyPutDto>().ReverseMap();

            CreateMap<ManagementReport, ManagementReportGetDto>();

            CreateMap<Role, RoleGetDto>();

            CreateMap<TimeReport, TimeReportGetDto>();

            CreateMap<Todo, TodoGetDto>();
            CreateMap<Todo, TodoPostDto>().ReverseMap();
            CreateMap<Todo, TodoPutDto>().ReverseMap();

            CreateMap<Supplier, SupplierPostDto>().ReverseMap();
            CreateMap<Supplier, SupplierPutDto>().ReverseMap();

            CreateMap<Model.Entities.ProcessHistory, ProcessHistoryGetDto>();

            CreateMap<Accounting, UpdateUserVM>()
                .ForMember(dest => dest.Phone, opt => opt.MapFrom(src => src.Employee.Phone));

            CreateMap<Category, CategoryGetDto>();
            CreateMap<Category, CategoryPostDto>().ReverseMap();

            CreateMap<Department, DepartmentPostDto>().ReverseMap();
            CreateMap<Department, DepartmentPutDto>().ReverseMap();

            CreateMap<Role, RolePostDto>().ReverseMap();
            CreateMap<Role, RolePutDto>().ReverseMap();


            CreateMap<Accounting, EmployeeandAccountModel>()
                .ForMember(dest => dest.Phone, opt => opt.MapFrom(src => src.Employee.Phone))
                .ForMember(dest => dest.Image, opt => opt.MapFrom(src => src.Employee.Image));

            CreateMap<Authority, AuthorityPostDto>().ReverseMap();

            CreateMap<Support, SupportPostDto>().ReverseMap();
        }
    }   
}