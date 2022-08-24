using HotelListingApi.Data;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace HotelListingApi.Data.Entities
{
  public class HotelConfiguration : IEntityTypeConfiguration<Hotel>
  {
    public void Configure(EntityTypeBuilder<Hotel> builder)
    {
      builder.HasData
      (
        new Hotel
        {
          HotelId = 1,
          HotelName = "SANDALS RESORT AND SPA",
          HotelAddress = "NEGRIL",
          CountryId = 1,
          HotelRating = 5.0,
        },
        new Hotel
        {
          HotelId = 2,
          HotelName = "COMFORT SUITE",
          HotelAddress = "GEORGE TOWN",
          CountryId = 3,
          HotelRating = 4.5
        },
        new Hotel
        {
          HotelId = 3,
          HotelName = "GRAND PALLDIUM",
          HotelAddress = "NASSUA",
          CountryId = 2,
          HotelRating = 3.0
        }
      );

    }
  }
}
