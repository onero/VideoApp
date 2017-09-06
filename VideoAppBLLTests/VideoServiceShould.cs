using System;
using VideoAppBLL;
using VideoAppBLL.BusinessObjects;
using VideoAppBLL.Interfaces;
using VideoAppDAL.Entities;
using Xunit;
using Xunit.Sdk;

namespace VideoAppBLLTests
{
    public class VideoServiceShould
    {
        private readonly IService<VideoBO> _videoService;

        public VideoServiceShould()
        {
            var bllFacade = new BLLFacade();
            _videoService = bllFacade.VideoService;
            _videoService.ClearAll();
        }

        private static readonly VideoBO MockVideo = new VideoBO
        {
            Title = "Die Hard",
            Genre = GenreBO.Action
        };

        [Fact]
        public void CreateOneVideo()
        {
            var created = _videoService.Create(MockVideo);

            Assert.NotNull(created);
        }

        [Fact]
        public void DeleteVideoById()
        {
            var createdVideo = _videoService.Create(MockVideo);
            var idOfCreatedVideo = createdVideo.Id;
            var videoDeleted = _videoService.Delete(idOfCreatedVideo);
            Assert.True(videoDeleted);
        }

        [Fact]
        public void FailGetOneVideoByWrongId()
        {
            _videoService.Create(MockVideo);
            const int nonExistingId = 0;
            var videoFromSearch = Record.Exception( ()=> _videoService.GetById(nonExistingId));
            Assert.IsType(typeof(NullReferenceException), videoFromSearch);
        }

        [Fact]
        public void GetAllVideos()
        {
            _videoService.Create(MockVideo);
            var videos = _videoService.GetAll();

            Assert.NotEmpty(videos);
        }

        [Fact]
        public void GetOneVideoById()
        {
            var createdVideo = _videoService.Create(MockVideo);

            var videoFromSearch = _videoService.GetById(createdVideo.Id);

            Assert.Equal(createdVideo, videoFromSearch);
        }

        [Fact]
        public void ReturnEmptyListWhenCleared()
        {
            _videoService.Create(MockVideo);
            _videoService.ClearAll();
            var videos = _videoService.GetAll();

            Assert.Empty(videos);
        }

        [Fact]
        public void ShouldUpdateVideo()
        {
            var createdVideo = _videoService.Create(MockVideo);
            createdVideo.Title = "Awesome";
            createdVideo.Genre = GenreBO.Romance;
            var updatedVideo = _videoService.Update(createdVideo);

            Assert.Equal(createdVideo, updatedVideo);
        }
    }
}