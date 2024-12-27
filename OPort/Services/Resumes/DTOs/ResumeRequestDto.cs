namespace API.Services.Resumes.DTOs
{
    public class ResumeRequestDto
    {
        public int? WorkExperience { get; set; }
        public string? Speciality { get; set; }
        public int ItemsPerPage { get; set; } = 10;
        public int PageNumber { get; set; } = 1;
    }
}
