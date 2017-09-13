using VideoAppBLL.BusinessObjects;
using VideoAppDAL.Entities;

namespace VideoAppBLL.Converters
{
    public class UserConverter : IConverter<User, UserBO>
    {
        public User Convert(UserBO entity)
        {
            return new User()
            {
                Id = entity.Id,
                Username = entity.Username,
                Password = entity.Password
            };
        }

        public UserBO Convert(User entity)
        {
            return new UserBO()
            {
                Id = entity.Id,
                Username = entity.Username,
                Password = entity.Password
            };
        }
    }
}