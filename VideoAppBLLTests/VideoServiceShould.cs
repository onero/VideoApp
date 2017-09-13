using System.Collections.Generic;
using VideoAppBLL;
using VideoAppBLL.BusinessObjects;
using VideoAppBLL.Interfaces;
using Xunit;

namespace VideoAppBLLTests
{
    public class VideoServiceShould : ITest
    {
        private const int NonExistingId = 999;

        public VideoServiceShould()
        {
            var bllFacade = new BLLFacade();
            _service = bllFacade.VideoService;
            _service.ClearAll();
        }

        private readonly IVideoService _service;

        private static readonly VideoBO MockVideo = new VideoBO
        {
            Title = "Die Hard",
            GenreId = 1
        };

        [Fact]
        public void CreateOne()
        {
            var created = _service.Create(MockVideo);

            Assert.NotNull(created);
        }

        [Fact]
        public void GetAll()
        {
            _service.Create(MockVideo);
            var videos = _service.GetAll();

            Assert.NotEmpty(videos);
        }

        [Fact]
        public void GetOneByExistingId()
        {
            var createdVideo = _service.Create(MockVideo);

            var videoFromSearch = _service.GetById(createdVideo.Id);

            Assert.Equal(createdVideo.Id, videoFromSearch.Id);
        }

        [Fact]
        public void NotGetOneByNonExistingId()
        {
            _service.Create(MockVideo);
            var videoFromSearch = _service.GetById(NonExistingId);
            Assert.Null(videoFromSearch);
        }

        [Fact]
        public void DeleteByExistingId()
        {
            var createdVideo = _service.Create(MockVideo);
            var idOfCreatedVideo = createdVideo.Id;
            var videoDeleted = _service.Delete(idOfCreatedVideo);
            Assert.True(videoDeleted);
        }

        [Fact]
        public void NotDeleteByNonExistingId()
        {
            var deleted = _service.Delete(NonExistingId);

            Assert.False(deleted);
        }

        [Fact]
        public void UpdateByExistingId()
        {
            var createdVideo = _service.Create(MockVideo);
            createdVideo.Title = "Awesome";
            var updatedVideo = _service.Update(createdVideo);

            Assert.Equal(createdVideo, updatedVideo);
        }

        [Fact]
        public void NotUpdateByNonExistingId()
        {
            var result = _service.Update(MockVideo);

            Assert.Null(result);
        }

        [Fact]
        public void ReturnEmptyListWhenCleared()
        {
            _service.Create(MockVideo);
            _service.ClearAll();
            var videos = _service.GetAll();

            Assert.Empty(videos);
        }
    }
}