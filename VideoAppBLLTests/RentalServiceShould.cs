using System.Collections.Generic;
using VideoAppBLL;
using VideoAppBLL.BusinessObjects;
using VideoAppBLL.Interfaces;
using Xunit;

namespace VideoAppBLLTests
{
    public class RentalServiceShould : ITest
    {
        private const int NonExistingId = 999;
        private readonly IRentalService _service;

        private static RentalBO MockRental = new RentalBO()
        {
            Video = new VideoBO()
            {
                Title = "Die Hard Test"
            }
        };

        public RentalServiceShould()
        {
            var bllFacade = new BLLFacade();
            _service = bllFacade.RentalService;
            _service.ClearAll();
        }

        [Fact]
        public void CreateOne()
        {
            var createdRental = _service.Create(MockRental);

            Assert.NotNull(createdRental);
        }

        [Fact]
        public void GetAll()
        {
            _service.Create(MockRental);
            var rentals = _service.GetAll();

            Assert.NotEmpty(rentals);
        }

        [Fact]
        public void GetOneByExistingId()
        {
            var createdRental = _service.Create(MockRental);

            var result = _service.GetById(createdRental.Id);

            Assert.Equal(createdRental, result);
        }

        [Fact]
        public void NotGetOneByNonExistingId()
        {
            var result = _service.GetById(NonExistingId);

            Assert.Null(result);
        }

        [Fact]
        public void GetAllByExistingIds()
        {
            var rental = _service.Create(MockRental);
            var ids = new List<int>() { rental.Id };
            var genres = _service.GetAllByIds(ids);
            Assert.NotEmpty(genres);
        }

        [Fact]
        public void NotGetAllByNonExistingIds()
        {
            var ids = new List<int>() { MockRental.Id };
            var genres = _service.GetAllByIds(ids);
            Assert.Empty(genres);
        }

        [Fact]
        public void DeleteByExistingId()
        {
            var createdRental = _service.Create(MockRental);

            var deleted = _service.Delete(createdRental.Id);

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
            var createdRental = _service.Create(MockRental);

            createdRental.To = createdRental.To.AddDays(7);

            var updatedRental = _service.Update(createdRental);

            Assert.Equal(createdRental, updatedRental);
        }

        [Fact]
        public void NotUpdateByNonExistingId()
        {
            var updatedRental = _service.Update(MockRental);

            Assert.Null(updatedRental);
        }
    }
}