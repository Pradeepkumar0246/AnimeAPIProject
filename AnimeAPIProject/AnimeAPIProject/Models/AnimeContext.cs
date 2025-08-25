using Microsoft.EntityFrameworkCore;

namespace AnimeAPIProject.Models
{
    public class AnimeContext : DbContext
    {
        public AnimeContext(DbContextOptions<AnimeContext> options) : base(options) { }

        public DbSet<Anime> Animes { get; set; }
        public DbSet<Genre> Genres { get; set; }
        public DbSet<Studio> Studios { get; set; }
        public DbSet<Users> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Users <-> Animes (Many-to-Many)
            modelBuilder.Entity<Users>()
                .HasMany(u => u.WatchedAnimes)
                .WithMany(a => a.Users)
                .UsingEntity(j => j.ToTable("UserWatchedAnimes"));

            // Anime <-> Genres (Many-to-Many)
            modelBuilder.Entity<Anime>()
                .HasMany(a => a.Genres)
                .WithMany(g => g.Animes)
                .UsingEntity(j => j.ToTable("AnimeGenres"));

            // Anime <-> Studio (One-to-Many)
            modelBuilder.Entity<Anime>()
                .HasOne(a => a.Studio)
                .WithMany(s => s.Animes)
                .HasForeignKey(a => a.Studio_Id);
        }
    }
}
