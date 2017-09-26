using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Moq;
using VideoAppBLL.BusinessObjects;
using VideoAppBLL.Interfaces;
using VideoRestAPI.Controllers;
using Xunit;

namespace VideoRestAPITests
{
    public class RolesControllerShould : IControllerTest
    {
        private readonly Mock<IRoleService> MockRoleService = new Mock<IRoleService>(MockBehavior.Strict);
        private readonly RolesController _controller;

        public RolesControllerShould()
        {
            _controller = new RolesController(MockRoleService.Object);
        }

        private RoleBO MockRole = new RoleBO()
        {
            Id = 1,
            Name = "Admin"
        };

        [Fact]
        public void GetAll()
        {
            MockRoleService.Setup(r => r.GetAll()).Returns(new List<RoleBO>{MockRole});

            var result = _controller.Get();

            Assert.NotEmpty(result);
        }

        [Fact]
        public void GetByExistingId()
        {
            MockRoleService.Setup(r => r.GetById(MockRole.Id)).Returns(MockRole);

            var result = _controller.Get(MockRole.Id);

            Assert.IsType<ObjectResult>(result);
        }

        [Fact]
        public void NotGetByNonExistingId_ReturnNotFound()
        {
            MockRoleService.Setup(r => r.GetById(0)).Returns(() => null);

            var result = _controller.Get(0);
            var message = ResultMessageService.GetMessage(result);

            Assert.IsType<NotFoundObjectResult>(result);
            Assert.Contains(ErrorMessages.IdWasNotFoundMessage(0), message);
        }

        [Fact]
        public void PostWithValidObject()
        {
            MockRoleService.Setup(r => r.Create(It.IsAny<RoleBO>())).Returns((RoleBO newRole) => newRole);

            var result = _controller.Post(MockRole);

            Assert.IsType<CreatedResult>(result);
        }

        [Fact]
        public void NotPostWithInvalidObject_ReturnBadRequest()
        {
            _controller.ModelState.AddModelError("", "");

            var result = _controller.Post(new RoleBO());

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
            MockRoleService.Setup(r => r.Update(It.IsAny<RoleBO>())).Returns((RoleBO updatedRole) => updatedRole);

            var result = _controller.Put(MockRole.Id, MockRole);
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
            var result = _controller.Put(0, MockRole);
            var message = ResultMessageService.GetMessage(result);

            Assert.IsType<BadRequestObjectResult>(result);
            Assert.Contains(ErrorMessages.IdDoesNotMatchMessage(0), message);
        }

        [Fact]
        public void NotUpdateWithInvalidObject_ReturnBadRequest()
        {
            _controller.ModelState.AddModelError("", "");
            
            var result = _controller.Put(0, new RoleBO());

            Assert.IsType<BadRequestObjectResult>(result);
        }

        [Fact]
        public void NotUpdateWithNonExistingId_ReturnNotFound()
        {
            MockRoleService.Setup(r => r.Update(MockRole)).Returns(() => null);

            var result = _controller.Put(MockRole.Id, MockRole);
            var message = ResultMessageService.GetMessage(result);

            Assert.IsType<NotFoundObjectResult>(result);
            Assert.Contains(ErrorMessages.IdWasNotFoundMessage(MockRole.Id), message);
        }

        [Fact]
        public void DeleteByExistingId_ReturnOk()
        {
            MockRoleService.Setup(r => r.Delete(MockRole.Id)).Returns(true);

            var result = _controller.Delete(MockRole.Id);
            var message = ResultMessageService.GetMessage(result);

            Assert.IsType<OkObjectResult>(result);
            Assert.Contains("Deleted", message);
        }

        [Fact]
        public void NotDeleteByNonExistingId_ReturnNotFound()
        {
            MockRoleService.Setup(r => r.Delete(0)).Returns(false);

            var result = _controller.Delete(0);
            var message = ResultMessageService.GetMessage(result);

            Assert.IsType<NotFoundObjectResult>(result);
            Assert.Contains(ErrorMessages.IdWasNotFoundMessage(0), message);
        }
    }
}