using Core.Entities.Portfolio;
using Core.Entities.Projects;
using Core.Entities.Resumes;
using Core.Entities.Roles;
using Core.Entities.Specialities;
using Core.Entities.Users;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Data
{
    public class AppDbContext : IdentityDbContext<User, Role, string>
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Resume>()
                .HasOne(r => r.WorkExperience)
                .WithOne(e => e.Resume)
                .HasForeignKey<WorkExperience>(e => e.ResumeId);
        }

        public DbSet<Project> Projects { get; set; }
        public DbSet<Resume> Resumes { get; set; }
        public DbSet<Speciality> Specialities { get; set; }
        public DbSet<WorkExperience> WorkExperiences { get; set; }
        public DbSet<Core.Entities.Projects.File> Files { get; set; }
    }
}
