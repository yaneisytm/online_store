namespace OnlineStoreDAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _new : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Orders",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Status = c.String(),
                        DeliveryAddress = c.String(),
                        Date = c.DateTime(nullable: false),
                        User_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.User_Id)
                .Index(t => t.User_Id);
            
            AddColumn("dbo.OrderLines", "Order_Id", c => c.Int());
            CreateIndex("dbo.OrderLines", "Order_Id");
            AddForeignKey("dbo.OrderLines", "Order_Id", "dbo.Orders", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Orders", "User_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.OrderLines", "Order_Id", "dbo.Orders");
            DropIndex("dbo.Orders", new[] { "User_Id" });
            DropIndex("dbo.OrderLines", new[] { "Order_Id" });
            DropColumn("dbo.OrderLines", "Order_Id");
            DropTable("dbo.Orders");
        }
    }
}
