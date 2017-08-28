using VideoAppDAL.Context;
using VideoAppDAL.Entities;
using VideoAppDAL.Interfaces;
using VideoAppDAL.Repository;

namespace VideoAppDAL.UnitOfWork
{
    internal class UnitOfWorkMem : IUnitOfWork
    {
        private readonly InMemoryContext _context;

        public UnitOfWorkMem(InMemoryContext context)
        {
            _context = context;
            VideoRepository = new VideoRepository(_context);
        }

        public IRepository<Video> VideoRepository { get; }


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