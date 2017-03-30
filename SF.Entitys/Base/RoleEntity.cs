using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

using SF.Entitys.Abstraction;
using System.Collections.Generic;

namespace SF.Entitys
{
    /// <summary>
    /// 角色实体
    /// </summary>
    public class RoleEntity : IdentityRole<long, UserRoleEntity, IdentityRoleClaim<long>>, IEntityWithTypedId<long>
    {
        public RoleEntity()
        {
            RolePermissions = new List<RolePermissionEntity>();
            RoleModules = new List<RoleModuleEntity>();
        }

        public RoleEntity(string name)
        {
            Name = name;
        }

        public long SiteId { get; set; }

        public string Description { get; set; }


        public int Enabled { get; set; }


        public virtual IList<RolePermissionEntity> RolePermissions { get; set; }

        public virtual IList<RoleModuleEntity> RoleModules { get; set; }
    }
}
