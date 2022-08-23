using HotelListingApi.Configurations;
using HotelListingApi.Data;
using HotelListingApi.IRepository;
using HotelListingApi.Repository;
using HotelListingApi.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HotelListingApi
{
  public class Startup
  {
    public Startup(IConfiguration configuration)
    {
      Configuration = configuration;
    }

    public IConfiguration Configuration { get; }

    // This method gets called by the runtime. Use this method to add services to the container.
    public void ConfigureServices(IServiceCollection services)
    {
      services.AddDbContext<DatabaseContext>(o =>
      {
        o.UseSqlServer(Configuration.GetConnectionString("HotelApi"));
      });
      services.AddAuthentication();
      services.ConfigureIdentity();
      services.ConfigureJWT(Configuration);
      services.AddCors(o =>
      {
        o.AddPolicy("AllowAll", builder =>
          builder.AllowAnyOrigin()
          .AllowAnyMethod()
          .AllowAnyHeader());
      });
      services.AddAutoMapper(typeof(MapperInitializers));
      services.AddTransient<IUnitOfWork, UnitOfWork>();
      services.AddScoped<IAuthManager, AuthManager>();
      AddSwaggerDoc(services);
      //services.AddSwaggerGen(c =>
      //{
      //  c.SwaggerDoc("v1", new OpenApiInfo { Title = "HotelListingApi", Version = "v1" });
      //});
      services.AddControllers().AddNewtonsoftJson(o =>
        o.SerializerSettings.ReferenceLoopHandling =  
          Newtonsoft.Json.ReferenceLoopHandling.Ignore);
    }

    private void AddSwaggerDoc(IServiceCollection services)
    {
      services.AddSwaggerGen(c =>
      {
        c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
        {
          Description=@"JWT Authorization header using the bearer scheme.
            Enter 'Bearer' [space] and then your token in the text input below.
            Example: 'Bearer 12345abcdef'",
           Name="Authorization",
           In=ParameterLocation.Header,
           Type=SecuritySchemeType.ApiKey,
           Scheme="Bearer"
        });
        c.AddSecurityRequirement(new OpenApiSecurityRequirement()
        {
          {
            new  OpenApiSecurityScheme
            {
              Reference=new OpenApiReference
              {
                Type=ReferenceType.SecurityScheme,
                Id="Bearer"
              },
              Scheme ="0auth2",
              Name="Bearer",
              In=ParameterLocation.Header
            },
            new List<string>()
          }
        });
        c.SwaggerDoc("v1", new OpenApiInfo { Title = "HotelListingApi", Version = "v1" });
      });
    }

    // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
      if(env.IsDevelopment())
      {
        app.UseDeveloperExceptionPage();
      }
      app.UseSwagger();
      app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "HotelListingApi v1"));
      app.ConfigureExceptionHandler();
      app.UseHttpsRedirection();
      app.UseCors("AllowAll");
      app.UseRouting();
      app.UseAuthentication();
      app.UseAuthorization();
      app.UseEndpoints(endpoints =>
      {
        endpoints.MapControllers();
      });
    }
  }
}
