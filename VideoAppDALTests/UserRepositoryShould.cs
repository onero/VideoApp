using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using VideoAppDAL;
using VideoAppDAL.Context;
using VideoAppDAL.Entities;
using VideoAppDAL.Interfaces;
using VideoAppDAL.Repository;
using Xunit;

namespace VideoAppDALTests
{
    public class UserRepositoryShould : ITest
    {
        private const int NonExistingId = 999;

        private readonly InMemoryContext _context;
        private readonly IRepository<User> _repository;

        private static readonly User MockUser = new User()
        {
            Id = 1,
            Username = "Adamino",
            Password = "Secret"
        };

        public UserRepositoryShould()
        {
            _context = new InMemoryContext(new DbContextOptionsBuilder<InMemoryContext>().UseInMemoryDatabase($"{Guid.NewGuid()}").Options);
            _repository = new UserRepository(_context);
        }

        [Fact]
        public void CreateOne()
        {
            var created = _repository.Create(MockUser);

            Assert.NotNull(created);
        }

        private User CreateMockUser()
        {
           var createdUser = _repository.Create(MockUser);
            _context.SaveChanges();
            return createdUser;
        }

        [Fact]
        public void GetAll()
        {
            CreateMockUser();
            var users = _repository.GetAll();

            Assert.NotEmpty(users);
        }

        [Fact]
        public void GetOneByExistingId()
        {
            var createdUser = CreateMockUser();

            var videoFromSearch = _repository.GetById(createdUser.Id);

            Assert.Equal(createdUser, videoFromSearch);
        }

        [Fact]
        public void NotGetOneByNonExistingId()
        {
            CreateMockUser();
            var userFromSearch = _repository.GetById(NonExistingId);
            Assert.Null(userFromSearch);
        }

        [Fact]
        public void GetAllByExistingIds()
        {
            CreateMockUser();
            var ids = new List<int>() {MockUser.Id};
            var genres = _repository.GetAllByIds(ids);
            Assert.NotEmpty(genres);
        }

        [Fact]
        public void NotGetAllByNonExistingIds()
        {
            var ids = new List<int>() {MockUser.Id};
            var genres = _repository.GetAllByIds(ids);
            Assert.Empty(genres);
        }

        [Fact]
        public void DeleteByExistingId()
        {
            var createdUser = CreateMockUser();
            var idOfCreatedUser = createdUser.Id;
            var deleted = _repository.Delete(idOfCreatedUser);
            Assert.True(deleted);
        }

        [Fact]
        public void NotDeleteByNonExistingId()
        {
            var deleted = _repository.Delete(NonExistingId);

            Assert.False(deleted);
        }

        [Fact]
        public void UpdateByExistingId()
        {
            var createdUser = CreateMockUser();
            createdUser.Username = "Awesome";
            createdUser.Password = "MoarSecret";
            var updatedUser = _repository.Update(createdUser);

            Assert.Equal(createdUser, updatedUser);
        }

        [Fact]
        public void NotUpdateByNonExistingId()
        {
            var updatedUser = _repository.Update(MockUser);

            Assert.Null(updatedUser);
        }
    }
}