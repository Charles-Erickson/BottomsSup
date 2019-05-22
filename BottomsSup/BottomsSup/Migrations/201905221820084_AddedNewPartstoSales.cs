namespace BottomsSup.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedNewPartstoSales : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Bars", "Lat", c => c.String());
            AddColumn("dbo.Bars", "Lng", c => c.String());
            AddColumn("dbo.Bars", "PhoneNumber", c => c.String());
            AddColumn("dbo.Sales", "LaborPercentage", c => c.Double(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Sales", "LaborPercentage");
            DropColumn("dbo.Bars", "PhoneNumber");
            DropColumn("dbo.Bars", "Lng");
            DropColumn("dbo.Bars", "Lat");
        }
    }
}
