namespace BottomsSup.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class disjhodj : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Friends", "FirstName", c => c.String());
            AddColumn("dbo.Friends", "LastName", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Friends", "LastName");
            DropColumn("dbo.Friends", "FirstName");
        }
    }
}
