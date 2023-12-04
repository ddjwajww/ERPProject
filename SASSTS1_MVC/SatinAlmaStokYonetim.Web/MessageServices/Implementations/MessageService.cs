using SatinAlmaStokYonetim.Web.MessageServices.Interfaces;
using SatinAlmaStokYonetim.Web.Services;

namespace SatinAlmaStokYonetim.Web.MessageServices.Implementations
{
    public class MessageService : IMessageService
    {
        private readonly LanguageService _languageService;

        public MessageService(LanguageService languageService)
        {
            _languageService = languageService;
        }


        public string RegisterMessage()
        {
            var message = _languageService.Getkey($"Personel kayıt işleminiz tamamlandı.   Giriş bilgileriniz :").Value;
            return message;
        }
        public string NewPasswordMessage()
        {
            var message = _languageService.Getkey("Şifre güncelleme işlemi başarılı bir şekilde tamamlanmıştır.Güncelleme işlemi size ait değil ise, lütfen şifrenizi güncelleyiniz.").Value;
            return message;
        }

        public string UpdateUserMessage()
        {
            var message = _languageService.Getkey($"Kullanıcı bilgileriniz güncellenmiştir.").Value;
            return message;
        }

        public string UserNameMessage()
        {
            var message = _languageService.Getkey("Kullanıcı Adı :").Value;
            return message;
        }

        public string PasswordMessage()
        {
            var message = _languageService.Getkey("Şifre :").Value;
            return message;
        }

        public string PasswordResetMessage()
        {
            var message = _languageService.Getkey("Lütfen en kısa sürede şifrenizi değiştiriniz.").Value;
            return message;
        }

        public string LoginCodeMessage()
        {
            var message = _languageService.Getkey("Sisteme giriş yapabilmek için doğrulama kodunuz :");
            return message;
        }

        public string Hitap()
        {
            var message = _languageService.Getkey("Sayın").Value;
            return message;
        }

        public string SubjectMessage()
        {
            var message = _languageService.Getkey("ARAMIZA HOŞGELDİNİZ").Value;
            return message;
        }

        public string UpdateUser()
        {
            var message = _languageService.Getkey("Bilgileriniz Güncellendi").Value;
            return message;
        }

        public string SubjectMessagePassword()
        {
            var message = _languageService.Getkey("Şifre Yenileme").Value;
            return message;
        }

        public string SubjectMessageLogin()
        {
            var message = _languageService.Getkey("Giriş Doğrulama").Value;
            return message;
        }

        public string SuccessRequest()
        {
            var message = _languageService.Getkey("Ürün Stok Girişi").Value;
            return message;
        }

    }
}