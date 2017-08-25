using Microsoft.EntityFrameworkCore;
using VideoAppDAL.Interfaces;
using VidepAppEntity;

namespace VideoAppDAL.Context
{
    internal class InMemoryContext : DbContext, IContext
    {
        //In memory setup
        private static readonly DbContextOptions<InMemoryContext> Options =
            new DbContextOptionsBuilder<InMemoryContext>().UseInMemoryDatabase("VideoDB").Options;

        public DbSet<Video> Videos { set; get; }

        //Options that we want in memory
        public InMemoryContext() : base(Options)
        {
        }
    }
}