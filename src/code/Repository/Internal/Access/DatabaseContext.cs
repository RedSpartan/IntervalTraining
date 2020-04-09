using Microsoft.EntityFrameworkCore;
using RedSpartan.IntervalTraining.Repository.Internal.Data.Entities;

namespace RedSpartan.IntervalTraining.Internal.Repository.Access
{
    public class DatabaseContext : DbContext
    {
        private readonly string _databasePath;
        public DbSet<IntervalTemplate> Intervals { get; set; }
        public DbSet<History> Histories { get; set; }

        public DatabaseContext()
        {
            //This is just for migration
        }

        public DatabaseContext(string path)
        {
            _databasePath = path;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite($"Filename={_databasePath}");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            _ = modelBuilder.Entity<IntervalTemplate>(x =>
              {
                  x.Property(p => p.Id).ValueGeneratedOnAdd();

                  x.HasKey(p => p.Id);

                  x.Property(p => p.Name).IsRequired();

                  x.HasMany(p => p.History)
                      .WithOne(p => p.Template)
                      .HasForeignKey(p => p.TemplateId);
              });

            _ = modelBuilder.Entity<History>(x =>
              {
                  x.Property(p => p.Id).ValueGeneratedOnAdd();

                  x.HasKey(p => p.Id);
              });
        }
    }
}
