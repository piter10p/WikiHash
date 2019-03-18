namespace WikiHash.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using WikiHash.Models;
    using WikiHash.Models.Permissions;

    internal sealed class Configuration : DbMigrationsConfiguration<WikiHash.DAL.ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(DAL.ApplicationDbContext context)
        {
            /*if (!System.Diagnostics.Debugger.IsAttached)
                System.Diagnostics.Debugger.Launch();*/

            //Default
            GenerateRoles(context);
            GeneratePermissions(context);
            GenerateUsers(context);

            //Test
            var testCategory = GenerateTestCategories(context);
            GenerateTestArticles(context, testCategory);
            GenerateTestMedias(context, testCategory);

            context.SaveChanges();
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data.
        }

        private void GenerateTestArticles(DAL.ApplicationDbContext context, Models.Category testCategory)
        {
            if (!context.Articles.Any(a => a.Title == "Test Article"))
                context.Articles.AddOrUpdate(new Models.Articles.Article() { Title = "Test Article", Category = testCategory });
        }

        private void GenerateTestMedias(DAL.ApplicationDbContext context, Models.Category testCategory)
        {
            if(!context.Medias.Any( m => m.Title == "Test Media"))
                context.Medias.Add(new Models.Medias.Media() { Title = "Test Media", Description = "Some test description of media", FileName = "test-media.jpg", Category = testCategory });

            if (!context.Medias.Any(m => m.Title == "Nyan Cat"))
                context.Medias.Add(new Models.Medias.Media() { Title = "Nyan Cat", Description = "Get Nyaned! A video media example", FileName = "nyan cat.mp4" });

            if (!context.Medias.Any(m => m.Title == "Programming"))
                context.Medias.Add(new Models.Medias.Media() { Title = "Programming", Description = "A gif media example", FileName = "programming.gif" });
        }

        private Category GenerateTestCategories(DAL.ApplicationDbContext context)
        {
            if (!context.Categories.Any(c => c.CategoryName == "Test category"))
            {
                var category = context.Categories.Add(new Category() { CategoryName = "Test category" });
                context.Categories.AddOrUpdate();
                return category;
            }

            var query = from c in context.Categories where c.CategoryName == "Test category" select c;
            return query.First();
        }

        private void GenerateRoles(DAL.ApplicationDbContext context)
        {
            if (!context.Roles.Any(r => r.Name == DefaultRolesNames.HeadAdmin))
            {
                var role = new IdentityRole { Name = DefaultRolesNames.HeadAdmin };
                var store = new RoleStore<IdentityRole>(context);
                var manager = new RoleManager<IdentityRole>(store);
                manager.Create(role);
            }
        }

        private void GeneratePermissions(DAL.ApplicationDbContext context)
        {
            //Head Admin Permissions
            InsertPermission(context, PermissionTarget.CreatingNewArticles, DefaultRolesNames.HeadAdmin);
            InsertPermission(context, PermissionTarget.ModifyingArticlesData, DefaultRolesNames.HeadAdmin);
            InsertPermission(context, PermissionTarget.ModifyingArticlesBody, DefaultRolesNames.HeadAdmin);
            InsertPermission(context, PermissionTarget.CreatingNewMedias, DefaultRolesNames.HeadAdmin);
            InsertPermission(context, PermissionTarget.ReadingArticles, DefaultRolesNames.HeadAdmin);

            //Global Permissions
            /*InsertPermission(context, PermissionTarget.CreatingNewArticles, null);   TODO: that null throws an Exception. I guess that can be an EF issue.
            InsertPermission(context, PermissionTarget.ModifyingArticlesBody, null);
            InsertPermission(context, PermissionTarget.CreatingNewMedias, null);
            InsertPermission(context, PermissionTarget.ReadingArticles, null);*/
        }

        private void InsertPermission(DAL.ApplicationDbContext context, PermissionTarget permissionTarget, string roleName)
        {
            if (!context.Permissions.Any(p => p.PermissionTarget == permissionTarget && p.RoleName == roleName))
            {
                var permission = new Permission();
                permission.RoleName = roleName;
                permission.PermissionTarget = permissionTarget;
                PermissionsManager.CreatePermission(permission);
            }
        }

        private void GenerateUsers(DAL.ApplicationDbContext context)
        {
            if (!context.Users.Any(u => u.UserName == "admin@admin.admin"))
            {
                var store = new UserStore<ApplicationUser>(context);
                var manager = new UserManager<ApplicationUser>(store);
                var user = new ApplicationUser { UserName = "admin@admin.admin" };

                manager.Create(user, "!Pass123");
                manager.AddToRole(user.Id, DefaultRolesNames.HeadAdmin);
            }
        }
    }
}
