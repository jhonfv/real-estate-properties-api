using AutoMapper;
using properties.Application.DTOs;
using properties.Application.DTOs.Properties;
using properties.Domain.Entities;
using properties.Domain.Filters;

namespace properties.Application.Mappers
{
    public class MapperProfiles : Profile
    {
        public MapperProfiles()
        {
            CreateMap<Owner, OwnerDTO>();
            CreateMap<OwnerDTO, Owner>();

            CreateMap<Property, PropertyDTO>();
            CreateMap<PropertyDTO, Property>();
            CreateMap<CreatePropertyDTO, Property>();
            CreateMap<PropertyImageDTO, PropertyImage>();
            CreateMap<PropertyImage,  PropertyImageDTO>();
            CreateMap<FilterPropertyDTO, FilterProperty>();
            CreateMap<FilterProperty, FilterPropertyDTO>();

        }
    }
}
