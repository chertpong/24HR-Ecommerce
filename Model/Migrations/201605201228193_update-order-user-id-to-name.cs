namespace Model.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class updateorderuseridtoname : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Order", "Username", c => c.String());
            DropColumn("dbo.Order", "UserId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Order", "UserId", c => c.String());
            DropColumn("dbo.Order", "Username");
        }
    }
}
