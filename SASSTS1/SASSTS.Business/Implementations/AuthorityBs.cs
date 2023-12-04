using Infrastructure.ApiResponses;
using Infrastructure.Models;
using SASSTS.Business.Controls.Dto;
using SASSTS.Business.Interfaces;
using SASSTS.Model.Dtos.Authority;
using SASSTS.Model.Entities;

namespace SASSTS.Business.Implementations
{
    public class AuthorityBs : BusinessMap, IAuthorityBs
    {
        private readonly IExceptionValidDto<Authority, AuthorityGetDto, AuthorityBs, AuthorityPostDto, AuthorityPutDto> _authorityBs;
        public AuthorityBs(IExceptionValidDto<Authority, AuthorityGetDto, AuthorityBs, AuthorityPostDto, AuthorityPutDto> authorityBs) => _authorityBs = authorityBs;
        public async Task<ApiResponse<List<AuthorityGetDto>>> GetAllAsync() => await _authorityBs.GetAllDtosController("Authority", x => x.IsDeleted == false);
        public async Task<ApiResponse<NoData>> InsertAuthority(AuthorityPostDto dto) => await _authorityBs.InsertAsyncNoData(dto, "Authority", 
            x => x.IsDeleted = false, x => x.AuthorityName == dto.AuthorityName);
        public async Task<ApiResponse<NoData>> DeleteAsync(int id) => await _authorityBs.DeleteAsync("Authority", prd => prd.Id == id, mdl => mdl.IsDeleted =  true);
        public async Task<ApiResponse<NoData>> UpdateAsync(AuthorityPutDto dto) => await _authorityBs.UpdateDto(dto, "Authority", null!,
            x => x.AuthorityName == dto.AuthorityName);
    }
}