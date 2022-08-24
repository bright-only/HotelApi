using HotelListingApi.Core.DTOs;
using System.Threading.Tasks;

namespace HotelListingApi.Core.Services
{
  public interface IAuthManager
  {
    Task<bool> ValidateUser(LoginUserDTO userDTO);
    Task<string> CreateToken();
  }
}
