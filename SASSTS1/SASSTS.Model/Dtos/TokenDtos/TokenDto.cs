namespace SASSTS.Model.Dtos.TokenDtos
{
    public class TokenDto
    {
        public int Role { get; set; }
        public int Authority { get; set; }
        public long EmployeeId { get; set; }
        public string EmployeeName { get; set; }
        public string EmployeeSurname { get; set; }
        public string Email { get; set; }
        public DateTime ExpireDate { get; set; }
        public string Token { get; set; }
    }
}