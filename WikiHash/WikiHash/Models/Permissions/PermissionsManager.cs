using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WikiHash.DAL;

namespace WikiHash.Models.Permissions
{
    public static class PermissionsManager
    {
        public static Permission CreatePermission(Permission permission)
        {
            try
            {
                var context = ApplicationDbContext.Create();
                var rolesManager = GetRolesManager(context);

                var role = rolesManager.FindByName(permission.RoleName);

                if (role == null)
                    throw new KeyNotFoundException();

                var output = context.Permissions.Add(permission);
                context.SaveChanges();

                return output;
            }
            catch
            {
                throw;
            }
        }

        public static bool RoleHasPermission(string roleName, PermissionTarget permissionTarget)
        {
            try
            {
                var context = ApplicationDbContext.Create();

                var rolePermissionsQuery = from p in context.Permissions where p.RoleName == roleName select p;

                foreach(var permission in rolePermissionsQuery)
                {
                    if (permission.PermissionTarget == permissionTarget)
                        return true;
                }

                return false;
            }
            catch
            {
                throw;
            }
        }

        private static RoleManager<IdentityRole> GetRolesManager(ApplicationDbContext context)
        {
            var store = new RoleStore<IdentityRole>(context);
            return new RoleManager<IdentityRole>(store);
        }
    }
}