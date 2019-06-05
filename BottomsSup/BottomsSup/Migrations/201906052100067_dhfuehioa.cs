namespace BottomsSup.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class dhfuehioa : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Bars", "Tokens", c => c.Int(nullable: false));
            AddColumn("dbo.Clients", "Tokens", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Clients", "Tokens");
            DropColumn("dbo.Bars", "Tokens");
        }
    }
}
