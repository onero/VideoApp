using VideoAppBLL;
using VideoAppBLL.BusinessObjects;
using VideoAppBLL.Interfaces;
using Xunit;

namespace VideoAppBLLTests
{
    public class RentalServiceShould
    {
        private readonly IRentalService _service;

        public RentalServiceShould()
        {
            var bllFacade = new BLLFacade();
            _service = bllFacade.RentalService;
            _service.ClearAll();
        }

        [Fact]
        public void CreateOneRental()
        {
            var createdRental = _service.Create(new RentalBO());

            Assert.NotNull(createdRental);
        }

        [Fact]
        public void GetAllRentals()
        {
            _service.Create(new RentalBO());
            var rentals = _service.GetAll();

            Assert.NotEmpty(rentals);
        }
    }
}