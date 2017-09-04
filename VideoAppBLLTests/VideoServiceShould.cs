using System;
using VideoAppBLL;
using VideoAppBLL.BusinessObjects;
using VideoAppDAL.Entities;
using Xunit;
using Xunit.Sdk;

namespace VideoAppBLLTests
{
    public class VideoServiceShould
    {
        public VideoServiceShould()
        {
            var bllFacade = new BLLFacade();
            _videoService = bllFacade.VideoService;
            _videoService.ClearAll();
        }

        private readonly IService<VideoBO> _videoService;

        private static readonly VideoBO MockVideo = new VideoBO
        {
            Title = "Die Hard",
            Genre = Genre.Action
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
            _videoService.Delete(idOfCreatedVideo);
            var videos = _videoService.GetAll();
            Assert.DoesNotContain(createdVideo, videos);
        }

        [Fact]
        public void FailGetOneVideoByWrongId()
        {
            _videoService.Create(MockVideo);
            var nonExistingId = 0;
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
            createdVideo.Genre = Genre.Romance;
            var updatedVideo = _videoService.Update(createdVideo);

            Assert.Equal(createdVideo, updatedVideo);
        }
    }
}