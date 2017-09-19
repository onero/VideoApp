using System.Collections.Generic;
using System.Runtime.InteropServices.ComTypes;
using Moq;
using VideoAppBLL.BusinessObjects;
using VideoAppBLL.Interfaces;
using VideoAppBLL.Service;
using VideoAppDAL.Entities;
using VideoAppDAL.Interfaces;
using Xunit;

namespace VideoAppBLLTests
{
    public class RentalServiceShould : AServiceTest
    {
        private readonly Mock<IRepository<Rental>> MockRentalRepository = new Mock<IRepository<Rental>>(MockBehavior.Strict);
        private readonly IRentalService _service;

        public RentalServiceShould()
        {
            MockUOW.Setup(uow => uow.RentalRepository).Returns(MockRentalRepository.Object);
            _service = new RentalService(MockDALFacade.Object);
        }

        private readonly Rental MockRental = new Rental()
        {
            Id = 1,
            UserId = 1,
            User = new User()
            {
                Id = 1,
                Username = "Test",
                Password = "Secure",
                RoleId = 1,
                Role = new Role()
                {
                    Id = 1,
                    Name = "Admin"
                }
            },
            VideoId = 1,
            Video = new Video()
            {
                Id = 1,
                Title = "Die Hard"
            }
        };

        private readonly RentalBO MockRentalBO = new RentalBO()
        {
            Id = 1,
            UserId = 1,
            User = new UserBO()
            {
                Id = 1,
                Username = "Test",
                Password = "Secure",
                RoleId = 1
            },
            VideoId = 1,
            Video = new VideoBO()
            {
                Id = 1,
                Title = "Die Hard"
            }
        };

        [Fact]
        public override void CreateOne()
        {
            MockRentalRepository.Setup(r => r.Create(It.IsAny<Rental>())).Returns(MockRental);

            var entity = _service.Create(MockRentalBO);

            Assert.NotNull(entity);
        }

        [Fact]
        public override void GetAll()
        {
            MockRentalRepository.Setup(r => r.GetAll()).Returns(new List<Rental>(){MockRental});

            var entities = _service.GetAll();

            Assert.NotEmpty(entities);
        }

        [Fact]
        public override void GetOneByExistingId()
        {
            // Rental needs to contact VideoRepo to get rented video
            var mockVideoRepository = new Mock<IRepository<Video>>();
            MockUOW.Setup(uow => uow.VideoRepository).Returns(mockVideoRepository.Object);
            // Rental needs to contact UserRepo to get user who rented
            var mockUserRepository = new Mock<IRepository<User>>();
            MockUOW.Setup(uow => uow.UserRepository).Returns(mockUserRepository.Object);

            // Setup mock repos to respond with valid data
            MockRentalRepository.Setup(r => r.GetById(MockRentalBO.Id)).Returns(MockRental);
            mockVideoRepository.Setup(vr => vr.GetById(It.IsAny<int>())).Returns(
                new Video() {Id = 1, Title = "Die Hard"});
            mockUserRepository.Setup(gr => gr.GetById(It.IsAny<int>())).Returns(
                new User(){Id = 1, Username = "Admin", Password = "God", RoleId = 1, Role = new Role()});

            var entity = _service.GetById(MockRentalBO.Id);

            Assert.NotNull(entity);
        }

        [Fact]
        public void OnlyGetRentalByExistingId_WhenVideoAndUserDoNotExist()
        {
            // Rental needs to contact VideoRepo to get rented video
            var mockVideoRepository = new Mock<IRepository<Video>>();
            MockUOW.Setup(uow => uow.VideoRepository).Returns(mockVideoRepository.Object);
            // Rental needs to contact UserRepo to get user who rented
            var mockUserRepository = new Mock<IRepository<User>>();
            MockUOW.Setup(uow => uow.UserRepository).Returns(mockUserRepository.Object);

            // Setup mock repos to respond with valid data
            MockRentalRepository.Setup(r => r.GetById(MockRentalBO.Id)).Returns(
                new Rental(){Id = 1, UserId = 1, VideoId = 1});
            mockVideoRepository.Setup(vr => vr.GetById(It.IsAny<int>())).Returns(() => null);
            mockUserRepository.Setup(gr => gr.GetById(It.IsAny<int>())).Returns(() => null);

            var entity = _service.GetById(MockRentalBO.Id);

            Assert.NotNull(entity);
            Assert.Null(entity.User);
            Assert.Null(entity.Video);
        }

