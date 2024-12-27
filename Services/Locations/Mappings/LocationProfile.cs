using AutoMapper;
using Core.Entities.Locations;
using Services.Locations.DTOs;

namespace Services.Locations.Mappings
{
    public class LocationProfile : Profile
    {
        public LocationProfile()
        {
            CreateMap<Location, LocationDto>()
                .ForMember(dest => dest.ParentLocationId, opt => opt.MapFrom(src => src.ParentLocationId.HasValue ? src.ParentLocationId.ToString() : null))
                .ForMember(dest => dest.ChildrenLocations, opt => opt.MapFrom(src => src.ChildrenLocations != null ? src.ChildrenLocations.Select(c => c.Id).ToList() : new List<Guid>()));

            CreateMap<LocationDto, Location>()
                .ForMember(dest => dest.ParentLocationId, opt => opt.MapFrom(src => src.ParentLocationId))
                .ForMember(dest => dest.ChildrenLocations, opt => opt.MapFrom(src => src.ChildrenLocations != null ? src.ChildrenLocations.Select(c => new Location { Id = c}).ToList() : new List<Location>()));

            CreateMap<LocationCreateDto, Location>()
                .ForMember(dest => dest.ParentLocationId, opt => opt.MapFrom(src => src.ParentLocationId))
                .ForMember(dest => dest.ChildrenLocations, opt => opt.MapFrom(src => src.ChildrenLocations != null ? src.ChildrenLocations.Select(c => new Location { Id = c }).ToList() : new List<Location>()));
        }
    }
}
