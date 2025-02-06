using Barber.Colocho.Repository.Repository;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Barber.Colocho.Core.Helpers
{
    public class AuthService
    {
        private readonly IConfiguration _config;
        private readonly IGenericRepository<Entities.Tables.User> _userManager;

        public AuthService(IConfiguration config, IGenericRepository<Entities.Tables.User> userManager)
        {
            _config = config;
            _userManager = userManager;
        }

        public async Task<string> GenerateAccessToken(Entities.Tables.User user)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Sid, user.Id.ToString()),
                new Claim(ClaimTypes.Name, user.Name)
            };


            claims.Add(new Claim(ClaimTypes.Role, nameof(user.Roles.RolName)));
            int minutes = Convert.ToInt32(_config["Jwt:JwtExpireMinutes"]);
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:JwtKey"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256Signature);
            var tokenDescriptor = new JwtSecurityToken(
                issuer: _config["Jwt:JwtIssuer"],
                audience: _config["Jwt:JwtAudience"],
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(minutes),
                signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(tokenDescriptor);
        }
    }
}
