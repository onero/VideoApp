using VideoAppDAL.Repository;
using VidepAppEntity;

namespace VideoAppDAL.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        public IRepository<Video> VideoRepository => new VideoRepository();
    }
}