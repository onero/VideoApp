using Xunit;

namespace VideoAppBLLTests
{
    public interface ITest
    {
        void CreateOne();

        void GetAll();

        void GetOneByExistingId();

        void NotGetOneByNonExistingId();

        void DeleteByExistingId();

        void NotDeleteByNonExistingId();

        void UpdateByExistingId();

        void NotUpdateByNonExistingId();
    }
}