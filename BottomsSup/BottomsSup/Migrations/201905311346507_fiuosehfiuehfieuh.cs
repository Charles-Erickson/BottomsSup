namespace BottomsSup.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class fiuosehfiuehfieuh : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Bars", "Balance", c => c.Double(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Bars", "Balance");
        }
    }
}
