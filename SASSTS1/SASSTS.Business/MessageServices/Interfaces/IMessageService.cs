using SASSTS.Model.Entities;
using SASSTS.Model.RequestModels.AccountingVM;

namespace SASSTS.Business.MessageServices.Interfaces
{
    public interface IMessageService
    {
        string SubjectMessage();
        public string SubjectMessagePassword();
        public string UpdateUser();
        public string SubjectMessageLogin();
        string RegisterMessage(RegisterVM createUserVM);
        string NewPasswordMessage(Employee employee);
        string UpdateUserMessage(Employee employee);
        string SuccessRequest();
    }
}