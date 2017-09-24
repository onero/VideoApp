using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Moq;
using VideoAppBLL.BusinessObjects;
using VideoAppBLL.Interfaces;
using VideoRestAPI.Controllers;
using Xunit;

namespace VideoRestAPITests
{
    public class VideosControllerShould : IControllerTest
    {
        private readonly Mock<IVideoService> MockVideoService = new Mock<IVideoService>(MockBehavior.Strict);
        private readonly VideosController _controller;
        private readonly VideoBO MockVideoBO = new VideoBO()
        {
            Id = 1,
            Title = "Die Hard"
        };


        public VideosControllerShould()
        {
            _controller = new VideosController(MockVideoService.Object);
        }

        [Fact]
        public void GetAll()
        {
            MockVideoService.Setup(r => r.GetAll()).Returns(new List<VideoBO>(){MockVideoBO});

            var result = _controller.Get();

            Assert.NotEmpty(result);
        }
        [Fact]
        public void GetByExistingId()
        {
            MockVideoService.Setup(s => s.GetById(It.IsAny<int>())).Returns(MockVideoBO);

            var result = _controller.Get(1);

            Assert.NotNull(result);
        }
        [Fact]
        public void NotGetByNonExistingId_ReturnNotFound()
        {
            MockVideoService.Setup(s => s.GetById(It.IsAny<int>())).Returns(() => null);

            var result = _controller.Get(0);

            Assert.IsType<NotFoundObjectResult>(result);
        }
        [Fact]
        public void PostWithValidObject()
        {
            throw new NotImplementedException();
        }
        [Fact]
        public void NotPostWithInvalidObject_ReturnBadRequest()
        {
            throw new NotImplementedException();
        }
        [Fact]
        public void NotPostWithEmptyObject_ReturnBadRequest()
        {
            throw new NotImplementedException();
        }
        [Fact]
        public void UpdateWithValidObject_ReturnOk()
        {
            throw new NotImplementedException();
        }
        [Fact]
        public void NotUpdateWithEmptyObject_ReturnBadRequest()
        {
            throw new NotImplementedException();
        }
        [Fact]
        public void NotUpdateWithInvalidObject_ReturnBadRequest()
        {
            throw new NotImplementedException();
        }
        [Fact]
        public void NotUpdateWithNonExistingId_ReturnNotFound()
        {
            throw new NotImplementedException();
        }
        [Fact]
        public void DeleteByExistingId_ReturnOk()
        {
            throw new NotImplementedException();
        }
        [Fact]
        public void NotDeleteByNonExistingId_ReturnNotFound()
        {
            throw new NotImplementedException();
        }
    }
}
