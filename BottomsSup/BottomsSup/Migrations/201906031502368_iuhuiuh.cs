namespace BottomsSup.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class iuhuiuh : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Sales", "FirstDateToCompare", c => c.DateTime(nullable: false));
            AddColumn("dbo.Sales", "SecondDateToCompare", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Sales", "SecondDateToCompare");
            DropColumn("dbo.Sales", "FirstDateToCompare");
        }
    }
}
