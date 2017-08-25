using System;
using VidepAppEntity;

namespace VideoAppDAL
{
    public interface IUnitOfWork : IDisposable
    {
        IRepository<Video> VideoRepository { get; }

        void Complete();
    }
}