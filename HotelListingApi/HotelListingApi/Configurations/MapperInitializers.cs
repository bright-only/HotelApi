using AutoMapper;
using HotelListingApi.Data;
using HotelListingApi.Models;

namespace HotelListingApi.Configurations
{
  public class MapperInitializers : Profile
  {
    public MapperInitializers()
    {
      CreateMap<Country, CountryDTO>().ReverseMap();
      CreateMap<Country, CreateCountryDTO>().ReverseMap();
      CreateMap<Hotel, HotelDTO>().ReverseMap();
      CreateMap<Hotel, CreateHotelDTO>().ReverseMap();
      CreateMap<ApiUser, UserDTO>().ReverseMap();
    }
  }
}
