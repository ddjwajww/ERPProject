using Infrastructure.Models;
using System.ComponentModel.DataAnnotations;

namespace SASSTS.Model.Entities
{
    public class ManagementReport : IEntity
    {
        [Key] public int Id { get; set; }


        //Employee için
        public int CompanyId { get; set; }
        public string EmployeeName { get; set; }
        public string EmployeeSurname { get; set; }
        public string EmployeeCompanyName { get; set; }
        public string EmployeeDepartmentName { get; set; }


        //Ürün Bilgileri
        public string RequestNo { get; set; }
        public int ProductQuantity { get; set; }
        public string CategoryName { get; set; }
        public string ProductName { get; set; }
        public decimal Price { get; set; }
        public string PriceCurrency { get; set; }


        //Tedarikçi Bilgileri
        public string CompanyMail { get; set; }
        public string SupplierCompanyName { get; set; }
        public string CompanyPhone { get; set; }

        //Invoice
        public DateTime? CreateTime { get; set; }
        public bool? IsRead { get; set; }
    }
}
