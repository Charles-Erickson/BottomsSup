namespace BottomsSup.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class djaidjo : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Clients", "SelectedDrink", c => c.String());
            DropColumn("dbo.Clients", "FriendBool");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Clients", "FriendBool", c => c.Boolean(nullable: false));
            DropColumn("dbo.Clients", "SelectedDrink");
        }
    }
}
