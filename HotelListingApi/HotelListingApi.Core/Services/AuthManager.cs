using HotelListingApi.Core.DTOs;
using HotelListingApi.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace HotelListingApi.Core.Services
{
  public class AuthManager : IAuthManager
  {
    private readonly UserManager<ApiUser> _userManager;
    private readonly IConfiguration _configuration;
    private ApiUser _user;
    public AuthManager(UserManager<ApiUser> userManager, IConfiguration configuration)
    {
      _userManager = userManager;
      _configuration = configuration;
    }
    public async Task<string> CreateToken()
    {
      var signingCredentials=GetSigningCredentials();
      var claims =await GetClaims();
      var token = GenerateTokenOptions(signingCredentials, claims);
      return new JwtSecurityTokenHandler().WriteToken(token);
    }

    private JwtSecurityToken GenerateTokenOptions(SigningCredentials signingCredentials, List<Claim> claims)
    {
      var jwtSetting = _configuration.GetSection("JWTSETTING");
      var token = new JwtSecurityToken(
       issuer: jwtSetting.GetSection("ISSUER").Value,
       audience: jwtSetting.GetSection("AUDIENCE").Value,
       claims: claims,
       expires: DateTime.Now.AddMinutes(30) ,
       signingCredentials : signingCredentials
        ) ;
        return token; 
    }

    private async Task<List<Claim>> GetClaims()
    {
      var claims = new List<Claim>
      {
        new Claim(ClaimTypes.Name, _user.UserName)
      };
      var roles = await _userManager.GetRolesAsync(_user);
      foreach(var role in roles)
      {
        claims.Add(new Claim(ClaimTypes.Role, role));
      }
      return claims;
    }

    private SigningCredentials GetSigningCredentials()
    {
      var key = Environment.GetEnvironmentVariable("KEY");
      var secret = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key));
      return new SigningCredentials(secret, SecurityAlgorithms.HmacSha256);
    }

    public async Task<bool> ValidateUser(LoginUserDTO userDTO)
    {
      _user = await _userManager.FindByNameAsync(userDTO.Email);
      return (_user != null && await _userManager.CheckPasswordAsync(_user, userDTO.Password));
    }
  }
}
