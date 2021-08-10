namespace OnlineStoreDAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class deleteshoppingcart : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Orders", "Shoppingcart_Id", "dbo.ShoppingCarts");
            DropIndex("dbo.Orders", new[] { "Shoppingcart_Id" });
            DropColumn("dbo.Orders", "Shoppingcart_Id");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Orders", "Shoppingcart_Id", c => c.Int());
            CreateIndex("dbo.Orders", "Shoppingcart_Id");
            AddForeignKey("dbo.Orders", "Shoppingcart_Id", "dbo.ShoppingCarts", "Id");
        }
    }
}
