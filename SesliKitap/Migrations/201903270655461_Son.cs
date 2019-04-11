namespace SesliKitap.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Son : DbMigration
    {
        public override void Up()
        {
            RenameColumn(table: "dbo.SoruVeCevaps", name: "Users_Id", newName: "UserID");
            RenameIndex(table: "dbo.SoruVeCevaps", name: "IX_Users_Id", newName: "IX_UserID");
        }
        
        public override void Down()
        {
            RenameIndex(table: "dbo.SoruVeCevaps", name: "IX_UserID", newName: "IX_Users_Id");
            RenameColumn(table: "dbo.SoruVeCevaps", name: "UserID", newName: "Users_Id");
        }
    }
}
