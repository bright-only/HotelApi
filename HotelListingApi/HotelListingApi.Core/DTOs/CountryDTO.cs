using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace HotelListingApi.Core.DTOs
{
  public class CountryDTO : CreateCountryDTO
  {
    public int CountryId { get; set; }
    public IList<HotelDTO> Hotels { get; set; }
  }

  public class CreateCountryDTO
  {
    [Required]
    [StringLength(maximumLength: 2, MinimumLength = 2, ErrorMessage = "COUNTRY SHORT NAME IS GREATER 2")]
    public string CountryShortName { get; set; }
    [Required]
    [StringLength(maximumLength: 50, ErrorMessage = "COUNTRY NAME IS GREATER 50")]
    public string CountryName { get; set; }
  }

  public class UpdateCountryDTO : CreateCountryDTO
  {
    public IList<CreateHotelDTO> Hotels { get; set; }
  }
}
