namespace WikiHash.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedFileNamecolumntoMediasDatabase : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Media", "FileName", c => c.String(maxLength: 256));
            CreateIndex("dbo.Media", "FileName", unique: true);
        }
        
        public override void Down()
        {
            DropIndex("dbo.Media", new[] { "FileName" });
            DropColumn("dbo.Media", "FileName");
        }
    }
}
