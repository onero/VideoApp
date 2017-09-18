using Xunit;

namespace VideoAppBLLTests
{
    public interface IServiceTest
    {
        void CreateOne();

        void GetAll();

        void GetOneByExistingId();

        void NotGetOneByNonExistingId();

        void GetAllByExistingIds();

        void NotGetAllByNonExistingIds();

        void DeleteByExistingId();

        void NotDeleteByNonExistingId();

        void UpdateByExistingId();

        void NotUpdateByNonExistingId();
    }
}