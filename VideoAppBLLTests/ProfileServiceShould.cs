using System.Collections.Generic;
using Moq;
using VideoAppBLL.BusinessObjects;
using VideoAppBLL.Interfaces;
using VideoAppBLL.Service;
using VideoAppDAL.Entities;
using VideoAppDAL.Interfaces;
using Xunit;

namespace VideoAppBLLTests
{
    public class ProfileServiceShould : AServiceTest
    {
        private readonly Mock<IRepository<Profile>> MockProfileRepository = new Mock<IRepository<Profile>>(MockBehavior.Strict);
        private readonly IProfileService _service;

        public ProfileServiceShould()
        {
            MockUOW.Setup(uow => uow.ProfileRepository).Returns(MockProfileRepository.Object);
            _service = new ProfileService(MockDALFacade.Object);
        }

        private readonly Profile MockProfile = new Profile()
        {
            Id = 1,
            FirstName = "Adamion",
            LastName = "DaMan",
            Address = "Home"
        };

        private readonly ProfileBO MockProfileBO = new ProfileBO()
        {
            Id = 1,
            FirstName = "Adamion",
            LastName = "DaMan",
            Address = "Home"
        };

        [Fact]
        public override void CreateOne()
        {
            MockProfileRepository.Setup(r => r.Create(It.IsAny<Profile>())).Returns(MockProfile);

            var entity = _service.Create(MockProfileBO);

            Assert.NotNull(entity);
        }

        [Fact]
        public override void GetAll()
        {
            MockProfileRepository.Setup(r => r.GetAll()).Returns(new List<Profile>() {MockProfile});

            var entities = _service.GetAll();

            Assert.NotEmpty(entities);
        }

        [Fact]
        public override void GetOneByExistingId()
        {
            MockProfileRepository.Setup(r => r.GetById(MockProfileBO.Id)).Returns(MockProfile);

            var entity = _service.GetById(MockProfileBO.Id);

            Assert.NotNull(entity);
        }

        [Fact]
        public override void NotGetOneByNonExistingId()
        {
            MockProfileRepository.Setup(r => r.GetById(0)).Returns(() => null);

            var entity = _service.GetById(0);

            Assert.Null(entity);
        }

        [Fact]
        public override void NotConvertNullEntity()
        {
            var entity = _service.Create(null);

            Assert.Null(entity);
        }

        [Fact]
        public override void GetAllByExistingIds()
        {
            var existingIds = new List<int>();
            MockProfileRepository.Setup(r => r.GetAllByIds(existingIds)).Returns(new List<Profile>(){MockProfile});

            var entities = _service.GetAllByIds(existingIds);

            Assert.NotEmpty(entities);
        }

        [Fact]
        public override void NotGetAllByNonExistingIds()
        {
            MockProfileRepository.Setup(r => r.GetAllByIds(It.IsAny<List<int>>())).Returns(new List<Profile>());

            var entities = _service.GetAllByIds(new List<int>() {0});

            Assert.Empty(entities);
        }

        [Fact]
        public override void DeleteByExistingId()
        {
            MockProfileRepository.Setup(r => r.GetById(MockProfileBO.Id)).Returns(MockProfile);
            MockProfileRepository.Setup(r => r.Delete(MockProfileBO.Id)).Returns(true);

            var deleted = _service.Delete(MockProfileBO.Id);

            Assert.True(deleted);
        }

        [Fact]
        public override void NotDeleteByNonExistingId()
        {
            MockProfileRepository.Setup(r => r.GetById(0)).Returns(() => null);

            var deleted = _service.Delete(0);

            Assert.False(deleted);
        }

        [Fact]
        public override void UpdateByExistingId()
        {
            MockProfileRepository.Setup(r => r.GetById(MockProfileBO.Id)).Returns(MockProfile);
            MockProfileRepository.Setup(r => r.Update(It.IsAny<Profile>())).Returns((Profile updatedProfile) => updatedProfile);
            MockProfileBO.FirstName = "Awesome";
            var entity = _service.Update(MockProfileBO);

            Assert.Equal(MockProfileBO, entity);
        }

        [Fact]
        public override void NotUpdateByNonExistingId()
        {
            MockProfileRepository.Setup(r => r.GetById(0)).Returns(() => null);

            var entity = _service.Update(new ProfileBO() {Id = 0});

            Assert.Null(entity);
        }
    }
}