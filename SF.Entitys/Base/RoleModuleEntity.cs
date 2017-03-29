
using SF.Entitys.Abstraction;

namespace SF.Entitys
{
    public class RoleModuleEntity : BaseEntity
    {
        public RoleModuleEntity()
        {
        }

        public long RoleId { get; set; }
        public long ModuleId { get; set; }
        public RoleEntity Role { get; set; }
        public ModuleEntity Module { get; set; }

    }
}
