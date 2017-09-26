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

        public VideosControllerShould()
        {
            _controller = new VideosController(MockVideoService.Object);
        }

        private readonly VideoBO MockVideoBO = new VideoBO
        {
            Id = 1,
            Title = "Die Hard"
        };

        [Fact]
        public void DeleteByExistingId_ReturnOk()
        {
            MockVideoService.Setup(s => s.Delete(MockVideoBO.Id)).Returns(true);

            var result = _controller.Delete(MockVideoBO.Id);
            var message = ResultMessageService.GetMessage(result);

            Assert.IsType<OkObjectResult>(result);
            Assert.Contains("Deleted", message);
        }

        [Fact]
        public void GetAll()
        {
            MockVideoService.Setup(r => r.GetAll()).Returns(new List<VideoBO> {MockVideoBO});

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
        public void NotDeleteByNonExistingId_ReturnNotFound()
        {
            MockVideoService.Setup(s => s.Delete(0)).Returns(false);

            var result = _controller.Delete(0);
            var message = ResultMessageService.GetMessage(result);

            Assert.IsType<NotFoundObjectResult>(result);
            Assert.Contains(ErrorMessages.IdWasNotFoundMessage(0), message);
        }

        [Fact]
        public void NotGetByNonExistingId_ReturnNotFound()
        {
            MockVideoService.Setup(s => s.GetById(It.IsAny<int>())).Returns(() => null);

            var result = _controller.Get(0);
            var message = ResultMessageService.GetMessage(result);

            Assert.IsType<NotFoundObjectResult>(result);
            Assert.Contains(ErrorMessages.IdWasNotFoundMessage(0), message);
        }

        [Fact]
        public void NotPostWithInvalidObject_ReturnBadRequest()
        {
            MockVideoService.Setup(s => s.Create(It.IsAny<VideoBO>())).Returns(new VideoBO());
            var video = new VideoBO();
            _controller.ModelState.AddModelError("", "");
            var result = _controller.Post(video);

            Assert.IsType<BadRequestObjectResult>(result);
        }

        [Fact]
        public void NotPostWithNull_ReturnBadRequest()
        {
            MockVideoService.Setup(s => s.Create(It.IsAny<VideoBO>())).Returns(new VideoBO());
            var result = _controller.Post(null);
            var message = ResultMessageService.GetMessage(result);

            Assert.IsType<BadRequestObjectResult>(result);
            Assert.Contains(ErrorMessages.InvalidJSON, message);
        }

        [Fact]
        public void NotUpdateWithInvalidObject_ReturnBadRequest()
        {
            MockVideoService.Setup(s => s.Update(It.IsAny<VideoBO>())).Returns(new VideoBO());

            var video = new VideoBO();

            _controller.ModelState.AddModelError("", "");

            var result = _controller.Put(0, video);

            Assert.IsType<BadRequestObjectResult>(result);
        }

        [Fact]
        public void NotUpdateWithMisMatchingIds_ReturnBadRequest()
        {
            MockVideoService.Setup(s => s.Update(It.IsAny<VideoBO>())).Returns(new VideoBO());

            var result = _controller.Put(0, MockVideoBO);
            var message = ResultMessageService.GetMessage(result);

            Assert.IsType<BadRequestObjectResult>(result);
            Assert.Contains(ErrorMessages.IdDoesNotMatchMessage(0), message);
        }

        [Fact]
        public void NotUpdateWithNonExistingId_ReturnNotFound()
        {
            MockVideoService.Setup(s => s.Update(MockVideoBO)).Returns(() => null);

            var result = _controller.Put(MockVideoBO.Id, MockVideoBO);
            var message = ResultMessageService.GetMessage(result);

            Assert.IsType<NotFoundObjectResult>(result);
            Assert.Contains(ErrorMessages.IdWasNotFoundMessage(MockVideoBO.Id), message);
        }

        [Fact]
        public void NotUpdateWithNull_ReturnBadRequest()
        {
            MockVideoService.Setup(s => s.Update(It.IsAny<VideoBO>())).Returns(new VideoBO());

            var result = _controller.Put(0, null);
            var message = ResultMessageService.GetMessage(result);

            Assert.IsType<BadRequestObjectResult>(result);
            Assert.Contains(ErrorMessages.InvalidJSON, message);
        }

        [Fact]
        public void PostWithValidObject()
        {
            MockVideoService.Setup(s => s.Create(It.IsAny<VideoBO>())).Returns(new VideoBO());

            var video = new VideoBO {Id = 1, Title = "Die Hard"};
            var result = _controller.Post(video);

            Assert.IsType<CreatedResult>(result);
        }

        [Fact]
        public void UpdateWithValidObject_ReturnOk()
        {
            MockVideoService.Setup(s => s.Create(It.IsAny<VideoBO>())).Returns(new VideoBO());
            MockVideoService.Setup(s => s.Update(It.IsAny<VideoBO>())).Returns(new VideoBO());

            _controller.Post(MockVideoBO);

            MockVideoBO.Title = "Die Mega Hard";

            var updated = _controller.Put(MockVideoBO.Id, MockVideoBO);
            var message = ResultMessageService.GetMessage(updated);

            Assert.IsType<OkObjectResult>(updated);
            Assert.Contains("Updated!", message);
        }
    }
}