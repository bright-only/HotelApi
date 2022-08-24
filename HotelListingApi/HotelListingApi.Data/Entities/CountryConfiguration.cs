using HotelListingApi.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HotelListingApi.Data.Entities
{
  public class CountryConfiguration : IEntityTypeConfiguration<Country>
  {
    public void Configure(EntityTypeBuilder<Country> builder)
    {
      builder.HasData
      (
        new Country
        {
          CountryId=1,
          CountryName="JAMAICA",
          CountryShortName="JM"
        },
        new Country
        {
          CountryId = 2,
          CountryName = "BAHAMAS",
          CountryShortName = "BS"
        },
        new Country
        {
          CountryId = 3,
          CountryName = "CAYMAN ISLAND",
          CountryShortName = "CI"
        }
      );
      
    }
  }
}
