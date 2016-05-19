namespace Model.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class updateshoppingcart : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Order",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Created = c.DateTime(nullable: false),
                        Updated = c.DateTime(nullable: false),
                        Note = c.String(),
                        Status = c.Int(nullable: false),
                        TransportationType = c.Int(nullable: false),
                        UserId = c.String(),
                        Payment_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Payment", t => t.Payment_Id)
                .Index(t => t.Payment_Id);
            
            CreateTable(
                "dbo.Payment",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        PaymentMethod = c.Int(nullable: false),
                        Paid = c.Boolean(nullable: false),
                        Attachment = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Tag",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Order", "Payment_Id", "dbo.Payment");
            DropIndex("dbo.Order", new[] { "Payment_Id" });
            DropTable("dbo.Tag");
            DropTable("dbo.Payment");
            DropTable("dbo.Order");
        }
    }
}
