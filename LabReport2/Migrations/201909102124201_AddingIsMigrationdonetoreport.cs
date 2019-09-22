namespace LabReport2.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddingIsMigrationdonetoreport : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Reports", "isInvestgationDone", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Reports", "isInvestgationDone");
        }
    }
}
