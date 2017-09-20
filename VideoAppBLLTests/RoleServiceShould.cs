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
    public class RoleServiceShould : AServiceTest
    {
        private readonly Mock<IRepository<Role>> MockRoleRepository = new Mock<IRepository<Role>>(MockBehavior.Strict);
        private readonly IRoleService _service;

        public RoleServiceShould()
        {
            MockUOW.Setup(uow => uow.RoleRepository).Returns(MockRoleRepository.Object);
            _service = new RoleService(MockDALFacade.Object);
        }

        private const int NonExistingId = 99;

        private readonly RoleBO MockRoleBO = new RoleBO()
        {
            Id = 1,
            Name = "Admin"
        };

        private readonly Role MockRole = new Role()
        {
            Id = 1,
            Name = "Admin"
        };

        [Fact]
        public override void CreateOne()
        {
            MockRoleRepository.Setup(r => r.Create(It.IsAny<Role>())).Returns(MockRole);
            var entity = _service.Create(MockRoleBO);

            Assert.NotNull(entity);
        }

        [Fact]
        public override void GetAll()
        {
            MockRoleRepository.Setup(r => r.GetAll()).Returns(new List<Role>() {MockRole});

            var entities = _service.GetAll();

            Assert.NotEmpty(entities);
        }

        [Fact]
        public override void GetOneByExistingId()
        {
            MockRoleRepository.Setup(r => r.GetById(MockRoleBO.Id)).Returns(MockRole);

            var entity = _service.GetById(MockRoleBO.Id);

            Assert.NotNull(entity);
        }

        [Fact]
        public override void NotGetOneByNonExistingId()
        {
            MockRoleRepository.Setup(r => r.GetById(NonExistingId)).Returns(() => null);

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
            var existingIds = new List<int>(){MockRole.Id};

            MockRoleRepository.Setup(r => r.GetAllByIds(existingIds)).Returns(new List<Role>() {MockRole});

            var entities = _service.GetAllByIds(existingIds);

            Assert.NotEmpty(entities);
        }

        [Fact]
        public override void NotGetAllByNonExistingIds()
        {
            MockRoleRepository.Setup(r => r.GetAllByIds(new List<int>(){NonExistingId})).Returns(() => new List<Role>());

            var entities = _service.GetAllByIds(new List<int> {NonExistingId});

            Assert.Empty(entities);
        }

        [Fact]
        public override void DeleteByExistingId()
        {
            MockRoleRepository.Setup(r => r.GetById(MockRole.Id)).Returns(MockRole);
            MockRoleRepository.Setup(r => r.Delete(MockRole.Id)).Returns(true);

            var deleted = _service.Delete(MockRole.Id);

            Assert.True(deleted);
        }

        [Fact]
        public override void NotDeleteByNonExistingId()
        {
            MockRoleRepository.Setup(r => r.GetById(NonExistingId)).Returns(() => null);

            var deleted = _service.Delete(NonExistingId);

            Assert.False(deleted);
        }

        [Fact]
        public override void UpdateByExistingId()
        {
            MockRoleRepository.Setup(r => r.GetById(It.IsAny<int>())).Returns(MockRole);
            MockRoleRepository.Setup(r => r.Update(It.IsAny<Role>())).Returns((Role roleToUpdate) => roleToUpdate);

            MockRoleBO.Name = "MoqAdmin";
            var updatedRole = _service.Update(MockRoleBO);

            Assert.Equal(MockRoleBO, updatedRole);
        }

        [Fact]
        public override void NotUpdateByNonExistingId()
        {
            MockRoleRepository.Setup(r => r.GetById(NonExistingId)).Returns(() => null);

            var entity = _service.Update(new RoleBO(){Id = NonExistingId});

            Assert.Null(entity);
        }
    }
}