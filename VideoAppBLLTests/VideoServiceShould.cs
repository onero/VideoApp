using System;
using VideoAppBLL;
using VideoAppBLL.Service;
using VideoAppDAL;
using VidepAppEntity;
using Xunit;

namespace VideoAppBLLTests
{
    public class VideoServiceShould
    {
        private readonly IService<Video> _videoService;

        private static readonly Video MockVideo = new Video()
        {
            Title = "Die Hard",
            Genre = Genre.Action
        };

        public VideoServiceShould()
        {
            var bllFacade = new BLLFacade();
            _videoService = bllFacade.VideoService;
            _videoService.ClearAll();
        }

        [Fact]
        public void GetAllVideos()
        {
            _videoService.Create(MockVideo);
            var videos = _videoService.GetAll();

            Assert.NotEmpty(videos);
        }

        [Fact]
        public void CreateOneVideo()
        {
            var created = _videoService.Create(MockVideo);

            Assert.NotNull(created);
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
        public void GetOneVideoById()
        {
            var createdVideo = _videoService.Create(MockVideo);

            var videoFromSearch = _videoService.GetById(createdVideo.Id);

            Assert.Equal(createdVideo, videoFromSearch);
        }

        [Fact]
        public void FailGetOneVideoByWrongId()
        {
            _videoService.Create(MockVideo);
            var nonExistingId = 0;
            var videoFromSearch = _videoService.GetById(nonExistingId);
            Assert.Null(videoFromSearch);
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
        public void ShouldUpdateVideo()
        {
            var createdVideo = _videoService.Create(MockVideo);
            createdVideo.Title = "Awesome";
            var updatedVideo = _videoService.Update(createdVideo);

            Assert.Equal(createdVideo, updatedVideo);
        }

    }
}
