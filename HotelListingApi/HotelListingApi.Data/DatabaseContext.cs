using HotelListingApi.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace HotelListingApi.Data
{
  public class DatabaseContext  : IdentityDbContext<ApiUser>
  {
    public DatabaseContext(DbContextOptions options) : base(options)
    {}
    public DbSet<Country> Countries { get; set; }
    public DbSet<Hotel> Hotels { get; set; }

    protected override void OnModelCreating(ModelBuilder builder)
    {
      base.OnModelCreating(builder);  
      builder.ApplyConfiguration(new CountryConfiguration());
      builder.ApplyConfiguration(new HotelConfiguration());
      builder.ApplyConfiguration(new RoleConfiguration());
      //builder.Entity<Country>().HasData(
      //   new Country
      //   {
      //     CountryId = 1,
      //     CountryName = "JAMAICA",
      //     CountryShortName = "JM"
      //   },
      //    new Country
      //    {
      //      CountryId = 2,
      //      CountryName = "BAHAMAS",
      //      CountryShortName = "BS"
      //    },
      //    new Country
      //    {
      //      CountryId = 3,
      //      CountryName = "CAYMAN ISLAND",
      //      CountryShortName = "CI"
      //    }
      //  );
    }
  }
  
}
