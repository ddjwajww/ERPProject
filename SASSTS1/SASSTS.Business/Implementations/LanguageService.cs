using Microsoft.Extensions.Localization;
using System.Globalization;
using System.Reflection;

namespace SASSTS.Business.Implementations
{
    public class SharedResource
    {

    }
    public class LanguageService
    {
        private readonly IStringLocalizer _localizer;

        public LanguageService(IStringLocalizerFactory factory)
        {
            var type = typeof(SharedResource);
            var assemblyName = new AssemblyName(type.GetTypeInfo().Assembly.FullName);
            _localizer = factory.Create(nameof(SharedResource), assemblyName.Name);
        }

        public LocalizedString Getkey(string key)
        {
            return _localizer[key];
        }

        public LocalizedString Getkey(object key2)
        {
            return _localizer[key2.ToString()];
        }
        public string GetImagePath(string imageName)
        {

            string imagePath = $"/img/{CultureInfo.CurrentCulture.Name}/{imageName}";
            return imagePath;
        }

    }
}
