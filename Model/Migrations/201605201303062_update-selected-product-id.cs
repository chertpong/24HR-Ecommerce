namespace Model.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class updateselectedproductid : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.SelectedProduct", "Order_Id", "dbo.Order");
            DropForeignKey("dbo.SelectedProduct", "Product_Id", "dbo.Product");
            DropIndex("dbo.SelectedProduct", new[] { "Order_Id" });
            DropIndex("dbo.SelectedProduct", new[] { "Product_Id" });
            RenameColumn(table: "dbo.SelectedProduct", name: "Order_Id", newName: "OrderId");
            RenameColumn(table: "dbo.SelectedProduct", name: "Product_Id", newName: "ProductId");
            AlterColumn("dbo.SelectedProduct", "OrderId", c => c.Int(nullable: false));
            AlterColumn("dbo.SelectedProduct", "ProductId", c => c.Int(nullable: false));
            CreateIndex("dbo.SelectedProduct", "OrderId");
            CreateIndex("dbo.SelectedProduct", "ProductId");
            AddForeignKey("dbo.SelectedProduct", "OrderId", "dbo.Order", "Id", cascadeDelete: true);
            AddForeignKey("dbo.SelectedProduct", "ProductId", "dbo.Product", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.SelectedProduct", "ProductId", "dbo.Product");
            DropForeignKey("dbo.SelectedProduct", "OrderId", "dbo.Order");
            DropIndex("dbo.SelectedProduct", new[] { "ProductId" });
            DropIndex("dbo.SelectedProduct", new[] { "OrderId" });
            AlterColumn("dbo.SelectedProduct", "ProductId", c => c.Int());
            AlterColumn("dbo.SelectedProduct", "OrderId", c => c.Int());
            RenameColumn(table: "dbo.SelectedProduct", name: "ProductId", newName: "Product_Id");
            RenameColumn(table: "dbo.SelectedProduct", name: "OrderId", newName: "Order_Id");
            CreateIndex("dbo.SelectedProduct", "Product_Id");
            CreateIndex("dbo.SelectedProduct", "Order_Id");
            AddForeignKey("dbo.SelectedProduct", "Product_Id", "dbo.Product", "Id");
            AddForeignKey("dbo.SelectedProduct", "Order_Id", "dbo.Order", "Id");
        }
    }
}
