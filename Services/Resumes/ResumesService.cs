using AutoMapper;
using AutoMapper.QueryableExtensions;
using Core.Entities.Portfolio;
using Data;
using Microsoft.EntityFrameworkCore;
using Services.Resumes.DTOs;

namespace Services.Resumes
{
    public class ResumesService
    {
        AppDbContext _context;
        Mapper _mapper;

        public ResumesService(AppDbContext dbContext, Mapper mapper)
        {
            _context = dbContext;
            _mapper = mapper;
        }


        public async Task Add(ResumeCreateDto dto)
        {
            var model = _mapper.Map<Resume>(dto);

            await _context.Resumes.AddAsync(model);
            await _context.SaveChangesAsync();
        }

        public async Task Delete(Guid id)
        {
            _context.Resumes.Remove(new Resume { Id = id });

            await _context.SaveChangesAsync();
        }

        public async Task Update(ResumeDto dto)
        {
            var model = _mapper.Map<Resume>(dto);

            _context.Resumes.Update(model);
            await _context.SaveChangesAsync();
        }

        public async Task<List<ResumeDto>> FetchByUserId(int id)
        {
            var result = await _context.Resumes.Where(r => r.UserId == id)
                .ProjectTo<ResumeDto>(_mapper.ConfigurationProvider)
                .ToListAsync();

            return result;
        }

        public async Task<ResumeDto> Get(Guid id)
        {
            var model = await _context.Resumes.FindAsync(id);
            var dto = _mapper.Map<ResumeDto>(model);

            return dto;
        }
    }
}
