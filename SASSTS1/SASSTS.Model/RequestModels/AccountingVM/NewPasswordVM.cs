namespace SASSTS.Model.RequestModels.AccountingVM
{
    public class NewPasswordVM
    {
        public long Id { get; set; }
        public string UserPassword { get; set; }
        public int RandomNumberNo { get; set; }
        public string NewPasswordMessage { get; set; }
        public string Hitap { get; set; }
        public string SubjectMessagePassword { get; set; }
    }
}
