using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Moq;
using VideoAppBLL.BusinessObjects;
using VideoAppBLL.Interfaces;
using VideoRestAPI.Controllers;
using Xunit;

namespace VideoRestAPITests
{
    public class GenresControllerShould : IControllerTest
    {
        private readonly Mock<IGenreService> MockGenreService = new Mock<IGenreService>(MockBehavior.Strict);
        private readonly GenresController _controller;

        public GenresControllerShould()
        {
            _controller = new GenresController(MockGenreService.Object);
        }

        private readonly GenreBO MockGenre = new GenreBO()
        {
            Id = 1,
            Name = "Action"
        };
        
        [Fact]
        public void GetAll()
        {
            MockGenreService.Setup(r => r.GetAll()).Returns(new List<GenreBO>(){MockGenre});

            var result = _controller.Get();

            Assert.NotEmpty(result);
        }

        [Fact]
        public void GetByExistingId()
        {
            MockGenreService.Setup(r => r.GetById(MockGenre.Id)).Returns(MockGenre);

            var result = _controller.Get(MockGenre.Id);

            Assert.IsType<ObjectResult>(result);
        }

        [Fact]
        public void NotGetByNonExistingId_ReturnNotFound()
        {
            MockGenreService.Setup(r => r.GetById(MockGenre.Id)).Returns(() => null);

            var result = _controller.Get(MockGenre.Id);
            var message = ResultMessageService.GetMessage(result);

            Assert.IsType<NotFoundObjectResult>(result);
            Assert.Contains(ErrorMessages.IdWasNotFoundMessage(MockGenre.Id), message);
        }

        [Fact]
        public void PostWithValidObject()
        {
            MockGenreService.Setup(r => r.Create(It.IsAny<GenreBO>())).Returns((GenreBO newGenre) => newGenre);

            var result = _controller.Post(MockGenre);

            Assert.IsType<CreatedResult>(result);
        }

        [Fact]
        public void NotPostWithInvalidObject_ReturnBadRequest()
        {
            _controller.ModelState.AddModelError("", "");

            var result = _controller.Post(new GenreBO());

            Assert.IsType<BadRequestObjectResult>(result);
        }

        [Fact]
        public void NotPostWithNull_ReturnBadRequest()
        {
            var result = _controller.Post(null);
            var message = ResultMessageService.GetMessage(result);

            Assert.IsType<BadRequestObjectResult>(result);
            Assert.Contains(ErrorMessages.InvalidJSON, message);
        }

        [Fact]
        public void UpdateWithValidObject_ReturnOk()
        {
            MockGenreService.Setup(r => r.Update(It.IsAny<GenreBO>())).Returns((GenreBO updatedGenre) => updatedGenre);

            var result = _controller.Put(MockGenre.Id, MockGenre);
            var message = ResultMessageService.GetMessage(result);

            Assert.IsType<OkObjectResult>(result);
            Assert.Contains("Updated!", message);
        }

        [Fact]
        public void NotUpdateWithNull_ReturnBadRequest()
        {
            var result = _controller.Put(0, null);
            var message = ResultMessageService.GetMessage(result);

            Assert.IsType<BadRequestObjectResult>(result);
            Assert.Contains(ErrorMessages.InvalidJSON, message);
        }

        [Fact]
        public void NotUpdateWithMisMatchingIds_ReturnBadRequest()
        {
            var result = _controller.Put(0, MockGenre);
            var message = ResultMessageService.GetMessage(result);

            Assert.IsType<BadRequestObjectResult>(result);
            Assert.Contains(ErrorMessages.IdDoesNotMatchMessage(0), message);
        }

        [Fact]
        public void NotUpdateWithInvalidObject_ReturnBadRequest()
        {
            _controller.ModelState.AddModelError("", "");

            var result = _controller.Put(0, new GenreBO());

            Assert.IsType<BadRequestObjectResult>(result);
        }

        [Fact]
        public void NotUpdateWithNonExistingId_ReturnNotFound()
        {
            MockGenreService.Setup(r => r.Update(MockGenre)).Returns(() => null);

            var result = _controller.Put(MockGenre.Id, MockGenre);
            var message = ResultMessageService.GetMessage(result);

            Assert.IsType<NotFoundObjectResult>(result);
            Assert.Contains(ErrorMessages.IdWasNotFoundMessage(MockGenre.Id), message);
        }

        [Fact]
        public void DeleteByExistingId_ReturnOk()
        {
            MockGenreService.Setup(r => r.Delete(MockGenre.Id)).Returns(true);

            var result = _controller.Delete(MockGenre.Id);
            var message = ResultMessageService.GetMessage(result);

            Assert.IsType<OkObjectResult>(result);
            //Assert.Contains("Deleted", message);
        }

        [Fact]
        public void NotDeleteByNonExistingId_ReturnNotFound()
        {
            MockGenreService.Setup(r => r.Delete(MockGenre.Id)).Returns(false);

            var result = _controller.Delete(MockGenre.Id);
            var message = ResultMessageService.GetMessage(result);

            Assert.IsType<NotFoundObjectResult>(result);
            Assert.Contains(ErrorMessages.IdWasNotFoundMessage(MockGenre.Id), message);
        }
    }
}