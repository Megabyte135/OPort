using AutoMapper;
using Core.Entities.Portfolio;
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
        private readonly IWebHostEnvironment _env;

        public ProjectsService(AppDbContext dbContext, IMapper mapper, IWebHostEnvironment env)
        {
            _context = dbContext;
            _mapper = mapper;
            _env = env;
        }
        public async Task<ProjectDto> CreateProjectWithFileAsync(ProjectCreateDto dto, IFormFile file)
        {
            var project = _mapper.Map<Project>(dto);
            await _context.Projects.AddAsync(project);
            await _context.SaveChangesAsync();

            if (file != null && file.Length > 0)
            {
                var uploadFolder = Path.Combine(_env.ContentRootPath, "uploads");
                if (!Directory.Exists(uploadFolder))
                {
                    Directory.CreateDirectory(uploadFolder);
                }

                var fileExtension = Path.GetExtension(file.FileName);
                var newFileName = $"{Guid.NewGuid()}{fileExtension}";
                var filePath = Path.Combine(uploadFolder, newFileName);

                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    await file.CopyToAsync(fileStream);
                }

                var fileEntity = new Core.Entities.Projects.File
                {
                    Id = Guid.NewGuid(),
                    FilePath = filePath,
                    FileName = file.FileName
                };

                project.AttachedFiles.Add(fileEntity);

                await _context.Files.AddAsync(fileEntity);
                await _context.SaveChangesAsync();
            }
            return _mapper.Map<ProjectDto>(project);
        }

        public async Task<Guid> UploadFileAsync(Guid projectId, IFormFile formFile)
        {
            if (formFile == null || formFile.Length == 0)
            {
                throw new Exception("Пустой файл.");
            }

            var project = await _context.Projects
                .Include(x => x.AttachedFiles)
                .FirstOrDefaultAsync(x => x.Id == projectId);

            if (project == null)
            {
                throw new Exception($"Проект с Id={projectId} не найден");
            }

            var uploadFolder = Path.Combine(_env.WebRootPath, "uploads");
            if (!Directory.Exists(uploadFolder))
            {
                Directory.CreateDirectory(uploadFolder);
            }

            var fileExtension = Path.GetExtension(formFile.FileName);
            var newFileName = $"{Guid.NewGuid()}{fileExtension}";
            var filePath = Path.Combine(uploadFolder, newFileName);

            using (var fileStream = new FileStream(filePath, FileMode.Create))
            {
                await formFile.CopyToAsync(fileStream);
            }

            var fileEntity = new Core.Entities.Projects.File
            {
                Id = Guid.NewGuid(),
                FilePath = filePath,
                FileName = formFile.FileName
            };

            await _context.Files.AddAsync(fileEntity);

            project.AttachedFiles.Add(fileEntity);
            await _context.SaveChangesAsync();

            return fileEntity.Id;
        }

        public async Task UpdateProject(ProjectDto dto)
        {
            var project = await _context.Projects
                .Include(x => x.AttachedFiles)
                .FirstOrDefaultAsync(x => x.Id == dto.Id);

            if (project == null)
            {
                throw new Exception($"Проект с Id={dto.Id} не найден");
            }

            _mapper.Map(dto, project);

            var attachedFileIds = dto.AttachedFileIds?.ToHashSet() ?? new HashSet<Guid>();
            project.AttachedFiles = project.AttachedFiles
                .Where(f => attachedFileIds.Contains(f.Id))
                .ToList();

            await _context.SaveChangesAsync();
        }
        public async Task<ProjectDto> GetAsync(Guid id)
        {
            var project = await _context.Projects
                .Include(x => x.AttachedFiles)
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
                .Include(x => x.AttachedFiles)
                .Where(p => p.ResumeId == resumeId)
                .ToListAsync();

            return _mapper.Map<List<ProjectDto>>(projects);
        }

        public async Task<ProjectDto> UpdateAsync(Guid id, ProjectCreateDto dto)
        {
            var project = await _context.Projects
                .Include(x => x.AttachedFiles)
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
