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

        protected override void Seed(WikiHash.DAL.ApplicationDbContext context)
        {
            GenerateTestArticles(context);
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data.
        }

        private void GenerateTestArticles(WikiHash.DAL.ApplicationDbContext context)
        {
            context.Articles.Add(new Models.Articles.Article() { Title = "Test Article", Link = "test-article", ArticleId = 1});
            context.Articles.AddOrUpdate();
        }
    }
}
