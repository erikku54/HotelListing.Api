using AutoMapper;
using HotelListing.Api.Data;
using HotelListing.Api.Models.Country;
using HotelListing.Api.Models.Hotel;

namespace HotelListing.Api.Configurations;

public class MapperConfig : Profile
{
    public MapperConfig()
    {
        CreateMap<Country, CreateCountryDto>().ReverseMap();
        CreateMap<Country, GetCountryDto>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.CountryId))
            .ReverseMap()
            .ForMember(dest => dest.CountryId, opt => opt.MapFrom(src => src.Id));

        CreateMap<Country, UpdateCountryDto>().ReverseMap();
        CreateMap<Country, CountryDto>().ReverseMap();

        CreateMap<Hotel, HotelDto>().ReverseMap();
    }
}
