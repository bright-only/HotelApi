using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HotelListingApi
{
  public class Program
  {
    public static void Main(string[] args)
    {
      Log.Logger = new LoggerConfiguration()
        .WriteTo.File
        (
          path: "c:\\HotelApiLog\\Logs\\.txt",
          outputTemplate: "{Timestamp:yyyy-MM-dd HH:mm:ss.fff zzz} [{Level:u3}] {Message:1j} {NewLine} {Exception}",
          rollingInterval: RollingInterval.Day,
          restrictedToMinimumLevel: Serilog.Events.LogEventLevel.Information
        )
        .CreateLogger();
      try
      {
        Log.Information("APPLICATION IS STARTING");
        CreateHostBuilder(args).Build().Run();
      }
      catch(Exception ex)
      {
        Log.Fatal(ex, "APPLICATION FAILED TO START");
      }
      finally
      {
        Log.CloseAndFlush();
      }
      
    }

    public static IHostBuilder CreateHostBuilder(string[] args) =>
        Host.CreateDefaultBuilder(args)
      .UseSerilog()
            .ConfigureWebHostDefaults(webBuilder =>
            {
              webBuilder.UseStartup<Startup>();
            });
  }
}
