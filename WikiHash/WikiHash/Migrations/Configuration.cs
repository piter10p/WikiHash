namespace WikiHash.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<WikiHash.DAL.ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(DAL.ApplicationDbContext context)
        {
            GenerateTestArticles(context);
            GenerateTestMedias(context);
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data.
        }

        private void GenerateTestArticles(DAL.ApplicationDbContext context)
        {
            context.Articles.Add(new Models.Articles.Article() { Title = "Test Article", Link = Models.TitleFunctions.GenerateLink("Test Article"), ArticleId = 1});
            context.Articles.AddOrUpdate();
        }

        private void GenerateTestMedias(DAL.ApplicationDbContext context)
        {
            context.Medias.Add(new Models.Medias.Media() { Title = "Test Media", Link = Models.TitleFunctions.GenerateLink("Test Media"), Description = "Some test description of media", MediaId = 1});
            context.Medias.AddOrUpdate();
        }
    }
}
