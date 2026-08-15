using E_Commerece.Application.Contracts;
using E_Commerece.Application.Dtos.AuthDtos;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace E_Commerece.Infrastructure.Services
{
    public class JWTAccessTokenGenerator : IAcessTokenGenerator
    {

        private readonly JwtSecurityTokenHandler _handler = new();
        public string GenerateToken(UserDTo userInfo, IEnumerable<string> Roles)
        {
            //Validation
            ArgumentNullException.ThrowIfNull(userInfo,nameof(userInfo));
            if(userInfo.DisplayName == null) throw new ArgumentNullException(nameof(userInfo), "Name Required");
            if(userInfo.UserName == null) throw new ArgumentNullException(nameof(userInfo),"Username Required");
            if(userInfo.Eamil == null) throw new ArgumentNullException(nameof(userInfo),"Email Requird");

            //Claims
            var claims = new List<Claim>
            {
                new(ClaimTypes.NameIdentifier, userInfo.Id),
                //new(ClaimTypes.Upn, userInfo.UserName),
                new(ClaimTypes.Email, userInfo.Eamil),
                new(ClaimTypes.Name, userInfo.DisplayName),
            };
            claims.AddRange(Roles.Select(role => new Claim(ClaimTypes.Role, role)));

            //Token
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("Your_Secert_Key_HerreYour_Secert_Key_Herre"));
            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: "Your_issuer_Herre",
                audience: "Your_audience_Herre",
                expires:DateTime.UtcNow.AddMinutes(30),
                claims: claims,
                signingCredentials: credentials
                );

            //return
            return _handler.WriteToken(token);
        }
    }
}
