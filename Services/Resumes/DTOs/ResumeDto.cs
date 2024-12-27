namespace Services.Resumes.DTOs
{
    public class ResumeDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Speciality { get; set; }
        public Guid UserId { get; set; }
        public Dictionary<string, string> SocialMedia { get; set; }
        public string ExperienceName { get; set; }
        public string ExperienceEmployer { get; set; }
        public string ExperienceResponsibilities { get; set; }
        public string ExperienceDescription { get; set; }
        public DateTime ExperienceStartDate { get; set; }
        public DateTime? ExperienceEndDate { get; set; }
    }
}
