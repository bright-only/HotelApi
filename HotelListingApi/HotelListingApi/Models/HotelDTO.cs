using HotelListingApi.Data;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HotelListingApi.Models
{
  public class HotelDTO : CreateHotelDTO
  {
    public int HotelId { get; set; }
    public CountryDTO Country { get; set; } 

  }
  public class CreateHotelDTO
  {
    [Required]
    [StringLength(maximumLength: 50, ErrorMessage = "HOTEL NAME IS GREATER 50")]
    public string HotelName { get; set; }


    [Required]
    [Range(1,    5, ErrorMessage = "RATING MUST BE TETWEEN 1 TO 5")]
    public double HotelRating { get; set; } 


    public int CountryId { get; set; }


    [Required]
    [StringLength(maximumLength: 100, ErrorMessage = "HOTEL ADDRESS IS GREATER 100")]
    public string HotelAddress { get; set; }
  }

  public class UpdateHotelDTO : CreateHotelDTO
  {

  }

}
