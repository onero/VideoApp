﻿using System.Collections.Generic;
using Moq;
using VideoAppBLL.BusinessObjects;
using VideoAppBLL.Interfaces;
using VideoAppBLL.Service;
using VideoAppDAL.Entities;
using VideoAppDAL.Interfaces;
using Xunit;

namespace VideoAppBLLTests
{
    public class VideoServiceShould : AServiceTest
    {
        public VideoServiceShould()
        {
            MockUOW.SetupGet(uow => uow.VideoRepository).Returns(MockVideoRepository.Object);
            _service = new VideoService(MockDALFacade.Object);
        }

        private readonly Mock<IRepository<Video>> MockVideoRepository = new Mock<IRepository<Video>>(MockBehavior.Strict);

        private readonly IVideoService _service;

        private const int NonExistingId = 99;


        private readonly Video MockVideo = new Video
        {
            Id = 1,
            Title = "Die Hard"
        };

        private readonly VideoBO MockVideoBO = new VideoBO
        {
            Id = 1,
            Title = "Die Hard",
            GenreIds = new List<int>() {1}
        };

        [Fact]
        public override void CreateOne()
        {
            MockVideoRepository.Setup(r => r.Create(It.IsAny<Video>())).Returns(MockVideo);

            var entity = _service.Create(MockVideoBO);

            Assert.NotNull(entity);
        }

        [Fact]
        public override void DeleteByExistingId()
        {
            var existingId = MockVideoBO.Id;

            MockVideoRepository.Setup(r => r.GetById(existingId)).Returns(MockVideo);
            MockVideoRepository.Setup(r => r.Delete(existingId)).Returns(true);

            var deleted = _service.Delete(existingId);

            Assert.True(deleted);
        }

        [Fact]
        public override void GetAll()
        {
            MockVideoRepository.Setup(r => r.GetAll()).Returns(new List<Video> {MockVideo});

            var entitites = _service.GetAll();

            Assert.NotEmpty(entitites);
        }

        [Fact]
        public override void GetAllByExistingIds()
        {
            var listOfExistingIds = new List<int> {MockVideo.Id};

            MockVideoRepository.Setup(r => r.GetAllByIds(new List<int> {MockVideo.Id})).Returns(new List<Video> {MockVideo});

            var entities = _service.GetAllByIds(listOfExistingIds);

            Assert.NotEmpty(entities);
        }

        [Fact]
        public override void GetOneByExistingId()
        {
            var mockGenreRepository = new Mock<IRepository<Genre>>();
            MockUOW.Setup(uow => uow.GenreRepository).Returns(mockGenreRepository.Object);

            MockVideoBO.GenreIds = new List<int> {1};

            var genre = new Genre
            {
                Id = 1,
                Name = "Action"
            };

            MockVideo.Genres = new List<VideoGenre>
            {
                new VideoGenre
                {
                    GenreId = 1,
                    VideoId = 1
                }
            };
            MockVideoRepository.Setup(r => r.GetById(MockVideo.Id)).Returns(MockVideo);
            mockGenreRepository.Setup(g => g.GetAllByIds(MockVideoBO.GenreIds)).Returns(
                new List<Genre> {genre});

            var existingId = MockVideoBO.Id;

            var entity = _service.GetById(existingId);

            Assert.NotNull(entity);
            Assert.NotEmpty(entity.Genres);
        }

        [Fact]
        public void GetOneWithoutGenreByExistingId_WhenGenreDoNotExist()
        {
            MockVideoRepository.Setup(r => r.GetById(MockVideoBO.Id)).Returns(MockVideo);
            var mockGenreRepository = new Mock<IRepository<Genre>>();
            MockUOW.Setup(uow => uow.GenreRepository).Returns(mockGenreRepository.Object);
            mockGenreRepository.Setup(r => r.GetAllByIds(It.IsAny<List<int>>())).Returns(() => null);
            
            var entity = _service.GetById(MockVideoBO.Id);

            Assert.NotNull(entity);
            Assert.Null(entity.Genres);

        }

        [Fact]
        public override void NotConvertNullEntity()
        {
            var entity = _service.Create(null);

            Assert.Null(entity);
        }

        [Fact]
        public override void NotDeleteByNonExistingId()
        {
            MockVideoRepository.Setup(r => r.GetById(It.IsAny<int>())).Returns(() => null);

            var deleted = _service.Delete(NonExistingId);

            Assert.False(deleted);
        }

        [Fact]
        public override void NotGetAllByNonExistingIds()
        {
            var nonExistingIds = new List<int> {NonExistingId};

            MockVideoRepository.Setup(r => r.GetAllByIds(It.IsAny<List<int>>())).Returns(new List<Video>());

            var entities = _service.GetAllByIds(nonExistingIds);

            Assert.Empty(entities);
        }

        [Fact]
        public override void NotGetOneByNonExistingId()
        {
            MockVideoRepository.Setup(r => r.GetById(It.IsAny<int>())).Returns(() => null);

            var entity = _service.GetById(NonExistingId);

            Assert.Null(entity);
        }

        [Fact]
        public override void NotUpdateByNonExistingId()
        {
            MockVideoRepository.Setup(r => r.GetById(It.IsAny<int>())).Returns(() => null);

            var updatedEntity = _service.Update(MockVideoBO);

            Assert.Null(updatedEntity);
        }

        [Fact]
        public override void UpdateByExistingId()
        {
            MockVideoRepository.Setup(r => r.GetById(It.IsAny<int>())).Returns(MockVideo);

            MockVideoRepository.Setup(r => r.Update(It.IsAny<Video>())).Returns((Video video) => video);

            MockVideo.Title = "Moq";

            var updatedEntity = _service.Update(MockVideoBO);

            Assert.Equal(MockVideoBO, updatedEntity);
        }
    }
}