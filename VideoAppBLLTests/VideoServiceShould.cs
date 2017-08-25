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
            var facade = new DALFacade();
            _videoService = new VideoService(facade);
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

    }
}
