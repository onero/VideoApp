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

        public VideoServiceShould()
        {
            var facade = new DALFacade();
            _videoService = new VideoService(facade);
        }

        [Fact]
        public void GetAllVideos()
        {
            var videos = _videoService.GetAll();

            Assert.NotNull(videos);
        }
    }
}
