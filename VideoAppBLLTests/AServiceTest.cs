using Moq;
using VideoAppDAL.Interfaces;
using Xunit;

namespace VideoAppBLLTests
{
    public abstract class AServiceTest
    {
        protected readonly Mock<IDALFacade> MockDALFacade = new Mock<IDALFacade>();
        protected readonly Mock<IUnitOfWork> MockUOW = new Mock<IUnitOfWork>();

        protected AServiceTest()
        {
            MockDALFacade.SetupGet(dal => dal.UnitOfWork).Returns(MockUOW.Object);
        }
        
        public abstract void CreateOne();

        public abstract void GetAll();

        public abstract void GetOneByExistingId();

        public abstract void NotGetOneByNonExistingId();

        public abstract void NotConvertNullEntity();

        public abstract void GetAllByExistingIds();

        public abstract void NotGetAllByNonExistingIds();

        public abstract void DeleteByExistingId();

        public abstract void NotDeleteByNonExistingId();

        public abstract void UpdateByExistingId();

        public abstract void NotUpdateByNonExistingId();
    }
}