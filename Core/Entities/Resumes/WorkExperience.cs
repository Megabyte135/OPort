using Core.Entities.Portfolio;

namespace Core.Entities.Resumes
{
    public class WorkExperience : BaseEntity<Guid>
    {
        public string Name { get; set; }
        public string Employer { get; set; }
        public string Responsibilities { get; set; }
        public string Description { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public Guid ResumeId { get; set; }
        public virtual Resume Resume { get; set; }
    }
}
