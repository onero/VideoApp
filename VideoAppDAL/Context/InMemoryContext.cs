using Microsoft.EntityFrameworkCore;
using VideoAppDAL.Entities;
using VideoAppDAL.Interfaces;

namespace VideoAppDAL.Context
{
    public class InMemoryContext : DbContext, IVideoContext
    {
        //Options that we want in memory
        public InMemoryContext(DbContextOptions<InMemoryContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // AddressId + CustomerId = PK
            modelBuilder.Entity<VideoGenre>()
                .HasKey(vg => new {vg.GenreId, vg.VideoId});

            /* One address in CustomerAddress is one address from Address table,
             * however that one address can have many customers,
             * the address links to addressId as foreign key
             * */
            modelBuilder.Entity<VideoGenre>()
                .HasOne(vg => vg.Video)
                .WithMany(g => g.Genres)
                .HasForeignKey(vg => vg.GenreId);

            /* One customer in CustomerAddress is one customer from Customer table,
             * however that one customer can have many addresses,
             * the customer links to customerId as foreign key
            */
            modelBuilder.Entity<VideoGenre>()
                .HasOne(vg => vg.Genre)
                .WithMany(v => v.Videos)
                .HasForeignKey(vg => vg.GenreId);
            base.OnModelCreating(modelBuilder);
        }

        public DbSet<Video> Videos { get; set; }
        public DbSet<Profile> Profiles { get; set; }
        public DbSet<Rental> Rentals { get; set; }
        public DbSet<Genre> Genres { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
    }
}