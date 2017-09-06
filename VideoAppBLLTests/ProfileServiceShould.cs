using VideoAppBLL;
using VideoAppBLL.BusinessObjects;
using VideoAppBLL.Interfaces;
using VideoAppDAL.Entities;
using Xunit;

namespace VideoAppBLLTests
{
    public class ProfileServiceShould
    {
        private readonly IService<ProfileBO> _profileService;

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
            _profileService = bllFacade.ProfileService;
            _profileService.ClearAll();
        }

        [Fact]
        public void CreateOneProfile()
        {
            var createdProfile = _profileService.Create(_mockProfile);

            Assert.NotNull(createdProfile);
        }

        [Fact]
        public void GetAllProfiles()
        {
            _profileService.Create(_mockProfile);
            var profiles = _profileService.GetAll();

            Assert.NotEmpty(profiles);
        }

        [Fact]
        public void GetProfileByExistingId()
        {
            var createdProfile = _profileService.Create(_mockProfile);
            var idOfProfile = createdProfile.Id;
            var profileById = _profileService.GetById(idOfProfile);

            Assert.Equal(createdProfile, profileById);
        }

        [Fact]
        public void FailGetProfileByNonExistingId()
        {
            const int nonExistingId = 0;
            var profile = _profileService.GetById(nonExistingId);

            Assert.Null(profile);
        }

        [Fact]
        public void DeleteVideoById()
        {
            var createdProfile = _profileService.Create(_mockProfile);

            var profileDeleted = _profileService.Delete(createdProfile.Id);

            Assert.True(profileDeleted);
        }

        [Fact]
        public void UpdateVideo()
        {
            var createdVideo = _profileService.Create(_mockProfile);
            createdVideo.FirstName = "Changed!";

            var updatedVideo = _profileService.Update(createdVideo);

            Assert.Equal(createdVideo, updatedVideo);
        }
    }
}