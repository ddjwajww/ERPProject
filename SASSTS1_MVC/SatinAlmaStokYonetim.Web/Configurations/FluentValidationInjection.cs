using FluentValidation.AspNetCore;
using SatinAlmaStokYonetim.Web.Validators.Accounts;

namespace SatinAlmaStokYonetim.Web.Configurations
{
    public static class FluentValidationInjection
    {
        public static IServiceCollection AddFluentValidationService(this IServiceCollection services)
        {
            services.AddControllersWithViews().AddFluentValidation(opt =>
            {
                opt.RegisterValidatorsFromAssemblyContaining<LoginValidator>();
                opt.DisableDataAnnotationsValidation = true;
                opt.ValidatorOptions.LanguageManager.Culture = new System.Globalization.CultureInfo("tr-TR");
                opt.ValidatorOptions.LanguageManager.Culture = new System.Globalization.CultureInfo("en-GB");
                opt.ValidatorOptions.LanguageManager.Culture = new System.Globalization.CultureInfo("de-DE");
                opt.ValidatorOptions.LanguageManager.Culture = new System.Globalization.CultureInfo("it-IT");
                opt.ValidatorOptions.LanguageManager.Culture = new System.Globalization.CultureInfo("es-ES");
            });
            return services;
        }
    }
}