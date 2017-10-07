using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Moq;
using VideoAppBLL.BusinessObjects;
using VideoAppBLL.Interfaces;
using VideoRestAPI.Controllers;
using Xunit;

namespace VideoRestAPITests
{
    public class ProfilesControllerShould : IControllerTest
    {
        private readonly Mock<IProfileService> MockProfileService = new Mock<IProfileService>(MockBehavior.Strict);
        private readonly ProfilesController _controller;

        public ProfilesControllerShould()
        {
            _controller = new ProfilesController(MockProfileService.Object);
        }

        private readonly ProfileBO MockProfile = new ProfileBO()
        {
            Id = 1,
            FirstName = "Adamino",
            LastName = "DaMan",
            Address = "Secret"
        };

        

        [Fact]
        public void GetAll()
        {
            MockProfileService.Setup(r => r.GetAll()).Returns(new List<ProfileBO>(){MockProfile});

            var result = _controller.Get();

            Assert.NotEmpty(result);
        }

        [Fact]
        public void GetByExistingId()
        {
            MockProfileService.Setup(r => r.GetById(MockProfile.Id)).Returns(MockProfile);

            var result = _controller.Get(MockProfile.Id);

            Assert.IsType<ObjectResult>(result);
        }

        [Fact]
        public void NotGetByNonExistingId_ReturnNotFound()
        {
            MockProfileService.Setup(r => r.GetById(0)).Returns(() => null);

            var result = _controller.Get(0);
            var message = ResultMessageService.GetMessage(result);

            Assert.IsType<NotFoundObjectResult>(result);
            Assert.Contains(ErrorMessages.IdWasNotFoundMessage(0), message);
        }

        [Fact]
        public void PostWithValidObject()
        {
            MockProfileService.Setup(r => r.Create(It.IsAny<ProfileBO>())).Returns((ProfileBO newProfile) => newProfile);

            var result = _controller.Post(MockProfile);

            Assert.IsType<CreatedResult>(result);
        }

        [Fact]
        public void NotPostWithInvalidObject_ReturnBadRequest()
        {
            _controller.ModelState.AddModelError("", "");

            var result = _controller.Post(new ProfileBO());

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
            MockProfileService.Setup(r => r.Update(It.IsAny<ProfileBO>())).Returns((ProfileBO updatedProfile) => updatedProfile);

            var result = _controller.Put(MockProfile.Id, MockProfile);
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
            var result = _controller.Put(0, MockProfile);
            var message = ResultMessageService.GetMessage(result);

            Assert.IsType<BadRequestObjectResult>(result);
            Assert.Contains(ErrorMessages.IdDoesNotMatchMessage(0), message);
        }

        [Fact]
        public void NotUpdateWithInvalidObject_ReturnBadRequest()
        {
            _controller.ModelState.AddModelError("", "");

            var result = _controller.Put(0, new ProfileBO());

            Assert.IsType<BadRequestObjectResult>(result);
        }

        [Fact]
        public void NotUpdateWithNonExistingId_ReturnNotFound()
        {
            MockProfileService.Setup(r => r.Update(MockProfile)).Returns(() => null);

            var result = _controller.Put(MockProfile.Id, MockProfile);
            var message = ResultMessageService.GetMessage(result);

            Assert.IsType<NotFoundObjectResult>(result);
            Assert.Contains(ErrorMessages.IdWasNotFoundMessage(MockProfile.Id), message);
        }

        [Fact]
        public void DeleteByExistingId_ReturnOk()
        {
            MockProfileService.Setup(r => r.Delete(MockProfile.Id)).Returns(true);

            var result = _controller.Delete(MockProfile.Id);
            var message = ResultMessageService.GetMessage(result);

            Assert.IsType<OkObjectResult>(result);
            //Assert.Contains("Deleted", message);
        }

        [Fact]
        public void NotDeleteByNonExistingId_ReturnNotFound()
        {
            MockProfileService.Setup(r => r.Delete(MockProfile.Id)).Returns(false);

            var result = _controller.Delete(MockProfile.Id);
            var message = ResultMessageService.GetMessage(result);

            Assert.IsType<NotFoundObjectResult>(result);
            Assert.Contains(ErrorMessages.IdWasNotFoundMessage(MockProfile.Id), message);
        }
    }
}