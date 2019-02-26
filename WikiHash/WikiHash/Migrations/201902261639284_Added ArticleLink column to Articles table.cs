namespace WikiHash.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedArticleLinkcolumntoArticlestable : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Articles", "ArticleLink", c => c.String(maxLength: 512));
            AlterColumn("dbo.Articles", "Title", c => c.String(maxLength: 256));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Articles", "Title", c => c.String(maxLength: 128));
            DropColumn("dbo.Articles", "ArticleLink");
        }
    }
}
