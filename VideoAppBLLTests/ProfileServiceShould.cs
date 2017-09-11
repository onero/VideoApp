using VideoAppBLL;
using VideoAppBLL.BusinessObjects;
using VideoAppBLL.Interfaces;
using VideoAppDAL.Entities;
using Xunit;

namespace VideoAppBLLTests
{
    public class ProfileServiceShould
    {
        private readonly IProfileService _service;

        private readonly ProfileBO _mockProfile = new ProfileBO()
        {
            Id = 1,
            FirstName = "Adamino",
            LastName = "Hansen",
            Address = "Secret"
        };
        public ProfileServiceShould()
        {
            var bllFacade = new BLLFacade();
            _service = bllFacade.ProfileService;
            _service.ClearAll();
        }

        [Fact]
        public void CreateOneProfile()
        {
            var createdProfile = _service.Create(_mockProfile);

            Assert.NotNull(createdProfile);
        }

        [Fact]
        public void GetAllProfiles()
        {
            _service.Create(_mockProfile);
            var profiles = _service.GetAll();

            Assert.NotEmpty(profiles);
        }

        [Fact]
        public void GetProfileByExistingId()
        {
            var createdProfile = _service.Create(_mockProfile);
            var idOfProfile = createdProfile.Id;
            var profileById = _service.GetById(idOfProfile);

            Assert.Equal(createdProfile, profileById);
        }

        [Fact]
        public void FailGetProfileByNonExistingId()
        {
            const int nonExistingId = 0;
            var profile = _service.GetById(nonExistingId);

            Assert.Null(profile);
        }

        [Fact]
        public void DeleteVideoById()
        {
            var createdProfile = _service.Create(_mockProfile);

            var profileDeleted = _service.Delete(createdProfile.Id);

            Assert.True(profileDeleted);
        }

        [Fact]
        public void UpdateVideo()
        {
            var createdVideo = _service.Create(_mockProfile);
            createdVideo.FirstName = "Changed!";

            var updatedVideo = _service.Update(createdVideo);

            Assert.Equal(createdVideo, updatedVideo);
        }
    }
}