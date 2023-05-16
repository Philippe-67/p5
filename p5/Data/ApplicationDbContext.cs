using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using p5.Models;

namespace p5.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public DbSet<Voiture> Voiture { get; set; } = default!;
        public DbSet<Reparation> Reparation { get; set; } = default!;
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
           base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Reparation>()
                .HasOne(v => v.Voiture)
                .WithMany(v => v.Reparations)
                .HasForeignKey(v => v.VoitureId);
        }
    }
}
