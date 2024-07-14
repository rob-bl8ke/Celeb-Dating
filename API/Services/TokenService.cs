using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using API.Entities;
using API.Interfaces;
using Microsoft.IdentityModel.Tokens;

namespace API.Services;

public class TokenService(IConfiguration config) : ITokenService
{
    public string CreateToken(AppUser user)
    {
        var tokenKey = config["TokenKey"] ?? throw new Exception("Cannot access the TokenKey from appsettings");
        if (tokenKey.Length < 64) throw new Exception("Your token key needs to be longer");
        // One key to rule them all (encrypt an decrypt)
        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(tokenKey));

        
        // Claims this user has about themself.. "I claim to be..."
        var claims = new List<Claim>
        {
            new(ClaimTypes.NameIdentifier, user.UserName)
        };

        // Specify the algorithm to encrypt the key
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

        // Describe the other elements of our token
        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(claims),
            Expires = DateTime.UtcNow.AddDays(7),
            SigningCredentials = creds
        };
        
        // Create and return the token string
        var tokenHandler = new JwtSecurityTokenHandler();
        var token = tokenHandler.CreateToken(tokenDescriptor);

        return tokenHandler.WriteToken(token);
    }
}
