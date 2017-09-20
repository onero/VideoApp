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
    public class GenreServiceShould : AServiceTest
    {
        private readonly Mock<IRepository<Genre>> MockGenreRepository = new Mock<IRepository<Genre>>(MockBehavior.Strict);
        private readonly IGenreService _service;

        public GenreServiceShould()
        {
            MockUOW.Setup(uow => uow.GenreRepository).Returns(MockGenreRepository.Object);
            _service = new GenreService(MockDALFacade.Object);
        }

        private readonly Genre MockGenre = new Genre()
        {
            Id = 1,
            Name = "Action"
        };

        private readonly GenreBO MockGenreBO = new GenreBO()
        {
            Id = 1,
            Name = "Action"
        };

        [Fact]
        public override void CreateOne()
        {
            MockGenreRepository.Setup(r => r.Create(It.IsAny<Genre>())).Returns(MockGenre);

            var entity = _service.Create(MockGenreBO);

            Assert.NotNull(entity);
        }

        [Fact]
        public override void GetAll()
        {
            MockGenreRepository.Setup(r => r.GetAll()).Returns(new List<Genre>() {MockGenre});

            var entities = _service.GetAll();

            Assert.NotEmpty(entities);
        }

        [Fact]
        public override void GetOneByExistingId()
        {
            MockGenreRepository.Setup(r => r.GetById(MockGenreBO.Id)).Returns(MockGenre);

            var entity = _service.GetById(MockGenreBO.Id);

            Assert.NotNull(entity);
        }

        [Fact]
        public override void NotGetOneByNonExistingId()
        {
            MockGenreRepository.Setup(r => r.GetById(0)).Returns(() => null);

            var entity = _service.GetById(0);

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
            var existingIds = new List<int>() {MockGenreBO.Id};
            MockGenreRepository.Setup(r => r.GetAllByIds(existingIds)).Returns(new List<Genre>() {MockGenre});

            var entities = _service.GetAllByIds(existingIds);

            Assert.NotEmpty(entities);
        }

        [Fact]
        public override void NotGetAllByNonExistingIds()
        {
            MockGenreRepository.Setup(r => r.GetAllByIds(It.IsAny<List<int>>())).Returns(new List<Genre>());

            var entity = _service.GetAllByIds(new List<int>() {0});

            Assert.Empty(entity);
        }

        [Fact]
        public override void DeleteByExistingId()
        {
            MockGenreRepository.Setup(r => r.GetById(MockGenreBO.Id)).Returns(MockGenre);
            MockGenreRepository.Setup(r => r.Delete(MockGenreBO.Id)).Returns(true);

            var delete = _service.Delete(MockGenre.Id);

            Assert.True(delete);
        }

        [Fact]
        public override void NotDeleteByNonExistingId()
        {
            MockGenreRepository.Setup(r => r.GetById(0)).Returns(() => null);

            var delete = _service.Delete(0);

            Assert.False(delete);
        }

        [Fact]
        public override void UpdateByExistingId()
        {
            MockGenreRepository.Setup(r => r.GetById(MockGenreBO.Id)).Returns(() => MockGenre);
            MockGenreRepository.Setup(r => r.Update(It.IsAny<Genre>())).Returns((Genre updatedGenre) => updatedGenre);

            MockGenreBO.Name = "Romance";
            var entity = _service.Update(MockGenreBO);

            Assert.Equal(MockGenreBO, entity);
        }

        [Fact]
        public override void NotUpdateByNonExistingId()
        {
            MockGenreRepository.Setup(r => r.GetById(MockGenreBO.Id)).Returns(() => null);
            var entity = _service.Update(MockGenreBO);

            Assert.Null(entity);
        }
    }
}