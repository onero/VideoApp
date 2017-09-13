using VideoAppBLL;
using VideoAppBLL.BusinessObjects;
using VideoAppBLL.Interfaces;
using Xunit;

namespace VideoAppBLLTests
{
    public class GenreServiceShould : ITest
    {
        private const int NonExistingId = 999;
        private readonly IGenreService _service;

        private static readonly GenreBO MockGenre = new GenreBO
        {
            Id = 1,
            Name = "Action"
        };

        public GenreServiceShould()
        {
            var bllFacade = new BLLFacade();
            _service = bllFacade.GenreService;
            _service.ClearAll();
        }

        [Fact]
        public void CreateOne()
        {
            var createdGenre = _service.Create(MockGenre);

            Assert.NotNull(createdGenre);
        }

        [Fact]
        public void GetAll()
        {
            _service.Create(MockGenre);
            var genres = _service.GetAll();

            Assert.NotEmpty(genres);
        }

        [Fact]
        public void GetOneByExistingId()
        {
            var createdGenre = _service.Create(MockGenre);
            var idOfProfile = createdGenre.Id;
            var profileById = _service.GetById(idOfProfile);

            Assert.Equal(createdGenre, profileById);
        }

        [Fact]
        public void NotGetOneByNonExistingId()
        {
            var genre = _service.GetById(NonExistingId);

            Assert.Null(genre);
        }

        [Fact]
        public void DeleteByExistingId()
        {
            var createdGenre = _service.Create(MockGenre);

            var deleted = _service.Delete(createdGenre.Id);

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
            var createdGenre = _service.Create(MockGenre);
            createdGenre.Name = "Test";
            var updatedVideo = _service.Update(createdGenre);
            Assert.Equal(createdGenre, updatedVideo);
        }

        [Fact]
        public void NotUpdateByNonExistingId()
        {
            var result = _service.Update(MockGenre);

            Assert.Null(result);
        }
    }
}