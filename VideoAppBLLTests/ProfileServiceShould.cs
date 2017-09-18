using System.Collections.Generic;
using VideoAppBLL;
using VideoAppBLL.BusinessObjects;
using VideoAppBLL.Interfaces;
using VideoAppDAL.Entities;
using Xunit;

namespace VideoAppBLLTests
{
    public class ProfileServiceShould : ITest
    {
        private const int NonExistingId = 999;
        private readonly IProfileService _service;

        private static readonly ProfileBO MockProfile = new ProfileBO()
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
        public void CreateOne()
        {
            var createdProfile = _service.Create(MockProfile);

            Assert.NotNull(createdProfile);
        }

        [Fact]
        public void GetAll()
        {
            _service.Create(MockProfile);
            var profiles = _service.GetAll();

            Assert.NotEmpty(profiles);
        }

        [Fact]
        public void GetOneByExistingId()
        {
            var createdProfile = _service.Create(MockProfile);
            var idOfProfile = createdProfile.Id;
            var profileById = _service.GetById(idOfProfile);

            Assert.Equal(createdProfile, profileById);
        }

        [Fact]
        public void NotGetOneByNonExistingId()
        {
            var profile = _service.GetById(NonExistingId);

            Assert.Null(profile);
        }

        [Fact]
        public void GetAllByExistingIds()
        {
            _service.Create(MockProfile);
            var ids = new List<int>() { MockProfile.Id };
            var genres = _service.GetAllByIds(ids);
            Assert.NotEmpty(genres);
        }

        [Fact]
        public void NotGetAllByNonExistingIds()
        {
            var ids = new List<int>() { MockProfile.Id };
            var genres = _service.GetAllByIds(ids);
            Assert.Empty(genres);
        }

        [Fact]
        public void DeleteByExistingId()
        {
            var createdProfile = _service.Create(MockProfile);

            var profileDeleted = _service.Delete(createdProfile.Id);

            Assert.True(profileDeleted);
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
            var createdVideo = _service.Create(MockProfile);
            createdVideo.FirstName = "Changed!";

            var updatedVideo = _service.Update(createdVideo);

            Assert.Equal(createdVideo, updatedVideo);
        }

        [Fact]
        public void NotUpdateByNonExistingId()
        {
            var result = _service.Update(MockProfile);

            Assert.Null(result);
        }
    }
}