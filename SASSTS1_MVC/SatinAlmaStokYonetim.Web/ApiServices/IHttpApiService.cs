using FluentValidation;
using Microsoft.AspNetCore.Mvc;

namespace SatinAlmaStokYonetim.Web.ApiServices
{
    public interface IHttpApiService
    {
        Task<IActionResult> GetData<T>(string endPoint);
        Task<IActionResult> PostData<T>(string endPoint, string item);
        Task<IActionResult> PostData2<T, M>(string endPoint, string item, M models, IValidator<M> _validator);
        Task<IActionResult> DeleteData<T>(string endPoint);
        Task<IActionResult> PutData<T>(string endPoint, string jsonData);
        Task<IActionResult> PutData2<T, M>(string endPoint, string item, M models, IValidator<M> _validator);

    }
}
