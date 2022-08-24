using HotelListingApi.Core.Models;
using HotelListingApi.Data;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Serilog;
using System;
using System.Reflection;
using System.Text;

namespace HotelListingApi.Core
{
  public static class ServiceExtensions
  {
    public static void ConfigureIdentity(this IServiceCollection services)
    {
      var builder = services.AddIdentityCore<ApiUser>(q => q.User.RequireUniqueEmail = true);
      builder = new IdentityBuilder(builder.UserType, typeof(IdentityRole), services);
      builder.AddEntityFrameworkStores<DatabaseContext>().AddDefaultTokenProviders();
    }
    public static void ConfigureJWT(this IServiceCollection services, IConfiguration configuration)
    {
      var jwtSetting = configuration.GetSection("JWTSETTING");
      var key = Environment.GetEnvironmentVariable("KEY");
      services.AddAuthentication(opt =>
      {
        opt.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
        opt.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        opt.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
      })
        .AddJwtBearer(o =>
        {
          o.TokenValidationParameters = new TokenValidationParameters
          {
            ValidateIssuer = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidateAudience=true,
            ValidIssuer = jwtSetting.GetSection("ISSUER").Value,
            ValidAudience = jwtSetting.GetSection("AUDIENCE").Value,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key))
          };
        });
    }

    public static void ConfigureAutoMapper(this IServiceCollection services)
    {
      services.AddAutoMapper(Assembly.GetExecutingAssembly());
    }


      public static void ConfigureExceptionHandler(this IApplicationBuilder app)
    {
      app.UseExceptionHandler(err =>
      {
        err.Run(async context =>
        {
          context.Response.StatusCode = StatusCodes.Status500InternalServerError;
          context.Response.ContentType="application/json";
          var contextFeature = context.Features.Get<IExceptionHandlerFeature>();
          if(contextFeature!=null)
          {
            Log.Error($"SOMETHING WENT WRONG {contextFeature.Error}");
            await context.Response.WriteAsync(new GlobalErrorException
            {
              StatusCode = context.Response.StatusCode,
              Message = "INTERNAL SERVER ERROR. PLEASE TRY AGAIN LATER."
            }.ToString());
          }
        });
      });
    }
  }
}
