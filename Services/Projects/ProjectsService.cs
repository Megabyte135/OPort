using AutoMapper;
using Core.Entities.Projects;
using Data;
using Microsoft.EntityFrameworkCore;
using Services.Projects.DTOs;

namespace Services.Projects
{
    public class ProjectsService
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;

        public ProjectsService(AppDbContext dbContext, IMapper mapper)
        {
            _context = dbContext;
            _mapper = mapper;
        }

        public async Task AddAsync(ProjectCreateDto dto)
        {
            var project = _mapper.Map<Project>(dto);

            await _context.Projects.AddAsync(project);
            await _context.SaveChangesAsync();
        }

        public async Task AddPageToProjectAsync(ProjectPageCreateDto dto)
        {
            var page = _mapper.Map<ProjectPage>(dto);

            await _context.Pages.AddAsync(page);
            await _context.SaveChangesAsync();
        }

        public async Task DeletePageFromProject(Guid id)
        {
            var page = await _context.Pages.FindAsync(id);
            if (page == null)
            {
                throw new Exception($"Страница проекта с Id={id} не найдена");
            }

            _context.Pages.Remove(page);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateProjectPage(ProjectPageDto dto)
        {
            var page = await _context.Pages.FindAsync(dto.Id);
            if (page == null)
            {
                throw new Exception($"Страница проекта с Id={dto.Id} не найдена");
            }

            _mapper.Map(dto, page);

            _context.Pages.Update(page);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateProject(ProjectDto dto)
        {
            var project = await _context.Projects
                .Include(p => p.AttachedFiles)
                .FirstOrDefaultAsync(x => x.Id == dto.Id);

            if (project == null)
            {
                throw new Exception($"Проект с Id={dto.Id} не найден");
            }

            _mapper.Map(dto, project);

            _context.Projects.Update(project);
            await _context.SaveChangesAsync();
        }

        public async Task<ProjectDto> GetAsync(Guid id)
        {
            var project = await _context.Projects
                .Include(p => p.AttachedFiles)
                .FirstOrDefaultAsync(x => x.Id == id);

            if (project == null)
            {
                throw new Exception($"Проект с Id={id} не найден");
            }

            return _mapper.Map<ProjectDto>(project);
        }

        public async Task<List<ProjectDto>> FetchByResumeIdAsync(Guid resumeId)
        {
            var projects = await _context.Projects
                .Include(p => p.AttachedFiles)
                .Where(p => p.ResumeId == resumeId)
                .ToListAsync();

            return _mapper.Map<List<ProjectDto>>(projects);
        }

        public async Task<ProjectDto> UpdateAsync(Guid id, ProjectCreateDto dto)
        {
            var project = await _context.Projects
                .Include(p => p.AttachedFiles)
                .FirstOrDefaultAsync(p => p.Id == id);

            if (project == null)
            {
                throw new Exception($"Проект с Id={id} не найден");
            }

            _mapper.Map(dto, project);

            await _context.SaveChangesAsync();

            return _mapper.Map<ProjectDto>(project);
        }

        public async Task DeleteAsync(Guid id)
        {
            var project = await _context.Projects.FindAsync(id);

            if (project == null)
            {
                throw new Exception($"Проект с Id={id} не найден");
            }

            _context.Projects.Remove(project);
            await _context.SaveChangesAsync();
        }
    }
}
