using AutoMapper;
using HotelListingApi.Core.DTOs;
using HotelListingApi.Data;

namespace HotelListingApi.Core.Configurations
{
  public class MapperInitializers : Profile
  {
    public MapperInitializers()
    {
      CreateMap<Country, CountryDTO>().ReverseMap();
      CreateMap<Country, CreateCountryDTO>().ReverseMap();
      CreateMap<Country, UpdateCountryDTO>().ReverseMap();
      CreateMap<Hotel, HotelDTO>().ReverseMap();
      CreateMap<Hotel, CreateHotelDTO>().ReverseMap();
      CreateMap<Hotel, UpdateHotelDTO>().ReverseMap();
      CreateMap<ApiUser, UserDTO>().ReverseMap();
    }
  }
}
