using AutoMapper;
using AutoMapper.QueryableExtensions;
using Core.Entities.Specialities;
using Data;
using Microsoft.EntityFrameworkCore;
using Services.Specialities.DTOs;

namespace Services.Specialities
{
    public class SpecialitiesService
    {
        AppDbContext _context;
        Mapper _mapper;

        public SpecialitiesService(AppDbContext dbContext, Mapper mapper)
        {
            _context = dbContext;
            _mapper = mapper;
        }

        public async Task AddAsync(SpecialityCreateDto dto)
        {
            var model = _mapper.Map<Speciality>(dto);

            await _context.Specialities.AddAsync(model);
            await _context.SaveChangesAsync();
        }

        public async Task Delete(int id)
        {
            _context.Specialities.Remove(new Speciality { Id = id });

            await _context.SaveChangesAsync();
        }

        public async Task<SpecialityDto?> Get(int id)
        {
            var model = await _context.Specialities.FindAsync(id);
            if (model == null)
                return null;
            var dto = _mapper.Map<SpecialityDto>(model);

            return dto;
        }

        public async Task<List<SpecialityDto>> Fetch()
        {
            var model = await _context.Specialities
                .ProjectTo<SpecialityDto>(_mapper.ConfigurationProvider)
                .ToListAsync();

            return model;
        }
    }
}
