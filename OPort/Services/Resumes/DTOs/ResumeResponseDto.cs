using Services.Resumes.DTOs;

namespace API.Services.Resumes.DTOs
{
    public class ResumeResponseDto
    {
        public List<ResumeDto> Resumes { get; set; }
        public int? WorkExperience { get; set; }
        public string? Speciality { get; set; }
        public int ItemsPerPage { get; set; } = 10;
        public int PageNumber { get; set; } = 1;
        public int TotalPages { get; set; }
    }
}
