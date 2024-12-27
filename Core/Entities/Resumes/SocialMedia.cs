using Core.Entities.Portfolio;

namespace Core.Entities.Resumes
{
    public class SocialMedia : BaseEntity<Guid>
    {
        public string Name { get; set; }
        public string Value { get; set; }
        public Guid ResumeId { get; set; }
        public virtual Resume Resume { get; set; }
    }
}
