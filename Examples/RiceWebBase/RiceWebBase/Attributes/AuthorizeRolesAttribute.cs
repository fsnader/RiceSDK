using System;
using Microsoft.AspNetCore.Authorization;

namespace RiceWebBase.Attributes
{
    /// <summary>
    /// Attribute to determine roles that can access a controller/action
    /// </summary>
    public class AuthorizeRolesAttribute : AuthorizeAttribute
    {
        /// <summary>
        /// Attribute to determine roles that can access a controller/action
        /// </summary>
        /// <param name="roles"></param>
        public AuthorizeRolesAttribute(params string[] roles)
        {
            Roles = String.Join(",", roles);
        }
    }
}