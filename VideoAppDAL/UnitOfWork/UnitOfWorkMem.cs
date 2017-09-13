using VideoAppDAL.Context;
using VideoAppDAL.Entities;
using VideoAppDAL.Interfaces;
using VideoAppDAL.Repository;

namespace VideoAppDAL.UnitOfWork
{
    internal class UnitOfWorkMem : IUnitOfWork
    {
        private readonly InMemoryContext _context;

        public IRepository<Video> VideoRepository { get; }
        public IRepository<Profile> ProfileRepository { get; }
        public IRepository<Rental> RentalRepository { get; }
        public IRepository<Genre> GenreRepository { get; }
        public IRepository<User> UserRepository { get; }
        public IRepository<Role> RoleRepository { get; }

        public UnitOfWorkMem(InMemoryContext context)
        {
            _context = context;
            RoleRepository = new RoleRepository(_context);
            UserRepository = new UserRepository(_context);
            GenreRepository = new GenreRepository(_context);
            VideoRepository = new VideoRepository(_context);
            ProfileRepository = new ProfileRepository(_context);
            RentalRepository = new RentalRepository(_context);
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