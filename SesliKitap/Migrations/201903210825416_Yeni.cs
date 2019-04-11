namespace SesliKitap.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Yeni : DbMigration
    {
        public override void Up()
        {
            RenameColumn(table: "dbo.Uruns", name: "Users_Id", newName: "UserID");
            RenameIndex(table: "dbo.Uruns", name: "IX_Users_Id", newName: "IX_UserID");
            AddColumn("dbo.AspNetUsers", "İsim", c => c.String());
            AddColumn("dbo.AspNetUsers", "Soyisim", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.AspNetUsers", "Soyisim");
            DropColumn("dbo.AspNetUsers", "İsim");
            RenameIndex(table: "dbo.Uruns", name: "IX_UserID", newName: "IX_Users_Id");
            RenameColumn(table: "dbo.Uruns", name: "UserID", newName: "Users_Id");
        }
    }
}
