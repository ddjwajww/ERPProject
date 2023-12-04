using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SASSTS.Model.Dtos.ManagementReport
{
    public class MRGetDto
    {
        public int Id { get; set; }//
        public int CompanyId { get; set; } //
        public string EmployeeName { get; set; }
        public string EmployeeSurname { get; set; }
        public string EmployeeCompanyName { get; set; }//
        public string EmployeeDepartmentName { get; set; }//
        public string RequestNo { get; set; }//
        public decimal Price { get; set; }//
        public string PriceCurrency { get; set; }//
        public string CompanyMail { get; set; }//
        public string SupplierCompanyName { get; set; }//
        public string CompanyPhone { get; set; }//
        public DateTime? CreateTime { get; set; }//
        public List<DetailProducts> DetailProducts { get; set; }
    }
    public class DetailProducts
    {
        public int ProductQuantity { get; set; }
        public string CategoryName { get; set; }//
        public string ProductName { get; set; }//
    }
}
