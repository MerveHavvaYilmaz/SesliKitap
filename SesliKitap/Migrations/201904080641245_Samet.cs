namespace SesliKitap.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Samet : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Uruns", "DinlenmeSayisi", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Uruns", "DinlenmeSayisi");
        }
    }
}
