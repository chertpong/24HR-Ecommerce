namespace Model.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddPost : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Post",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        title = c.String(),
                        content = c.String(),
                        Thumbnail = c.String(),
                        UpdatedTime = c.DateTime(nullable: false),
                        CreatedTime = c.DateTime(nullable: false),
                        Priority = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Post");
        }
    }
}
