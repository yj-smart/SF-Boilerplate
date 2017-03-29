using SF.Entitys.Abstraction;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SF.Entitys
{
    public class PermissionEntity : BaseEntity
    {
        public PermissionEntity()
        {
            RolePermissions = new List<RolePermissionEntity>();
        }

        [Required]
        [StringLength(256)]
        public string Name { get; set; }

        public string ActionAddress { get; set; }

        public string Description { get; set; }

        public long ModuleId { get; set; }

        public virtual IList<RolePermissionEntity> RolePermissions { get; set; }
    }
}
