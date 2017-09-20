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
    public class ProfileRepositoryShould : IRepositoryTest
    {
        private const int NonExistingId = 999;

        private readonly InMemoryContext _context;
        private readonly IRepository<Profile> _repository;

        private static readonly Profile MockProfile = new Profile()
        {
            Id = 1,
            FirstName = "Adamino",
            LastName = "Hansen",
            Address = "Secret"
        };

        public ProfileRepositoryShould()
        {
            _context = new InMemoryContext(new DbContextOptionsBuilder<InMemoryContext>().UseInMemoryDatabase($"{Guid.NewGuid()}").Options);
            _repository = new ProfileRepository(_context);
        }

        private Profile CreateMockEntity()
        {
            var entity = _repository.Create(MockProfile);
            _context.SaveChanges();
            return entity;
        }

        [Fact]
        public void CreateOne()
        {
            var createdProfile = CreateMockEntity();

            Assert.NotNull(createdProfile);
        }

        [Fact]
        public void GetAll()
        {
            CreateMockEntity();
            var profiles = _repository.GetAll();

            Assert.NotEmpty(profiles);
        }

        [Fact]
        public void GetOneByExistingId()
        {
            var createdProfile = CreateMockEntity();
            var idOfProfile = createdProfile.Id;
            var profileById = _repository.GetById(idOfProfile);

            Assert.Equal(createdProfile, profileById);
        }

        [Fact]
        public void NotGetOneByNonExistingId()
        {
            var profile = _repository.GetById(NonExistingId);

            Assert.Null(profile);
        }

        [Fact]
        public void GetAllByExistingIds()
        {
            CreateMockEntity();
            var ids = new List<int>() { MockProfile.Id };
            var genres = _repository.GetAllByIds(ids);
            Assert.NotEmpty(genres);
        }

        [Fact]
        public void NotGetAllByNonExistingIds()
        {
            var ids = new List<int>() { MockProfile.Id };
            var genres = _repository.GetAllByIds(ids);
            Assert.Empty(genres);
        }

        [Fact]
        public void DeleteByExistingId()
        {
            var createdProfile = CreateMockEntity();

            var profileDeleted = _repository.Delete(createdProfile.Id);

            Assert.True(profileDeleted);
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
            var createdVideo = CreateMockEntity();
            createdVideo.FirstName = "Changed!";

            var updatedVideo = _repository.Update(createdVideo);

            Assert.Equal(createdVideo, updatedVideo);
        }

        [Fact]
        public void NotUpdateByNonExistingId()
        {
            var result = _repository.Update(MockProfile);

            Assert.Null(result);
        }
    }
}