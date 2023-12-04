namespace SatinAlmaStokYonetim.Web.MessageServices.Interfaces
{
    public interface IMessageService
    {
        string RegisterMessage();

        string NewPasswordMessage();

        string UpdateUserMessage();

        string UserNameMessage();

        string PasswordMessage();

        string PasswordResetMessage();

        string LoginCodeMessage();

        string Hitap();

        string SubjectMessage();

        string SubjectMessagePassword();

        string SubjectMessageLogin();

        string SuccessRequest();

    }
}