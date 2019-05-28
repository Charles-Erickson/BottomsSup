namespace BottomsSup.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class njew : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Clients", "FriendName", c => c.String());
            AddColumn("dbo.Clients", "FriendBool", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Clients", "FriendBool");
            DropColumn("dbo.Clients", "FriendName");
        }
    }
}
