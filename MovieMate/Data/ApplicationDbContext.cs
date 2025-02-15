using Microsoft.AspNetCore.Components.Server.ProtectedBrowserStorage;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using MovieMate.Models;

namespace MovieMate.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) 
            : base(options)
        {
        }

        public DbSet<Media> Media { get; set; }
        public DbSet<PlanToWatch> PlanToWatch { get; set; }
        public DbSet<Watching> Watching { get; set; }
        public DbSet<Watched> Watched { get; set; }
        public DbSet<Favourite> Favourites { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // PlanToWatch many-to-many relationship with User and Media
            modelBuilder.Entity<PlanToWatch>()
                .HasKey(e => new {e.UserId, e.PlanId});

            modelBuilder.Entity<PlanToWatch>()
                .HasOne(p => p.User)
                .WithMany(u => u.PlanToWatch)
                .HasForeignKey(p => p.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<PlanToWatch>()
                .HasOne(p => p.Media)
                .WithMany(m => m.PlanToWatch)
                .HasForeignKey(p => p.PlanId)
                .OnDelete(DeleteBehavior.Cascade);


            //Watching many-to-many relationship with User and Media
            modelBuilder.Entity<Watching>()
                .HasKey(e => new { e.UserId, e.WatchingId });

            modelBuilder.Entity<Watching>()
                .HasOne(w => w.User)
                .WithMany(u => u.Watching)
                .HasForeignKey(w => w.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Watching>()
                .HasOne(w => w.Media)
                .WithMany(m => m.Watching)
                .HasForeignKey(w => w.WatchingId)
                .OnDelete(DeleteBehavior.Cascade);


            //Watched many-to-many relationship with User and Media
            modelBuilder.Entity<Watched>()
                .HasKey(e => new {e.UserId, e.WatchedId});

            modelBuilder.Entity<Watched>()
                .HasOne(w => w.User)
                .WithMany(u => u.Watched)
                .HasForeignKey(w => w.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Watched>()
                .HasOne(w => w.Media)
                .WithMany(m => m.Watched)
                .HasForeignKey(w => w.WatchedId)
                .OnDelete(DeleteBehavior.Cascade);


            //Favourites many-to-many relationships with User and Media
            modelBuilder.Entity<Favourite>()
                .HasKey(e => new { e.UserId, e.FavouriteId });

            modelBuilder.Entity<Favourite>()
                .HasOne(f => f.User)
                .WithMany(u => u.Favourites)
                .HasForeignKey(f => f.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Favourite>()
                .HasOne(f => f.Media)
                .WithMany(m => m.Favourites)
                .HasForeignKey(f => f.FavouriteId)
                .OnDelete(DeleteBehavior.Cascade);

        }
    }
}
