namespace LabReport2.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Addinvestigatedbyonreport : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Reports", "ReportBy", c => c.String());
            AddColumn("dbo.Reports", "InvestigatedBy", c => c.String());
            DropColumn("dbo.Reports", "AddedBy");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Reports", "AddedBy", c => c.String());
            DropColumn("dbo.Reports", "InvestigatedBy");
            DropColumn("dbo.Reports", "ReportBy");
        }
    }
}
