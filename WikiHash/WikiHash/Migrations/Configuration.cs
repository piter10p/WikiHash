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
            if(context.Articles.Count() == 0)
            {
                context.Articles.Add(new Models.Articles.Article() { Title = "Test Article" });
                context.Articles.AddOrUpdate();
            }
        }

        private void GenerateTestMedias(DAL.ApplicationDbContext context)
        {
            if (context.Medias.Count() == 0)
            {
                context.Medias.Add(new Models.Medias.Media() { Title = "Test Media", Description = "Some test description of media", FileName = "test-media.jpg" });
                context.Medias.Add(new Models.Medias.Media() { Title = "Nyan Cat", Description = "Get Nyaned! A video media example", FileName = "nyan cat.mp4" });
                context.Medias.Add(new Models.Medias.Media() { Title = "Programming", Description = "A gif media example", FileName = "programming.gif" });
                context.Medias.AddOrUpdate();
            }
        }
    }
}
