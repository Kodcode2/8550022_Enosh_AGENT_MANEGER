using AgentsRest.Models;
using Microsoft.EntityFrameworkCore;

namespace AgentsRest.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<AgentModel> Agents { get; set; }
        public DbSet<TargetModel> Targets { get; set; }
        public DbSet<MissonModel> Missons { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<MissonModel>()
                .HasOne(a => a.Agent)
                .WithMany()
                .HasForeignKey(m => m.AgentId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<MissonModel>()
                .HasOne(a => a.Target)
                .WithMany()
                .HasForeignKey(m => m.TagetId)
                .OnDelete(DeleteBehavior.Restrict);


        }

    }
}
