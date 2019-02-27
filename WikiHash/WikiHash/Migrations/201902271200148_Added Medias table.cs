namespace WikiHash.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedMediastable : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Media",
                c => new
                    {
                        MediaId = c.Int(nullable: false, identity: true),
                        Title = c.String(maxLength: 128),
                        Description = c.String(maxLength: 256),
                        Link = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.MediaId);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Media");
        }
    }
}
