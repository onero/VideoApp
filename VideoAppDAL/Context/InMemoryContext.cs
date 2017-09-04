﻿using Microsoft.EntityFrameworkCore;
using VideoAppDAL.Entities;
using VideoAppDAL.Interfaces;

namespace VideoAppDAL.Context
{
    internal class InMemoryContext : DbContext
    {
        //In memory setup
        private static readonly DbContextOptions<InMemoryContext> Options =
            new DbContextOptionsBuilder<InMemoryContext>().UseInMemoryDatabase("VideoDB").Options;

        //Options that we want in memory
        public InMemoryContext() : base(Options)
        {
        }

        public DbSet<Video> Videos { set; get; }
        public DbSet<Profile> Profiles { get; set; }
    }
}