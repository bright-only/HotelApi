using System.ComponentModel.DataAnnotations.Schema;

namespace HotelListingApi.Data
{
  public class Hotel
  {
    public int HotelId { get; set; }
    public string HotelName { get; set; }
    public string HotelAddress { get; set; }
    public double  HotelRating { get; set; }

    [ForeignKey(nameof(Country))]
    public int CountryId { get; set; }
    public Country Country { get; set; }
  }
}
