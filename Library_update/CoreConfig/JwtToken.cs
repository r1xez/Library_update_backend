using Library_update.Models;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Library_update.CoreConfig
{
    public  sealed class JwtToken
    {
      
        

            private readonly IConfiguration _config;

            public JwtToken(IConfiguration config)

            {

                _config = config;

            }

            public string Create(UserRecord user)

            {

                var jwt = _config.GetSection("Jwt");

                var key = jwt["Key"];

                var issuer = jwt["Issuer"];

                var audience = jwt["Audience"];

                var expiresMinutes = int.Parse(jwt["ExpiresInMinutes"]);

                var claims = new List<Claim>

        {

            new Claim(ClaimTypes.NameIdentifier, user.Id),

            new Claim(ClaimTypes.Email, user.Email),

            new Claim(ClaimTypes.Role, user.Role)

        };

                var signingKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key));

                var creds = new SigningCredentials(signingKey, SecurityAlgorithms.HmacSha256);

                var token = new JwtSecurityToken(

                    issuer: issuer,

                    audience: audience,

                    claims: claims,

                    expires: DateTime.UtcNow.AddMinutes(expiresMinutes),

                    signingCredentials: creds);

                return new JwtSecurityTokenHandler().WriteToken(token);

            }

        }
    }
