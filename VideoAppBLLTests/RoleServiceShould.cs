using VideoAppBLL;
using VideoAppBLL.BusinessObjects;
using VideoAppBLL.Interfaces;
using Xunit;

namespace VideoAppBLLTests
{
    public class RoleServiceShould : ITest
    {
        private const int NonExistingId = 999;
        private readonly IRoleService _service;

        private static readonly RoleBO MockRole = new RoleBO()
        {
            Id = 1,
            Name = "Admin"
        };

        public RoleServiceShould()
        {
            var facade = new BLLFacade();
            _service = facade.RoleService;
            _service.ClearAll();
        }

        [Fact]
        public void CreateOne()
        {
            var createdRole = _service.Create(MockRole);

            Assert.NotNull(createdRole);
        }

        [Fact]
        public void GetAll()
        {
            _service.Create(MockRole);
            var roles = _service.GetAll();

            Assert.NotEmpty(roles);
        }

        [Fact]
        public void GetOneByExistingId()
        {
            var createdRole = _service.Create(MockRole);

            var result = _service.GetById(createdRole.Id);

            Assert.Equal(createdRole, result);
        }

        [Fact]
        public void NotGetOneByNonExistingId()
        {
            var result = _service.GetById(NonExistingId);

            Assert.Null(result);
        }

        [Fact]
        public void DeleteByExistingId()
        {
            var createdRole = _service.Create(MockRole);
            var deleted = _service.Delete(createdRole.Id);

            Assert.True(deleted);
        }

        [Fact]
        public void NotDeleteByNonExistingId()
        {
            var deleted = _service.Delete(NonExistingId);

            Assert.False(deleted);
        }

        [Fact]
        public void UpdateByExistingId()
        {
            var createdRole = _service.Create(MockRole);
            createdRole.Name = "Noob";
            var updatedRole = _service.Update(createdRole);

            Assert.Equal(createdRole, updatedRole);
        }

        [Fact]
        public void NotUpdateByNonExistingId()
        {
            var result = _service.Update(MockRole);

            Assert.Null(result);
        }
    }
}