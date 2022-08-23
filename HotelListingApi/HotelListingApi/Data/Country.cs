using System.Collections.Generic;

namespace HotelListingApi.Data
{
  public class Country
  {
    public int CountryId { get; set; }
    public string CountryName { get; set; }
    public string CountryShortName { get; set; }
    public virtual IList<Hotel> Hotels { get; set; }//migration is not needed
  }
}
