using System.Collections.Generic;
using Moq;
using VideoAppBLL.BusinessObjects;
using VideoAppBLL.Interfaces;
using VideoAppBLL.Service;
using VideoAppDAL;
using VideoAppDAL.Entities;
using VideoAppDAL.Interfaces;
using Xunit;

namespace VideoAppBLLTests
{
    public class VideoServiceShould : IServiceTest
    {
        private readonly Mock<IDALFacade> MockDALFacade = new Mock<IDALFacade>();
        private readonly Mock<IUnitOfWork> MockUOW = new Mock<IUnitOfWork>();
        private readonly Mock<IRepository<Video>> MockVideoRepository = new Mock<IRepository<Video>>(MockBehavior.Strict);
        private readonly IVideoService _service;

        private const int NonExistingId = 99;

        private readonly Video MockVideo = new Video()
        {
            Id = 1,
            Title = "Die Hard"
        };

        private readonly VideoBO MockVideoBO = new VideoBO
        {
            Id = 1,
            Title = "Die Hard"
        };

        public VideoServiceShould()
        {
            MockUOW.SetupGet(uow => uow.VideoRepository).Returns(MockVideoRepository.Object);
            MockDALFacade.SetupGet(dal => dal.UnitOfWork).Returns(MockUOW.Object);
            _service = new VideoService(MockDALFacade.Object);
        }

        [Fact]
        public void CreateOne()
        {
            MockVideoRepository.Setup(r => r.Create(It.IsAny<Video>())).Returns(MockVideo);

            var entity = _service.Create(MockVideoBO);

            Assert.NotNull(entity);
        }

        [Fact]
        public void GetAll()
        {
            MockVideoRepository.Setup(r => r.GetAll()).Returns(new List<Video>() {MockVideo});

            var entitites = _service.GetAll();

            Assert.NotEmpty(entitites);
        }

        [Fact]
        public void GetOneByExistingId()
        {
            MockVideoRepository.Setup(r => r.GetById(MockVideo.Id)).Returns(MockVideo);

            var existingId = MockVideoBO.Id;

            var entity = _service.GetById(existingId);

            Assert.NotNull(entity);
        }

        [Fact]
        public void NotGetOneByNonExistingId()
        {
            MockVideoRepository.Setup(r => r.GetById(It.IsAny<int>())).Returns(() => null);

            var entity = _service.GetById(NonExistingId);

            Assert.Null(entity);
        }

        [Fact]
        public void GetAllByExistingIds()
        {
            var listOfExistingIds = new List<int>(){MockVideo.Id};

            MockVideoRepository.Setup(r => r.GetAllByIds(new List<int>() {MockVideo.Id})).Returns(new List<Video>() {MockVideo});

            var entities = _service.GetAllByIds(listOfExistingIds);

            Assert.NotEmpty(entities);
        }

        [Fact]
        public void NotGetAllByNonExistingIds()
        {
            var nonExistingIds = new List<int>(){NonExistingId};

            MockVideoRepository.Setup(r => r.GetAllByIds(It.IsAny<List<int>>())).Returns(new List<Video>());

            var entities = _service.GetAllByIds(nonExistingIds);

            Assert.Empty(entities);
        }

        [Fact]
        public void DeleteByExistingId()
        {
            var existingId = MockVideoBO.Id;

            MockVideoRepository.Setup(r => r.GetById(existingId)).Returns(MockVideo);
            MockVideoRepository.Setup(r => r.Delete(existingId)).Returns(true);

            var deleted = _service.Delete(existingId);

            Assert.True(deleted);
        }

        [Fact]
        public void NotDeleteByNonExistingId()
        {
            MockVideoRepository.Setup(r => r.GetById(It.IsAny<int>())).Returns(() => null);

            var deleted = _service.Delete(NonExistingId);

            Assert.False(deleted);
        }

        [Fact]
        public void UpdateByExistingId()
        {
            MockVideoRepository.Setup(r => r.GetById(It.IsAny<int>())).Returns(MockVideo);

            MockVideoRepository.Setup(r => r.Update(It.IsAny<Video>())).Returns((Video video) => video);

            MockVideo.Title = "Moq";

            var updatedEntity = _service.Update(MockVideoBO);

            Assert.Equal(MockVideoBO, updatedEntity);
        }

        [Fact]
        public void NotUpdateByNonExistingId()
        {
            MockVideoRepository.Setup(r => r.GetById(It.IsAny<int>())).Returns(() => null);

            var updatedEntity = _service.Update(MockVideoBO);

            Assert.Null(updatedEntity);
        }
    }
}