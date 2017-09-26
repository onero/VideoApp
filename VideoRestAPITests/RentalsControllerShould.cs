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
    public class RentalsControllerShould : IControllerTest
    {
        private readonly Mock<IRentalService> MockRentalService = new Mock<IRentalService>(MockBehavior.Strict);
        private readonly RentalsController _controller;

        public RentalsControllerShould()
        {
            _controller = new RentalsController(MockRentalService.Object);
        }

        private readonly RentalBO MockRental = new RentalBO()
        {
            Id = 1,
            VideoId = 1,
            UserId = 1
        };

        [Fact]
        public void DeleteByExistingId_ReturnOk()
        {
            MockRentalService.Setup(r => r.Delete(MockRental.Id)).Returns(true);

            var result = _controller.Delete(MockRental.Id);
            var message = ResultMessageService.GetMessage(result);

            Assert.IsType<OkObjectResult>(result);
            Assert.Contains("Deleted", message);
        }

        [Fact]
        public void GetAll()
        {
            MockRentalService.Setup(r => r.GetAll()).Returns(new List<RentalBO>() {MockRental});

            var result = _controller.Get();

            Assert.NotEmpty(result);
        }

        [Fact]
        public void GetByExistingId()
        {
            MockRentalService.Setup(r => r.GetById(MockRental.Id)).Returns(MockRental);

            var result = _controller.Get(MockRental.Id);

            Assert.IsType<ObjectResult>(result);
        }

        [Fact]
        public void NotDeleteByNonExistingId_ReturnNotFound()
        {
            MockRentalService.Setup(r => r.Delete(0)).Returns(false);

            var result = _controller.Delete(0);
            var message = ResultMessageService.GetMessage(result);

            Assert.IsType<NotFoundObjectResult>(result);
            Assert.Contains(ErrorMessages.IdWasNotFoundMessage(0), message);
        }

        [Fact]
        public void NotGetByNonExistingId_ReturnNotFound()
        {
            MockRentalService.Setup(r => r.GetById(0)).Returns(() => null);

            var result = _controller.Get(0);
            var message = ResultMessageService.GetMessage(result);

            Assert.IsType<NotFoundObjectResult>(result);
            Assert.Contains(ErrorMessages.IdWasNotFoundMessage(0), message);
        }

        [Fact]
        public void NotPostWithInvalidObject_ReturnBadRequest()
        {
            _controller.ModelState.AddModelError("", "");

            var result = _controller.Post(new RentalBO());

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
        public void NotUpdateWithInvalidObject_ReturnBadRequest()
        {
            _controller.ModelState.AddModelError("", "");

            var result = _controller.Put(MockRental.Id, MockRental);

            Assert.IsType<BadRequestObjectResult>(result);
        }

        [Fact]
        public void NotUpdateWithMisMatchingIds_ReturnBadRequest()
        {
            var result = _controller.Put(0, MockRental);
            var message = ResultMessageService.GetMessage(result);

            Assert.IsType<BadRequestObjectResult>(result);
            Assert.Contains(ErrorMessages.IdDoesNotMatchMessage(0), message);
        }

        [Fact]
        public void NotUpdateWithNonExistingId_ReturnNotFound()
        {
            MockRentalService.Setup(r => r.Update(MockRental)).Returns(() => null);

            var result = _controller.Put(MockRental.Id, MockRental);
            var message = ResultMessageService.GetMessage(result);

            Assert.IsType<NotFoundObjectResult>(result);
            Assert.Contains(ErrorMessages.IdWasNotFoundMessage(MockRental.Id), message);
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
        public void PostWithValidObject()
        {
            MockRentalService.Setup(r => r.Create(It.IsAny<RentalBO>())).Returns(MockRental);

            var result = _controller.Post(MockRental);

            Assert.IsType<CreatedResult>(result);
        }

        [Fact]
        public void UpdateWithValidObject_ReturnOk()
        {
            MockRentalService.Setup(r => r.Update(MockRental)).Returns(MockRental);

            var result = _controller.Put(MockRental.Id, MockRental);
            var message = ResultMessageService.GetMessage(result);

            Assert.IsType<OkObjectResult>(result);
            Assert.Contains("Updated!", message);
        }
    }
}