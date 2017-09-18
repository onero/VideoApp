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
    public class RentalRepositoryShould : ITest
    {
        private const int NonExistingId = 999;

        private readonly InMemoryContext _context;
        private readonly IRepository<Rental> _repository;

        private static readonly Rental MockRental = new Rental
        {
            Video = new Video
            {
                Id = 1,
                Title = "Die Hard Test"
            }
        };

        public RentalRepositoryShould()
        {
            _context = new InMemoryContext(new DbContextOptionsBuilder<InMemoryContext>().UseInMemoryDatabase($"{Guid.NewGuid()}").Options);
            _repository = new RentalRepository(_context);
        }

        private Rental CreateMockVideo()
        {
            var rental = _repository.Create(MockRental);
            _context.SaveChanges();
            return rental;
        }

        [Fact]
        public void CreateOne()
        {
            var createdRental = CreateMockVideo();

            Assert.NotNull(createdRental);
        }

        [Fact]
        public void DeleteByExistingId()
        {
            var createdRental = CreateMockVideo();

            var deleted = _repository.Delete(createdRental.Id);

            Assert.True(deleted);
        }

        [Fact]
        public void GetAll()
        {
            CreateMockVideo();
            var rentals = _repository.GetAll();

            Assert.NotEmpty(rentals);
        }

        [Fact]
        public void GetAllByExistingIds()
        {
            var rental = CreateMockVideo();
            var ids = new List<int> {rental.Id};
            var genres = _repository.GetAllByIds(ids);
            Assert.NotEmpty(genres);
        }

        [Fact]
        public void GetOneByExistingId()
        {
            var createdRental = CreateMockVideo();

            var result = _repository.GetById(createdRental.Id);

            Assert.Equal(createdRental, result);
        }

        [Fact]
        public void NotDeleteByNonExistingId()
        {
            var deleted = _repository.Delete(NonExistingId);

            Assert.False(deleted);
        }

        [Fact]
        public void NotGetAllByNonExistingIds()
        {
            var ids = new List<int> {MockRental.Id};
            var genres = _repository.GetAllByIds(ids);
            Assert.Empty(genres);
        }

        [Fact]
        public void NotGetOneByNonExistingId()
        {
            var result = _repository.GetById(NonExistingId);

            Assert.Null(result);
        }

        [Fact]
        public void NotUpdateByNonExistingId()
        {
            var updatedRental = _repository.Update(MockRental);

            Assert.Null(updatedRental);
        }

        [Fact]
        public void UpdateByExistingId()
        {
            var createdRental = CreateMockVideo();

            createdRental.To = createdRental.To.AddDays(7);

            var updatedRental = _repository.Update(createdRental);

            Assert.Equal(createdRental, updatedRental);
        }
    }
}