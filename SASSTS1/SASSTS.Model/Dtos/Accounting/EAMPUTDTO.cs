namespace SASSTS.Model.Dtos.Accounting
{
    public class EAMPUTDTO
    {
        public long Id { get; set; }   //bu gelecek olan employeeId
        public string Phone { get; set; }
        public string Email { get; set; }
        public string UserPassword { get; set; }
        public string? UserName { get; set; }
        public string Image { get; set; }
    }
}
