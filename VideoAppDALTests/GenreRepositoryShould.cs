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
    public class GenreRepositoryShould : ITest
    {
        private const int NonExistingId = 999;

        private readonly InMemoryContext _context;
        private readonly IRepository<Genre> _repository;

        private static readonly Genre MockGenre = new Genre
        {
            Id = 1,
            Name = "Action",
        };

        public GenreRepositoryShould()
        {
            _context = new InMemoryContext(new DbContextOptionsBuilder<InMemoryContext>().UseInMemoryDatabase($"{Guid.NewGuid()}").Options);
            _repository = new GenreRepository(_context);
        }

        private Genre CreateMockGenre()
        {
            var genre = _repository.Create(MockGenre);
            _context.SaveChanges();
            return genre;
        }

        [Fact]
        public void CreateOne()
        {
            var createdGenre = CreateMockGenre();

            Assert.NotNull(createdGenre);
        }

        [Fact]
        public void GetAll()
        {
            CreateMockGenre();
            var genres = _repository.GetAll();

            Assert.NotEmpty(genres);
        }

        [Fact]
        public void GetOneByExistingId()
        {
            var createdGenre = CreateMockGenre();
            var idOfProfile = createdGenre.Id;
            var profileById = _repository.GetById(idOfProfile);

            Assert.Equal(createdGenre, profileById);
        }

        [Fact]
        public void NotGetOneByNonExistingId()
        {
            var genre = _repository.GetById(NonExistingId);

            Assert.Null(genre);
        }

        [Fact]
        public void GetAllByExistingIds()
        {
            CreateMockGenre();
            var ids = new List<int>() {MockGenre.Id};
            var genres = _repository.GetAllByIds(ids);
            Assert.NotEmpty(genres);
        }

        [Fact]
        public void NotGetAllByNonExistingIds()
        {
            var ids = new List<int>() { MockGenre.Id };
            var genres = _repository.GetAllByIds(ids);
            Assert.Empty(genres);
        }

        [Fact]
        public void DeleteByExistingId()
        {
            var createdGenre = CreateMockGenre();

            var deleted = _repository.Delete(createdGenre.Id);

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
            var createdGenre = _repository.Create(MockGenre);
            createdGenre.Name = "Test";
            var updatedVideo = _repository.Update(createdGenre);
            Assert.Equal(createdGenre, updatedVideo);
        }

        [Fact]
        public void NotUpdateByNonExistingId()
        {
            var result = _repository.Update(MockGenre);

            Assert.Null(result);
        }
    }
}