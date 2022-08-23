using HotelListingApi.Models;
using System.Threading.Tasks;

namespace HotelListingApi.Services
{
  public interface IAuthManager
  {
    Task<bool> ValidateUser(LoginUserDTO userDTO);
    Task<string> CreateToken();
  }
}
