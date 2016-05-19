namespace Model.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class updateproduct : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.SelectedProduct",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Amount = c.Int(nullable: false),
                        Price = c.Double(nullable: false),
                        CalculatePrice = c.Double(nullable: false),
                        Product_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Product", t => t.Product_Id)
                .Index(t => t.Product_Id);
            
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
            DropIndex("dbo.TagProduct", new[] { "Product_Id" });
            DropIndex("dbo.TagProduct", new[] { "Tag_Id" });
            DropIndex("dbo.SelectedProduct", new[] { "Product_Id" });
            DropTable("dbo.TagProduct");
            DropTable("dbo.SelectedProduct");
        }
    }
}
