﻿using Infrastructure.Models;

namespace SASSTS.Model.Dtos.BillAllModel
{
    public class BillModelItem : IDto
    {
        public int RequestId { get; set; }
        public int SupplierId { get; set; }
        public string BillType { get; set; }
        public DateTime BillDate { get; set; }
        public string BillNumber { get; set; }
        public decimal Discount { get; set; }
        public decimal ProductAmount { get; set; }
        public decimal TotalKDV { get; set; }
        public decimal BillTotalPrice { get; set; }
        public string Currency { get; set; }
        public int CompanyId { get; set; }
        public int EmployeeId { get; set; }
        public List<BillDetailItem> BillDetailItem { get; set; }
    }
    public class BillDetailItem
    {
        public int ProductId { get; set; }
        public decimal UnitPrice { get; set; }
        public int ProductKdv { get; set; }
        public int ProductQuantity { get; set; }
    }
}