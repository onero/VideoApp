using VideoAppBLL.BusinessObjects;
using VideoAppDAL.Entities;

namespace VideoAppBLL.Converters
{
    public class RoleConverter : IConverter<Role, RoleBO>
    {
        public Role Convert(RoleBO entity)
        {
            return new Role()
            {
                Id = entity.Id,
                Name = entity.Name
            };
        }

        public RoleBO Convert(Role entity)
        {
            return new RoleBO()
            {
                Id = entity.Id,
                Name = entity.Name
            };
        }
    }
}