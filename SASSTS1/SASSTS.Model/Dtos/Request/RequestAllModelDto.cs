namespace SASSTS.Model.Dtos.Request
{
    public class RequestAllModelDto
    {
        public long UserId { get; set; }       //Sessiondan gelecek.
        public long CompanyId { get; set; }    //Sessiondan gelecek.
        // public long DepartmentId { get; set; } Yine sessiondan gelecek bu property eklenecek.
        public long DepartmentId { get; set; }
        public List<Products> Basket { get; set; }
    }

    public class Products
    {
        public long ProductId { get; set; }
        public long ProductQuantity { get; set; }
        public long ProductUnitId { get; set; }
        public long ProductUnitQuantity { get; set; }
    }
}
