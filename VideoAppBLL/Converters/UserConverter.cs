using VideoAppBLL.BusinessObjects;
using VideoAppDAL.Entities;

namespace VideoAppBLL.Converters
{
    public class UserConverter : IConverter<User, UserBO>
    {
        public User Convert(UserBO entity)
        {
            if (entity == null) return null;
            return new User()
            {
                Id = entity.Id,
                Username = entity.Username,
                Password = entity.Password,
                RoleId = entity.RoleId
            };
        }

        public UserBO Convert(User entity)
        {
            if (entity == null) return null;
            return new UserBO()
            {
                Id = entity.Id,
                Username = entity.Username,
                Password = entity.Password,
                RoleId = entity.RoleId,
                Role = new RoleConverter().Convert(entity.Role)
            };
        }
    }
}