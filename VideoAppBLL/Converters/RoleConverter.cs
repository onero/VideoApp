using VideoAppBLL.BusinessObjects;
using VideoAppDAL.Entities;

namespace VideoAppBLL.Converters
{
    public class RoleConverter : IConverter<Role, RoleBO>
    {
        public Role Convert(RoleBO entity)
        {
            if (entity == null) return null;
            return new Role()
            {
                Id = entity.Id,
                Name = entity.Name
            };
        }

        public RoleBO Convert(Role entity)
        {
            if (entity == null) return null;
            return new RoleBO()
            {
                Id = entity.Id,
                Name = entity.Name
            };
        }
    }
}