using VideoAppBLL.BusinessObjects;
using VideoAppDAL.Entities;

namespace VideoAppBLL.Converters
{
    internal class ProfileConverter : IConverter<Profile, ProfileBO>
    {
        /// <summary>
        /// Convert Profile
        /// </summary>
        /// <param name="profile"></param>
        /// <returns>Profile BO</returns>
        public ProfileBO Convert(Profile profile)
        {
            return new ProfileBO()
            {
                Id = profile.Id,
                FirstName = profile.FirstName,
                LastName = profile.LastName,
                Address = profile.Address
            };
        }

        /// <summary>
        /// Convert ProfileBO
        /// </summary>
        /// <param name="profileBO"></param>
        /// <returns>Profile</returns>
        public Profile Convert(ProfileBO profileBO)
        {
            return new Profile()
            {
                Id = profileBO.Id,
                FirstName = profileBO.FirstName,
                LastName = profileBO.LastName,
                Address = profileBO.Address
            };
        }
    }
}