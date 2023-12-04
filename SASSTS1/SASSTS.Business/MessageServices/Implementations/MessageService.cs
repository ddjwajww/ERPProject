using FluentValidation.Resources;
using SASSTS.Business.Implementations;
using SASSTS.Business.MessageServices.Interfaces;
using SASSTS.Model.Entities;
using SASSTS.Model.RequestModels.AccountingVM;
using SASSTS.Model.RequestModels.EmployeeVM;

namespace SASSTS.Business.MessageServices.Implementations
{
    public class MessageService : IMessageService
    {
        private readonly LanguageService _languageService;

        public MessageService(LanguageService languageService)
        {
            _languageService = languageService;
        }

        public string RegisterMessage(RegisterVM createUserVM)
        {
            var message = _languageService.Getkey($"Sayın {createUserVM.EmployeeName.ToUpper()} {createUserVM.EmployeeSurname.ToUpper()}.  Personel kayıt işleminiz tamamlandı.   Giriş bilgileriniz :     Kullanıcı adı :{createUserVM.Email}  Şifreniz : {createUserVM.UserPassword} Lütfen en kısa sürede şifrenizi değiştiriniz.").Value;
            return message;
        }
        public string NewPasswordMessage(Employee employee)
        {

            var message = _languageService.Getkey("Sayın").Value +" "+ ($"{employee.EmployeeName.ToUpper()} {employee.EmployeeSurname.ToUpper()}.")+" " +_languageService.Getkey("Şifre güncelleme işlemi başarılı bir şekilde tamamlanmıştır.Güncelleme işlemi size ait değil ise, lütfen şifrenizi güncelleyiniz.").Value;
            return message;
        }

        public string UpdateUserMessage(Employee employee)
        {
            var message = _languageService.Getkey($"Sayın {employee.EmployeeName.ToUpper()} {employee.EmployeeSurname.ToUpper()} kullanıcı bilgileriniz güncellenmiştir.").Value;
            return message;
        }


        public string SubjectMessage()
        {
            var message = "ARAMIZA HOŞGELDİNİZ";
            return message;
        }

        public string UpdateUser()
        {
            var message = "Bilgileriniz Güncellendi";
            return message;
        }

        public string SubjectMessagePassword()
        {
            var message = _languageService.Getkey("Şifre Yenileme").Value;
            return message;
        }

        public string SubjectMessageLogin()
        {
            var message = "Giriş Doğrulama";
            return message;
        }

        public string SuccessRequest()
        {
            var message = "Ürün Stok Girişi";
            return message;
        }

    }
}