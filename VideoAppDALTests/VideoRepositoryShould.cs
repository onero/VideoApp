using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using VideoAppDAL.Context;
using VideoAppDAL.Entities;
using VideoAppDAL.Interfaces;
using VideoAppDAL.Repository;
using Xunit;

namespace VideoAppDALTests
{
    public class VideoServiceShould : ITest
    {
        private const int NonExistingId = 999;

        private readonly InMemoryContext _context;

        private readonly IRepository<Video> _repository;

        private static readonly Video MockVideo = new Video
        {
            Id = 1,
            Title = "Die Hard"
        };

        public VideoServiceShould()
        {
            _context = new InMemoryContext(new DbContextOptionsBuilder<InMemoryContext>().UseInMemoryDatabase($"{Guid.NewGuid()}").Options);
            _repository = new VideoRepository(_context);
        }

        private Video CreateMockEntity()
        {
            var entity = _repository.Create(MockVideo);
            _context.SaveChanges();
            return entity;
        }

        [Fact]
        public void CreateOne()
        {
            var created = CreateMockEntity();

            Assert.NotNull(created);
        }

        [Fact]
        public void DeleteByExistingId()
        {
            var createdVideo = CreateMockEntity();

            var idOfCreatedVideo = createdVideo.Id;
            var videoDeleted = _repository.Delete(idOfCreatedVideo);
            Assert.True(videoDeleted);
        }

        [Fact]
        public void GetAll()
        {
            CreateMockEntity();
            var videos = _repository.GetAll();

            Assert.NotEmpty(videos);
        }

        [Fact]
        public void GetAllByExistingIds()
        {
            var video = CreateMockEntity();

            var ids = new List<int> {video.Id};
            var genres = _repository.GetAllByIds(ids);
            Assert.NotEmpty(genres);
        }

        [Fact]
        public void GetOneByExistingId()
        {
            var createdVideo = CreateMockEntity();

            var videoFromSearch = _repository.GetById(createdVideo.Id);

            Assert.Equal(createdVideo.Id, videoFromSearch.Id);
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
            var ids = new List<int> {MockVideo.Id};
            var genres = _repository.GetAllByIds(ids);
            Assert.Empty(genres);
        }

        [Fact]
        public void NotGetOneByNonExistingId()
        {
            CreateMockEntity();

            var videoFromSearch = _repository.GetById(NonExistingId);
            Assert.Null(videoFromSearch);
        }

        [Fact]
        public void NotUpdateByNonExistingId()
        {
            var result = _repository.Update(MockVideo);

            Assert.Null(result);
        }

        [Fact]
        public void UpdateByExistingId()
        {
            var createdVideo = CreateMockEntity();

            createdVideo.Title = "Awesome";
            var updatedVideo = _repository.Update(createdVideo);

            Assert.Equal(createdVideo, updatedVideo);
        }
    }
}