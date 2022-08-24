using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace HotelListingApi.Core.DTOs
{
  public class UserDTO : LoginUserDTO
  {
    [Required]
    public string FirstName { get; set; }
    [Required]
    public string LastName { get; set; }
    [Required]
    [DataType(DataType.PhoneNumber)]
    public string PhoneNumber { get; set; }
    public ICollection<string> Roles { get; set; }

  }
  public class LoginUserDTO
  {
    [Required]
    [DataType(DataType.EmailAddress)]
    public string Email { get; set; }
    [Required]
    [DataType(DataType.Password)]
    public string Password { get; set; }
  }
}
