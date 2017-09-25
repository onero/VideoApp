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
            throw new System.NotImplementedException();
        }

        [Fact]
        public void NotPostWithInvalidObject_ReturnBadRequest()
        {
            throw new System.NotImplementedException();
        }

        [Fact]
        public void NotPostWithNull_ReturnBadRequest()
        {
            throw new System.NotImplementedException();
        }

        [Fact]
        public void UpdateWithValidObject_ReturnOk()
        {
            throw new System.NotImplementedException();
        }

        [Fact]
        public void NotUpdateWithNull_ReturnBadRequest()
        {
            throw new System.NotImplementedException();
        }

        [Fact]
        public void NotUpdateWithMisMatchingIds_ReturnBadRequest()
        {
            throw new System.NotImplementedException();
        }

        [Fact]
        public void NotUpdateWithInvalidObject_ReturnBadRequest()
        {
            throw new System.NotImplementedException();
        }

        [Fact]
        public void NotUpdateWithNonExistingId_ReturnNotFound()
        {
            throw new System.NotImplementedException();
        }

        [Fact]
        public void DeleteByExistingId_ReturnOk()
        {
            throw new System.NotImplementedException();
        }

        [Fact]
        public void NotDeleteByNonExistingId_ReturnNotFound()
        {
            throw new System.NotImplementedException();
        }
    }
}