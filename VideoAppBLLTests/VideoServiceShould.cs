using System;
using System.Collections.Generic;
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
        private readonly IVideoService _service;

        public VideoServiceShould()
        {
            var bllFacade = new BLLFacade();
            _service = bllFacade.VideoService;
            _service.ClearAll();
        }

        private static readonly VideoBO MockVideo = new VideoBO
        {
            Title = "Die Hard",
            Rentals = new List<RentalBO>()
        };

        [Fact]
        public void CreateOneVideo()
        {
            var created = _service.Create(MockVideo);

            Assert.NotNull(created);
        }

        [Fact]
        public void DeleteVideoById()
        {
            var createdVideo = _service.Create(MockVideo);
            var idOfCreatedVideo = createdVideo.Id;
            var videoDeleted = _service.Delete(idOfCreatedVideo);
            Assert.True(videoDeleted);
        }

        [Fact]
        public void FailGetOneVideoByWrongId()
        {
            _service.Create(MockVideo);
            const int nonExistingId = 0;
            var videoFromSearch = _service.GetById(nonExistingId);
            Assert.Null(videoFromSearch);
        }

        [Fact]
        public void GetAllVideos()
        {
            _service.Create(MockVideo);
            var videos = _service.GetAll();

            Assert.NotEmpty(videos);
        }

        [Fact]
        public void GetOneVideoById()
        {
            var createdVideo = _service.Create(MockVideo);

            var videoFromSearch = _service.GetById(createdVideo.Id);

            Assert.Equal(createdVideo, videoFromSearch);
        }

        [Fact]
        public void ReturnEmptyListWhenCleared()
        {
            _service.Create(MockVideo);
            _service.ClearAll();
            var videos = _service.GetAll();

            Assert.Empty(videos);
        }

        [Fact]
        public void ShouldUpdateVideo()
        {
            var createdVideo = _service.Create(MockVideo);
            createdVideo.Title = "Awesome";
            var updatedVideo = _service.Update(createdVideo);

            Assert.Equal(createdVideo, updatedVideo);
        }
    }
}