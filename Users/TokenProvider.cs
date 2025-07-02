using Microsoft.IdentityModel.JsonWebTokens;
using Microsoft.IdentityModel.Tokens;
using SportEdge.API.Models.Domain;
using System.Security.Claims;
using System.Text;

namespace SportEdge.API.Users
{
    /// <summary>
    /// Service for creating JWT for user authentication and authorization.
    /// </summary>
    public class TokenProvider (IConfiguration configuration)
    {


        /// <summary>
        /// Creates a JWT for the specified user.
        /// </summary>
        /// <param name="user">The user for whom the token is being created.</param>
        /// <returns>A signed JWT as a string.</returns>
        public string Create (User user)
        {
            string secretKey = configuration["Jwt:Secret"];
            var securityKey = new SymmetricSecurityKey (Encoding.UTF8.GetBytes (secretKey));

            var credentials = new SigningCredentials (securityKey,SecurityAlgorithms.HmacSha256);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(
                    [
                        new Claim(JwtRegisteredClaimNames.Sub, user.Id.ToString ()),
                        new Claim (JwtRegisteredClaimNames.Email,user.Email),
                        new Claim(ClaimTypes.Role, user.IsAdmin ? "Admin" : "User")
                    ]),
                Expires = DateTime.UtcNow.AddMinutes(configuration.GetValue<int>("Jwt:ExpirationInMinutes")),
                SigningCredentials = credentials,
                Issuer = configuration["Jwt:Issuer"],
                Audience = configuration["Jwt:Audience"]
            };

            var handler = new JsonWebTokenHandler();

            string token = handler.CreateToken(tokenDescriptor);

            return token;
        }
    }
}
