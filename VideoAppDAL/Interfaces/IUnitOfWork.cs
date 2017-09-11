using System;
using VideoAppDAL.Entities;

namespace VideoAppDAL.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        IRepository<Video> VideoRepository { get; }

        IRepository<Profile> ProfileRepository { get; }

        IRepository<Rental> RentalRepository { get; }

        void Complete();
    }
}