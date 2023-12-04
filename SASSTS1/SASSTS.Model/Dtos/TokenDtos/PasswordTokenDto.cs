namespace SASSTS.Model.Dtos.TokenDtos
{
    public class PasswordTokenDto
    {
        public DateTime ExpireDate { get; set; }
        public string Token { get; set; }
    }
}
