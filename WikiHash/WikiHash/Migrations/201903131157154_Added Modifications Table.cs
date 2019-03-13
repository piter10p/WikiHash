namespace WikiHash.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedModificationsTable : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Modifications",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ArticleId = c.Int(nullable: false),
                        AuthorIp = c.String(nullable: false, maxLength: 15),
                        UserEmail = c.String(),
                        CreationDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Articles", t => t.ArticleId, cascadeDelete: true)
                .Index(t => t.ArticleId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Modifications", "ArticleId", "dbo.Articles");
            DropIndex("dbo.Modifications", new[] { "ArticleId" });
            DropTable("dbo.Modifications");
        }
    }
}