        [Fact]
        public void OnlyGetRentalWithVideoByExistingId_WhenUserDoNotExist()
        {
            // Rental needs to contact VideoRepo to get rented video
            var mockVideoRepository = new Mock<IRepository<Video>>();
            MockUOW.Setup(uow => uow.VideoRepository).Returns(mockVideoRepository.Object);
            // Rental needs to contact UserRepo to get user who rented
            var mockUserRepository = new Mock<IRepository<User>>();
            MockUOW.Setup(uow => uow.UserRepository).Returns(mockUserRepository.Object);

            // Setup mock repos to respond with valid data
            MockRentalRepository.Setup(r => r.GetById(MockRentalBO.Id)).Returns(
                new Rental() { Id = 1, UserId = 1, VideoId = 1 });
            mockVideoRepository.Setup(vr => vr.GetById(It.IsAny<int>())).Returns(
                new Video() { Id = 1, Title = "Die Hard" });
            mockUserRepository.Setup(gr => gr.GetById(It.IsAny<int>())).Returns(() => null);

            var entity = _service.GetById(MockRentalBO.Id);

            Assert.NotNull(entity);
            Assert.NotNull(entity.Video);
            Assert.Null(entity.User);
        }

        [Fact]
        public void OnlyGetRentalWithUserByExistingId_WhenVideoDoNotExist()
        {
            // Rental needs to contact VideoRepo to get rented video
            var mockVideoRepository = new Mock<IRepository<Video>>();
            MockUOW.Setup(uow => uow.VideoRepository).Returns(mockVideoRepository.Object);
            // Rental needs to contact UserRepo to get user who rented
            var mockUserRepository = new Mock<IRepository<User>>();
            MockUOW.Setup(uow => uow.UserRepository).Returns(mockUserRepository.Object);

            // Setup mock repos to respond with valid data
            MockRentalRepository.Setup(r => r.GetById(MockRentalBO.Id)).Returns(
                new Rental() { Id = 1, UserId = 1, VideoId = 1 });
            mockVideoRepository.Setup(vr => vr.GetById(It.IsAny<int>())).Returns(() => null);
            mockUserRepository.Setup(gr => gr.GetById(It.IsAny<int>())).Returns(
                new User() { Id = 1, Username = "Admin", Password = "God", RoleId = 1, Role = new Role()});

            var entity = _service.GetById(MockRentalBO.Id);

            Assert.NotNull(entity);
            Assert.NotNull(entity.User);
            Assert.Null(entity.Video);
        }

        [Fact]
        public override void NotGetOneByNonExistingId()
        {
            MockRentalRepository.Setup(r => r.GetById(0)).Returns(() => null);

            var entity = _service.GetById(0);

            Assert.Null(entity);
        }

        [Fact]
        public override void NotConvertNullEntity()
        {
            var entity = _service.Create(null);

            Assert.Null(entity);
        }

        [Fact]
        public override void GetAllByExistingIds()
        {
            var existingIds = new List<int>(){MockRental.Id};
            MockRentalRepository.Setup(r => r.GetAllByIds(existingIds)).Returns(new List<Rental>(){MockRental});

            var entities = _service.GetAllByIds(existingIds);

            Assert.NotEmpty(entities);
        }

        [Fact]
        public override void NotGetAllByNonExistingIds()
        {
            var nonExistingIds = new List<int>(){0};
            MockRentalRepository.Setup(r => r.GetAllByIds(nonExistingIds)).Returns(new List<Rental>());

            var entities = _service.GetAllByIds(nonExistingIds);

            Assert.Empty(entities);
        }

        [Fact]
        public override void DeleteByExistingId()
        {
            MockRentalRepository.Setup(r => r.GetById(It.IsAny<int>())).Returns(MockRental);
            MockRentalRepository.Setup(r => r.Delete(MockRental.Id)).Returns(true);

            var deleted = _service.Delete(MockRental.Id);

            Assert.True(deleted);
        }

        [Fact]
        public override void NotDeleteByNonExistingId()
        {
            MockRentalRepository.Setup(r => r.GetById(0)).Returns(() => null);

            var deleted = _service.Delete(0);

            Assert.False(deleted);

        }

        [Fact]
        public override void UpdateByExistingId()
        {
            MockRentalRepository.Setup(r => r.GetById(MockRentalBO.Id)).Returns(MockRental);
            MockRentalRepository.Setup(r => r.Update(MockRental)).Returns((Rental updatedRental) => updatedRental);

            MockRentalBO.Video = new VideoBO(){Id = 2, Title = "Titanic"};
            var updatedEntity = _service.Update(MockRentalBO);

            Assert.Equal(MockRentalBO, updatedEntity);
        }

        [Fact]
        public override void NotUpdateByNonExistingId()
        {
            MockRentalRepository.Setup(r => r.GetById(0)).Returns(() => null);

            var entity = _service.Update(new RentalBO(){Id = 0});

            Assert.Null(entity);
        }
    }
}