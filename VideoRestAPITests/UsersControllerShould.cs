using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Moq;
using VideoAppBLL.BusinessObjects;
using VideoAppBLL.Interfaces;
using VideoRestAPI.Controllers;
using Xunit;

namespace VideoRestAPITests
{
    public class UsersControllerShould : IControllerTest
    {
        private readonly Mock<IUserService> MockUserService = new Mock<IUserService>(MockBehavior.Strict);
        private readonly UsersController _controller;

        public UsersControllerShould()
        {
            _controller = new UsersController(MockUserService.Object);
        }

        private UserBO MockUser = new UserBO()
        {
            Id = 1,
            Username = "Adamino",
            Password = "Secret"
        };

        [Fact]
        public void GetAll()
        {
            MockUserService.Setup(r => r.GetAll()).Returns(new List<UserBO>(){MockUser});

            var result = _controller.Get();

            Assert.NotEmpty(result);
        }

        [Fact]
        public void GetByExistingId()
        {
            MockUserService.Setup(r => r.GetById(MockUser.Id)).Returns(MockUser);

            var result = _controller.Get(MockUser.Id);

            Assert.IsType<ObjectResult>(result);
        }

        [Fact]
        public void NotGetByNonExistingId_ReturnNotFound()
        {
            MockUserService.Setup(r => r.GetById(0)).Returns(() => null);

            var result = _controller.Get(0);
            var message = RequestObjectResultMessage.GetMessage(result);

            Assert.IsType<NotFoundObjectResult>(result);
            Assert.Contains(ErrorMessages.IdWasNotFoundMessage(0), message);
        }

        [Fact]
        public void PostWithValidObject()
        {
            MockUserService.Setup(r => r.Create(It.IsAny<UserBO>())).Returns((UserBO newUser) => newUser);

            var result = _controller.Post(MockUser);

            Assert.IsType<CreatedResult>(result);
        }

        [Fact]
        public void NotPostWithInvalidObject_ReturnBadRequest()
        {
            _controller.ModelState.AddModelError("", "");

            var result = _controller.Post(new UserBO());

            Assert.IsType<BadRequestObjectResult>(result);
        }

        [Fact]
        public void NotPostWithNull_ReturnBadRequest()
        {
            var result = _controller.Post(null);
            var message = RequestObjectResultMessage.GetMessage(result);

            Assert.IsType<BadRequestObjectResult>(result);
            Assert.Contains(ErrorMessages.InvalidJSON, message);
        }

        [Fact]
        public void UpdateWithValidObject_ReturnOk()
        {
            MockUserService.Setup(r => r.Update(It.IsAny<UserBO>())).Returns((UserBO updated) => updated);

            var result = _controller.Put(MockUser.Id, MockUser);

            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public void NotUpdateWithNull_ReturnBadRequest()
        {
            var result = _controller.Put(0, null);

            var message = RequestObjectResultMessage.GetMessage(result);

            Assert.IsType<BadRequestObjectResult>(result);
            Assert.Contains(ErrorMessages.InvalidJSON, message);
        }

        [Fact]
        public void NotUpdateWithMisMatchingIds_ReturnBadRequest()
        {
            var result = _controller.Put(0, MockUser);
            var message = RequestObjectResultMessage.GetMessage(result);

            Assert.IsType<BadRequestObjectResult>(result);
            Assert.Contains(ErrorMessages.IdDoesNotMatchMessage(0), message);
        }

        [Fact]
        public void NotUpdateWithInvalidObject_ReturnBadRequest()
        {
            _controller.ModelState.AddModelError("", "");

            var result = _controller.Put(0, new UserBO());

            Assert.IsType<BadRequestObjectResult>(result);
        }

        [Fact]
        public void NotUpdateWithNonExistingId_ReturnNotFound()
        {
            MockUserService.Setup(r => r.Update(It.IsAny<UserBO>())).Returns(() => null);

            var result = _controller.Put(MockUser.Id, MockUser);
            var message = RequestObjectResultMessage.GetMessage(result);

            Assert.IsType<NotFoundObjectResult>(result);
            Assert.Contains(ErrorMessages.IdWasNotFoundMessage(MockUser.Id), message);
        }

        [Fact]
        public void DeleteByExistingId_ReturnOk()
        {
            MockUserService.Setup(r => r.Delete(MockUser.Id)).Returns(true);

            var result = _controller.Delete(MockUser.Id);
            var message = RequestObjectResultMessage.GetMessage(result);

            Assert.IsType<OkObjectResult>(result);
            Assert.Contains("Deleted", message);
        }

        [Fact]
        public void NotDeleteByNonExistingId_ReturnNotFound()
        {
            MockUserService.Setup(r => r.Delete(MockUser.Id)).Returns(false);

            var result = _controller.Delete(MockUser.Id);
            var message = RequestObjectResultMessage.GetMessage(result);

            Assert.IsType<NotFoundObjectResult>(result);
            Assert.Contains(ErrorMessages.IdWasNotFoundMessage(MockUser.Id), message);
        }
    }
}