namespace WikiHash.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ChangednameofcolumnArticleLinktoLinkinArticlestable : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Articles", "Link", c => c.String(maxLength: 512));
            DropColumn("dbo.Articles", "ArticleLink");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Articles", "ArticleLink", c => c.String(maxLength: 512));
            DropColumn("dbo.Articles", "Link");
        }
    }
}
