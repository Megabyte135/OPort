using AutoMapper;
using AutoMapper.QueryableExtensions;
using Core.Entities.Locations;
using Services.Locations.DTOs;
using Data;

namespace Services.Locations
{
    public class LocationsService
    {
        AppDbContext _context;
        Mapper _mapper;

        public LocationsService(AppDbContext dbContext, Mapper mapper)
        {
            _context = dbContext;
            _mapper = mapper;
        }

        public async Task Add(LocationCreateDto dto)
        {
            var model = _mapper.Map<Location>(dto);

            await _context.Locations.AddAsync(model);
        }

        public void Delete(Guid id)
        {
            _context.Locations.Remove(new Location { Id = id });
        }

        public async Task<List<LocationDto>> Fetch()
        {
            var result = await _context.Roles.ProjectTo<LocationDto>(_mapper.ConfigurationProvider).ToListAsync();

            return result;
        }
    }
}
