using HotelListingApi.Data;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;

namespace HotelListingApi.Entities
{
  public class RoleConfiguration : IEntityTypeConfiguration<IdentityRole>
  {
    public void Configure(EntityTypeBuilder<IdentityRole> builder)
    {
      {
        builder.HasData
          (
            new IdentityRole
            {
              Name = "Supervisor",
              NormalizedName = "SUPERVISOR"
            },
            new IdentityRole
            {
              Name = "Administrator",
              NormalizedName = "ADMINISTRATOR"
            },
            new IdentityRole
            {
              Name = "User",
              NormalizedName = "USER"
            }
          );
      }
    }
  }
}
