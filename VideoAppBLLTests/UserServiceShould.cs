using System.Collections.Generic;
using VideoAppBLL;
using VideoAppBLL.BusinessObjects;
using VideoAppBLL.Interfaces;
using Xunit;

namespace VideoAppBLLTests
{
    public class UserServiceShould : ITest
    {
        private readonly IUserService _service;
        private const int NonExistingId = 999;

        private static readonly UserBO MockUser = new UserBO()
        {
            Id = 1,
            Username = "Adamino",
            Password = "Secret"
        };

        public UserServiceShould()
        {
            var bllFacade = new BLLFacade();
            _service = bllFacade.UserService;
            _service.ClearAll();
        }
        [Fact]
        public void CreateOne()
        {
            var created = _service.Create(MockUser);

            Assert.NotNull(created);
        }

        [Fact]
        public void GetAll()
        {
            _service.Create(MockUser);
            var users = _service.GetAll();

            Assert.NotEmpty(users);
        }

        [Fact]
        public void GetOneByExistingId()
        {
            var createdUser = _service.Create(MockUser);

            var videoFromSearch = _service.GetById(createdUser.Id);

            Assert.Equal(createdUser, videoFromSearch);
        }

        [Fact]
        public void NotGetOneByNonExistingId()
        {
            _service.Create(MockUser);
            var userFromSearch = _service.GetById(NonExistingId);
            Assert.Null(userFromSearch);
        }

        [Fact]
        public void GetAllByExistingIds()
        {
            _service.Create(MockUser);
            var ids = new List<int>() { MockUser.Id };
            var genres = _service.GetAllByIds(ids);
            Assert.NotEmpty(genres);
        }

        [Fact]
        public void NotGetAllByNonExistingIds()
        {
            var ids = new List<int>() { MockUser.Id };
            var genres = _service.GetAllByIds(ids);
            Assert.Empty(genres);
        }

        [Fact]
        public void DeleteByExistingId()
        {
            var createdUser = _service.Create(MockUser);
            var idOfCreatedUser = createdUser.Id;
            var deleted = _service.Delete(idOfCreatedUser);
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
            var createdUser = _service.Create(MockUser);
            createdUser.Username = "Awesome";
            createdUser.Password = "MoarSecret";
            var updatedUser = _service.Update(createdUser);

            Assert.Equal(createdUser, updatedUser);
        }

        [Fact]
        public void NotUpdateByNonExistingId()
        {
            _service.Create(MockUser);
            _service.ClearAll();
            var videos = _service.GetAll();

            Assert.Empty(videos);
        }
    }
}