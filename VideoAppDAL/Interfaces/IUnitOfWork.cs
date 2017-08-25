using System;
using VidepAppEntity;

namespace VideoAppDAL.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        IRepository<Video> VideoRepository { get; }

        void Complete();
    }
}