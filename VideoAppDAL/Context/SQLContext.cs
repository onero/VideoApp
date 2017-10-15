using System;
using System.IO;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using VideoAppDAL.Entities;
using VideoAppDAL.Interfaces;

namespace VideoAppDAL.Context
{
    public sealed class SQLContext : DbContext, IVideoContext
    {

        //private static readonly string DBConnectionPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "DBConnection.txt");
        //private static readonly string ConnectionString = File.ReadAllText(DBConnectionPath);

        public static string ConnectionString = "";
        public SQLContext()
        {
            Database.EnsureCreated();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(ConnectionString);
            }
            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {// AddressId + CustomerId = PK
            modelBuilder.Entity<VideoGenre>()
                .HasKey(vg => new { vg.GenreId, vg.VideoId });

            /* One address in CustomerAddress is one address from Address table,
             * however that one address can have many customers,
             * the address links to addressId as foreign key
             * */
            modelBuilder.Entity<VideoGenre>()
                .HasOne(vg => vg.Genre)
                .WithMany(g => g.Videos)
                .HasForeignKey(vg => vg.GenreId);

            /* One customer in CustomerAddress is one customer from Customer table,
             * however that one customer can have many addresses,
             * the customer links to customerId as foreign key
            */
            modelBuilder.Entity<VideoGenre>()
                .HasOne(vg => vg.Video)
                .WithMany(v => v.Genres)
                .HasForeignKey(vg => vg.VideoId);

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