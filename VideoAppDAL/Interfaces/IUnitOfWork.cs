using VidepAppEntity;

namespace VideoAppDAL
{
    public interface IUnitOfWork
    {
        IRepository<Video> VideoRepository { get; }
    }
}