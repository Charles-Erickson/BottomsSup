namespace BottomsSup.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class oieunfhowuh : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Bars", "DrinkList", c => c.String());
            AddColumn("dbo.Bars", "Open", c => c.DateTime(nullable: false));
            AddColumn("dbo.Bars", "Close", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Bars", "Close");
            DropColumn("dbo.Bars", "Open");
            DropColumn("dbo.Bars", "DrinkList");
        }
    }
}
