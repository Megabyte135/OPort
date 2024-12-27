using Core.Entities.Portfolio;

namespace Core.Entities.Projects
{
    public class Project : BaseEntity<Guid>
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string ClientName { get; set; }
        public DateTime CompletedDate { get; set; }
        public Guid ResumeId { get; set; }
        public virtual Resume Resume { get; set; }
        public virtual ICollection<File> AttachedFiles { get; set; } = new List<File>();
    }
}
