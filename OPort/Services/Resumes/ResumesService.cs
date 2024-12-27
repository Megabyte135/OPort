using API.Services.Resumes.DTOs;
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
            model.SocialMedias = dto.SocialMedia.Select(s => new Core.Entities.Resumes.SocialMedia { Name = s.Key, Value = s.Value }).ToList();

            await _context.Resumes.AddAsync(model);
            var speciality = await _context.Specialities.FirstOrDefaultAsync(s => s.Name == dto.Speciality);
            if (speciality != null)
                model.Speciality = speciality;
            model.WorkExperienceId = model.WorkExperience.Id;
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
            model.SocialMedias = dto.SocialMedia.Select(s => new Core.Entities.Resumes.SocialMedia { Name = s.Key, Value = s.Value }).ToList();
            _context.Resumes.Update(model);
            var workExperience = await _context.WorkExperiences.FirstOrDefaultAsync(w => w.ResumeId == dto.Id);
            var speciality = await _context.Specialities.FirstOrDefaultAsync(s => s.Name == dto.Speciality);
            if (speciality != null)
                model.Speciality = speciality;
            model.WorkExperience.Id = workExperience.Id;
            model.WorkExperienceId = workExperience.Id;
            await _context.SaveChangesAsync();
        }

        public async Task<List<ResumeDto>> FetchByUserId(string id)
        {
            var result = await _context.Resumes.Where(r => r.UserId == id)
                .Include(r => r.SocialMedias)
                .Include(r => r.WorkExperience)
                .ProjectTo<ResumeDto>(_mapper.ConfigurationProvider)
                .ToListAsync();

            return result;
        }

        public async Task<ResumeResponseDto> Fetch(ResumeRequestDto request)
        {
            var query = _context.Resumes
                .Where(r => (r.Speciality.Name == request.Speciality || request.Speciality == null)
                    && (EF.Functions.DateDiffYear(r.WorkExperience.StartDate, r.WorkExperience.EndDate) >= request.WorkExperience || request.WorkExperience == null))
                .Include(r => r.SocialMedias)
                .Include(r => r.WorkExperience)
                .Skip((request.PageNumber - 1) * request.ItemsPerPage)
                .Take(request.ItemsPerPage);

            var list = await query
                .ProjectTo<ResumeDto>(_mapper.ConfigurationProvider)
                .ToListAsync();

            int totalPages = ((list.Count()-1) / request.ItemsPerPage)+1;

            var result = new ResumeResponseDto { Resumes = list, ItemsPerPage = request.ItemsPerPage,
            TotalPages = totalPages, PageNumber = request.PageNumber, Speciality = request.Speciality, WorkExperience = request.WorkExperience};

            return result;
        }


        public async Task<ResumeDto> Get(Guid id)
        {
            var model = await _context.Resumes
                .Include(r => r.SocialMedias)
                .Include(r => r.WorkExperience)
                .FirstOrDefaultAsync(r => r.Id == id);
            var dto = _mapper.Map<ResumeDto>(model);

            return dto;
        }
    }
}
