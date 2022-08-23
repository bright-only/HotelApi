using Newtonsoft.Json;

namespace HotelListingApi.Models
{
  public class GlobalErrorException
  {
    public int StatusCode { get; set; }
    public string Message { get; set; }
    public override string ToString() => JsonConvert.SerializeObject(this);
  }
}
