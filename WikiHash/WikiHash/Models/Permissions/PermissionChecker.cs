using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WikiHash.Models.Permissions
{
    public static class PermissionChecker
    {
        public static bool CheckPermission(string username, PermissionTarget permissionTarget)
        {
            try
            {
                if(username == "")
                {
                    if (PermissionsManager.RoleHasPermission(null, permissionTarget))
                        return true;
                }
                else
                {
                    var rolesArray = GetUserRoles(username);

                    foreach (var role in rolesArray)
                    {
                        if (PermissionsManager.RoleHasPermission(role, permissionTarget))
                            return true;
                    }
                }

                return false;
            }
            catch(Exception e)
            {
                throw;
            }
        }

        private static string[] GetUserRoles(string username)
        {
            try
            {
                var context = DAL.ApplicationDbContext.Create();
                var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(context));
                var userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context));
                var userId = userManager.FindByName(username).Id;

                var rolesList = userManager.GetRoles(userId);

                return rolesList.ToArray();
            }
            catch(Exception e)
            {
                throw;
            }
        }
    }
}