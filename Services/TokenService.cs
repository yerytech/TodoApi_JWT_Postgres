using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using TodoApi.Models;

namespace TodoApi.Services;

public static class TokenService
{
  public static string GenerateToken(string userName,int userId, string jwtKey)
  {
    var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtKey));
    var credential = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
    var claims = new[] {
      new Claim(ClaimTypes.Name,userName),
      new Claim("userId",userId.ToString()),
    };

    var token = new JwtSecurityToken(
      claims: claims,
      expires: DateTime.UtcNow.AddMinutes(20),
      signingCredentials: credential

    );

    return new JwtSecurityTokenHandler().WriteToken(token);
  }
}
