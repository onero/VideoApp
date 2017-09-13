using System;
using VideoAppDAL.Entities;

namespace VideoAppDAL.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        IRepository<Video> VideoRepository { get; }

        IRepository<Profile> ProfileRepository { get; }

        IRepository<Rental> RentalRepository { get; }
        IRepository<Genre> GenreRepository { get; }
        IRepository<User> UserRepository { get; }
        IRepository<Role> RoleRepository { get; }

        void Complete();
    }
}