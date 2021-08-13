namespace OnlineStoreDAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class vhangemodeluseraddorderlist : DbMigration
    {
        public override void Up()
        {
            RenameColumn(table: "dbo.Orders", name: "User_Id", newName: "ApplicationUser_Id");
            RenameIndex(table: "dbo.Orders", name: "IX_User_Id", newName: "IX_ApplicationUser_Id");
        }
        
        public override void Down()
        {
            RenameIndex(table: "dbo.Orders", name: "IX_ApplicationUser_Id", newName: "IX_User_Id");
            RenameColumn(table: "dbo.Orders", name: "ApplicationUser_Id", newName: "User_Id");
        }
    }
}
