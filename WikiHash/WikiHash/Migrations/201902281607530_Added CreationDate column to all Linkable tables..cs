namespace WikiHash.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedCreationDatecolumntoallLinkabletables : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Articles", "CreationDate", c => c.DateTime(nullable: false));
            AddColumn("dbo.Media", "CreationDate", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Media", "CreationDate");
            DropColumn("dbo.Articles", "CreationDate");
        }
    }
}
