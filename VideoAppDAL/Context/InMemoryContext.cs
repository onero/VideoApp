using Microsoft.EntityFrameworkCore;
using VidepAppEntity;

namespace VideoAppDAL.Context
{
    public class InMemoryContext : DbContext
    {
        //In memory setup
        private static readonly DbContextOptions<InMemoryContext> Options =
            new DbContextOptionsBuilder<InMemoryContext>().UseInMemoryDatabase("TheDB").Options;

        public DbSet<Video> Videos { set; get; }

        //Options that we want in memory
        public InMemoryContext() : base(Options)
        {

        }
    }
}