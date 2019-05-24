namespace BottomsSup.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class updatedModels : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Clients", "CreditCard", c => c.Int(nullable: false));
            AlterColumn("dbo.Clients", "FirstName", c => c.String(nullable: false));
            AlterColumn("dbo.Clients", "LastName", c => c.String(nullable: false));
            AlterColumn("dbo.Sales", "TotalSales", c => c.Double(nullable: false));
            AlterColumn("dbo.Sales", "TotalLabor", c => c.Double(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Sales", "TotalLabor", c => c.String());
            AlterColumn("dbo.Sales", "TotalSales", c => c.String());
            AlterColumn("dbo.Clients", "LastName", c => c.String());
            AlterColumn("dbo.Clients", "FirstName", c => c.String());
            DropColumn("dbo.Clients", "CreditCard");
        }
    }
}
