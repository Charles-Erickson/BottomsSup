namespace BottomsSup.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class sjadnao : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Friends",
                c => new
                    {
                        FriendId = c.Int(nullable: false, identity: true),
                        ClientId = c.Int(nullable: false),
                        FriendsId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.FriendId)
                .ForeignKey("dbo.Clients", t => t.ClientId, cascadeDelete: false)
                .ForeignKey("dbo.Clients", t => t.FriendsId, cascadeDelete: false)
                .Index(t => t.ClientId)
                .Index(t => t.FriendsId);
            
            DropColumn("dbo.Clients", "CreditCard");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Clients", "CreditCard", c => c.Int(nullable: false));
            DropForeignKey("dbo.Friends", "FriendsId", "dbo.Clients");
            DropForeignKey("dbo.Friends", "ClientId", "dbo.Clients");
            DropIndex("dbo.Friends", new[] { "FriendsId" });
            DropIndex("dbo.Friends", new[] { "ClientId" });
            DropTable("dbo.Friends");
        }
    }
}
