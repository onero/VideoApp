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
    public class RoleRepositoryShould : IRepositoryTest
    {
        private const int NonExistingId = 999;

        private static readonly Role MockRole = new Role()
        {
            Id = 1,
            Name = "Admin"
        };

        private readonly InMemoryContext _context;
        private readonly IRepository<Role> _repository;

        public RoleRepositoryShould()
        {
            _context = new InMemoryContext();
            _repository = new RoleRepository(_context);
        }

        private Role CreateMockEntity()
        {
            var entity = _repository.Create(MockRole);
            _context.SaveChanges();
            return entity;
        }

        [Fact]
        public void CreateOne()
        {
            var createdRole = CreateMockEntity();

            Assert.NotNull(createdRole);
        }

        [Fact]
        public void GetAll()
        {
            CreateMockEntity();
            var roles = _repository.GetAll();

            Assert.NotEmpty(roles);
        }

        [Fact]
        public void GetOneByExistingId()
        {
            var createdRole = CreateMockEntity();

            var result = _repository.GetById(createdRole.Id);

            Assert.Equal(createdRole, result);
        }

        [Fact]
        public void NotGetOneByNonExistingId()
        {
            var result = _repository.GetById(NonExistingId);

            Assert.Null(result);
        }

        [Fact]
        public void GetAllByExistingIds()
        {
            CreateMockEntity();
            var ids = new List<int>() { MockRole.Id };
            var genres = _repository.GetAllByIds(ids);
            Assert.NotEmpty(genres);
        }

        [Fact]
        public void NotGetAllByNonExistingIds()
        {
            var ids = new List<int>() { MockRole.Id };
            var genres = _repository.GetAllByIds(ids);
            Assert.Empty(genres);
        }

        [Fact]
        public void DeleteByExistingId()
        {
            var createdRole = CreateMockEntity();
            var deleted = _repository.Delete(createdRole.Id);

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
            var createdRole = CreateMockEntity();
            createdRole.Name = "Noob";
            var updatedRole = _repository.Update(createdRole);

            Assert.Equal(createdRole, updatedRole);
        }

        [Fact]
        public void NotUpdateByNonExistingId()
        {
            var result = _repository.Update(MockRole);

            Assert.Null(result);
        }
    }
}