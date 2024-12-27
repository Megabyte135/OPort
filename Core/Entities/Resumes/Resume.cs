using Core.Entities.Resumes;
using Core.Entities.Specialities;
using Core.Entities.Users;

namespace Core.Entities.Portfolio
{
    public class Resume : BaseEntity<Guid>
    {
        public string Name { get; set; }
        public Speciality Speciality { get; set; }
        public string UserId { get; set; }
        public string Location { get; set; }
        public Guid WorkExperienceId { get; set; }
        public virtual WorkExperience WorkExperience { get; set; }
        public virtual User User { get; set; }
        public virtual List<SocialMedia> SocialMedias { get; set; }
    }
}
