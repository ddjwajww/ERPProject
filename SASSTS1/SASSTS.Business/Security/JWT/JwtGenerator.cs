using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using SASSTS.DataAccess.UnitOfWork;
using SASSTS.Model.Entities;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace SASSTS.Business.Security.JWT
{
    public class JwtGenerator
    {
        private readonly IUnitWork _unitWork;
        private readonly IConfiguration _configuration;
        private TokenOptions _tokenOptions;
        private DateTime _tokenExpiration;

        public JwtGenerator(IConfiguration configuration, IUnitWork unitWork)
        {
            _configuration = configuration;
            _unitWork = unitWork;   
            _tokenOptions = (_configuration.GetSection("TokenOptions").Get<TokenOptions>())!;
        }

        public async Task<AccessToken> CreateAccessToken(long employeeId)
        {
            var employee = await _unitWork.GetRepository<Employee>().GetAsync(x => x.Id == employeeId);
            var authority = await _unitWork.GetRepository<Authority>().GetAsync(x => x.Id == employee.AuthorityId);
            var department = await _unitWork.GetRepository<Department>().GetAsync(x => x.Id == employee.DepartmentId);
            var company = await _unitWork.GetRepository<Company>().GetAsync(x => x.Id == employee.CompanyId);
            var role = await _unitWork.GetRepository<Role>().GetAsync(x => x.Id == employee.RoleId);    

            _tokenExpiration = DateTime.Now.AddMinutes(_tokenOptions.TokenExpiration);

            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("StokTakipveYönetimSistemiGirisAnahtari"));

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim(ClaimTypes.Name, employee.EmployeeName),
                    new Claim(ClaimTypes.Role, role.RoleName)
                }),
                Expires = _tokenExpiration,
                SigningCredentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256)
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tokenDescriptor);
            var tok =  tokenHandler.WriteToken(token);

            var data = new AccessToken { Token = tok, Claims = new List<string> { $"{employee.EmployeeName}",$"{employee.EmployeeSurname}", 
                $"{authority.AuthorityName}", $"{employee.Id}", $"{employee.CompanyId}", $"{employee.DepartmentId}", $"{company.CompanyName}", 
                $"{department.DepartmentName}",$"{employee.Image}",$"{authority.Id}", $"{role.RoleName}", },
                Expiration = _tokenExpiration
            };
            return data;
        }
    }
}