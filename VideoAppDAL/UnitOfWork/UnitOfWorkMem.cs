using VideoAppDAL.Context;
using VideoAppDAL.Repository;
using VidepAppEntity;

namespace VideoAppDAL.UnitOfWork
{
    public class UnitOfWorkMem : IUnitOfWork
    {

        private readonly InMemoryContext _context;

        public UnitOfWorkMem()
        {
            _context = new InMemoryContext();
        }

        public IRepository<Video> VideoRepository => new VideoRepository();

        public void Complete()
        {
            _context.SaveChanges();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}