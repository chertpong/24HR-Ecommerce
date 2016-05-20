namespace Model.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class initial : DbMigration
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
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Payment",
                c => new
                    {
                        Id = c.Int(nullable: false),
                        PaymentMethod = c.Int(nullable: false),
                        Paid = c.Boolean(nullable: false),
                        Attachment = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Order", t => t.Id)
                .Index(t => t.Id);
            
            CreateTable(
                "dbo.SelectedProduct",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Amount = c.Int(nullable: false),
                        Price = c.Double(nullable: false),
                        CalculatePrice = c.Double(nullable: false),
                        Order_Id = c.Int(),
                        Product_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Order", t => t.Order_Id)
                .ForeignKey("dbo.Product", t => t.Product_Id)
                .Index(t => t.Order_Id)
                .Index(t => t.Product_Id);
            
            CreateTable(
                "dbo.Product",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Price = c.Double(nullable: false),
                        Description = c.String(),
                        Thumbnail = c.String(),
                        Amount = c.Int(nullable: false),
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
            
            CreateTable(
                "dbo.Post",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Title = c.String(),
                        Content = c.String(),
                        Thumbnail = c.String(),
                        UpdatedTime = c.DateTime(nullable: false),
                        CreatedTime = c.DateTime(nullable: false),
                        Priority = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.TagProduct",
                c => new
                    {
                        Tag_Id = c.Int(nullable: false),
                        Product_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Tag_Id, t.Product_Id })
                .ForeignKey("dbo.Tag", t => t.Tag_Id, cascadeDelete: true)
                .ForeignKey("dbo.Product", t => t.Product_Id, cascadeDelete: true)
                .Index(t => t.Tag_Id)
                .Index(t => t.Product_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.SelectedProduct", "Product_Id", "dbo.Product");
            DropForeignKey("dbo.TagProduct", "Product_Id", "dbo.Product");
            DropForeignKey("dbo.TagProduct", "Tag_Id", "dbo.Tag");
            DropForeignKey("dbo.SelectedProduct", "Order_Id", "dbo.Order");
            DropForeignKey("dbo.Payment", "Id", "dbo.Order");
            DropIndex("dbo.TagProduct", new[] { "Product_Id" });
            DropIndex("dbo.TagProduct", new[] { "Tag_Id" });
            DropIndex("dbo.SelectedProduct", new[] { "Product_Id" });
            DropIndex("dbo.SelectedProduct", new[] { "Order_Id" });
            DropIndex("dbo.Payment", new[] { "Id" });
            DropTable("dbo.TagProduct");
            DropTable("dbo.Post");
            DropTable("dbo.Tag");
            DropTable("dbo.Product");
            DropTable("dbo.SelectedProduct");
            DropTable("dbo.Payment");
            DropTable("dbo.Order");
        }
    }
}
