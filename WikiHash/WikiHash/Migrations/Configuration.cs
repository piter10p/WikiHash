namespace WikiHash.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using WikiHash.Models;

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
            if (!context.Roles.Any(r => r.Name == RoleNames.Admin))
            {
                var role = new IdentityRole { Name = RoleNames.Admin };
                var store = new RoleStore<IdentityRole>(context);
                var manager = new RoleManager<IdentityRole>(store);
                manager.Create(role);
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
                manager.AddToRole(user.Id, RoleNames.Admin);
            }
        }
    }
}
