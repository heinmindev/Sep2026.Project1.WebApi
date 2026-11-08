using Microsoft.EntityFrameworkCore;

namespace Sep2026.Project1.WebApi.AppDbContextModels
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<Project1Answer> Project1Answers { get; set; }

        public DbSet<Project1Number> Project1Numbers { get; set; }

        public DbSet<Project1Question> Project1Questions { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Project1Answer>(entity =>
            {
                entity.ToTable("Project1Answer");
                entity.HasKey(x => x.AnswerId);
            });

            modelBuilder.Entity<Project1Number>(entity =>
            {
                entity.ToTable("Project1Number");
                entity.HasKey(x => x.NumberId);
            });

            modelBuilder.Entity<Project1Question>(entity =>
            {
                entity.ToTable("Project1Question");
                entity.HasKey(x => x.QuestionId);
                entity.Property(x => x.QuestionId).ValueGeneratedNever();
            });
        }
    }
}
