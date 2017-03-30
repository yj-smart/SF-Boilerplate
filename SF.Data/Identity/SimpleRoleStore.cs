using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using SF.Entitys;
using System.Security.Claims;

namespace SF.Data.Identity
{
    /// <summary>
    /// 为角色创建持久性存储的新实例
    /// </summary>
    public class SimpleRoleStore : RoleStore<RoleEntity, CoreDbContext, long, UserRoleEntity, IdentityRoleClaim<long>>
    {
        public SimpleRoleStore(CoreDbContext context) : base(context)
        {
        }

        protected override IdentityRoleClaim<long> CreateRoleClaim(RoleEntity role, Claim claim)
        {
            return new IdentityRoleClaim<long> { RoleId = role.Id, ClaimType = claim.Type, ClaimValue = claim.Value };
        }
    }
}
