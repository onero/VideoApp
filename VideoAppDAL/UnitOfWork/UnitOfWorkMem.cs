using VideoAppDAL.Context;
using VideoAppDAL.Interfaces;
using VideoAppDAL.Repository;
using VidepAppEntity;

namespace VideoAppDAL.UnitOfWork
{
    public class UnitOfWorkMem : IUnitOfWork
    {
        private readonly InMemoryContext _context;
        public IRepository<Video> VideoRepository { get; }

        public UnitOfWorkMem()
        {
            _context = new InMemoryContext();
            VideoRepository = new VideoRepository(_context);
        }


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