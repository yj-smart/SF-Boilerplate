using System.Collections.Generic;
using System.Reflection;
using System.Threading.Tasks;

namespace SF.Web.Security.AuthorizationHandlers.Custom
{
    public interface IAuthorizationHelper
    {
        Task AuthorizeAsync(IEnumerable<ISFAuthorizeAttribute> authorizeAttributes);

        Task AuthorizeAsync(MethodInfo methodInfo);
    }
}
