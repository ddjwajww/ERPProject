using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using SatinAlmaStokYonetim.Web.ApiServices;
using SatinAlmaStokYonetim.Web.Models.Authority;
using SatinAlmaStokYonetim.Web.Models.Response;
using System.Text.Json;

namespace SatinAlmaStokYonetim.Web.Controllers
{
    public class AuthorityController : Controller
    {
        private readonly IHttpApiService _httpApiService;
        private readonly IValidator<AuthorityPostDto> _validatorAuthorityPostDto;
        private readonly IValidator<AuthorityPutDto> _validatorAuthorityPutDto;
        public AuthorityController(IHttpApiService httpApiService, IValidator<AuthorityPostDto> validatorAuthorityPostDto, IValidator<AuthorityPutDto> validatorAuthorityPutDto)
        {
            _httpApiService = httpApiService;
            _validatorAuthorityPostDto = validatorAuthorityPostDto;
            _validatorAuthorityPutDto = validatorAuthorityPutDto;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllAuthority() =>
            await _httpApiService.GetData<List<AuthorityGetDto>>("authority/getAllAuthorities");

        [HttpPost]
        public async Task<IActionResult> CreateAuthority([FromBody] AuthorityPostDto dto) =>
             await _httpApiService.PostData2<NoData, AuthorityPostDto>("authority/insertAuthority", JsonSerializer.Serialize(dto), dto, _validatorAuthorityPostDto); 
        
        [HttpPost]
        public async Task<IActionResult> InsertAuthority([FromBody] AuthorityPostDto dto) =>
            await _httpApiService.PostData2<NoData, AuthorityPostDto>("authority/insertAuthority", JsonSerializer.Serialize(dto), dto, _validatorAuthorityPostDto);

        [HttpPost]
        public async Task<IActionResult> UpdateAuthority([FromBody] AuthorityPutDto dto) =>
            await _httpApiService.PutData2<NoData, AuthorityPutDto>("authority/updateAuthority", JsonSerializer.Serialize(dto), dto, _validatorAuthorityPutDto);

        [HttpPost]
        public async Task<IActionResult> DeleteAuthority([FromBody] int id) =>
            await _httpApiService.DeleteData<NoData>($"authority/deleteAuthority?id={id}");
    }
}
