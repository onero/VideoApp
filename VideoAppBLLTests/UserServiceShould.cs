using System.Collections.Generic;
using Moq;
using VideoAppBLL.BusinessObjects;
using VideoAppBLL.Interfaces;
using VideoAppBLL.Service;
using VideoAppDAL.Entities;
using VideoAppDAL.Interfaces;
using Xunit;

namespace VideoAppBLLTests
{
    public class UserServiceShould : AServiceTest
    {
        private readonly Mock<IRepository<User>> MockUserRepository = new Mock<IRepository<User>>(MockBehavior.Strict);

        private readonly IUserService _service;


        public UserServiceShould()
        {
            MockUOW.SetupGet(uow => uow.UserRepository).Returns(MockUserRepository.Object);
            _service = new UserService(MockDALFacade.Object);
        }

        private readonly User MockUser = new User()
        {
            Id = 1,
            Username = "Adamino",
            Password = "Secret"
        };

        private readonly UserBO MockUserBO = new UserBO()
        {
            Id = 1,
            Username = "Adamino",
            Password = "Secret"
        };

        private const int NonExistingId = 99;

        [Fact]
        public override void CreateOne()
        {
            MockUserRepository.Setup(r => r.Create(It.IsAny<User>())).Returns(MockUser);

            var entity = _service.Create(MockUserBO);

            Assert.NotNull(entity);
        }

        [Fact]
        public override void GetAll()
        {
            MockUserRepository.Setup(r => r.GetAll()).Returns(new List<User>() {MockUser});

            var entitites = _service.GetAll();

            Assert.NotEmpty(entitites);
        }

        [Fact]
        public override void GetOneByExistingId()
        {
            MockUserRepository.Setup(r => r.GetById(MockUser.Id)).Returns(MockUser);
            var roleRepository = new Mock<IRepository<Role>>();
            MockUOW.Setup(uow => uow.RoleRepository).Returns(roleRepository.Object);
            roleRepository.Setup(rr => rr.GetById(It.IsAny<int>())).Returns(new Role());

            var entity = _service.GetById(MockUserBO.Id);

            Assert.NotNull(entity);
        }

        [Fact]
        public override void NotGetOneByNonExistingId()
        {
            MockUserRepository.Setup(r => r.GetById(NonExistingId)).Returns(() => null);

            var entity = _service.GetById(NonExistingId);

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
            var existingIds = new List<int>() {MockUser.Id};
            MockUserRepository.Setup(r => r.GetAllByIds(existingIds)).Returns(new List<User>() {MockUser});

            var entities = _service.GetAllByIds(existingIds);

            Assert.NotEmpty(entities);
        }

        [Fact]
        public override void NotGetAllByNonExistingIds()
        {
            MockUserRepository.Setup(r => r.GetAllByIds(It.IsAny<List<int>>())).Returns(new List<User>());

            var entities = _service.GetAllByIds(new List<int>());

            Assert.Empty(entities);
        }

        [Fact]
        public override void DeleteByExistingId()
        {
            MockUserRepository.Setup(r => r.GetById(It.IsAny<int>())).Returns(MockUser);
            MockUserRepository.Setup(r => r.Delete(It.IsAny<int>())).Returns(true);

            var deleted = _service.Delete(MockUser.Id);

            Assert.True(deleted);
        }

        [Fact]
        public override void NotDeleteByNonExistingId()
        {
            MockUserRepository.Setup(r => r.GetById(It.IsAny<int>())).Returns(() => null);

            var deleted = _service.Delete(NonExistingId);

            Assert.False(deleted);
        }

        [Fact]
        public override void UpdateByExistingId()
        {
            MockUserRepository.Setup(r => r.GetById(MockUser.Id)).Returns(MockUser);
            MockUserRepository.Setup(r => r.Update(It.IsAny<User>())).Returns((User updatedUser) => updatedUser);

            MockUserBO.Username = "DaMan";
            MockUserBO.Password = "MoarSecret!";
            var entity = _service.Update(MockUserBO);

            Assert.Equal(MockUserBO, entity);
        }

        [Fact]
        public override void NotUpdateByNonExistingId()
        {
            MockUserRepository.Setup(r => r.GetById(It.IsAny<int>())).Returns(() => null);

            var entity = _service.Update(MockUserBO);

            Assert.Null(entity);
        }
    }
}