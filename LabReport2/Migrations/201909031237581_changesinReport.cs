namespace LabReport2.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class changesinReport : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Reports", "TestId", "dbo.Tests");
            DropIndex("dbo.Reports", new[] { "TestId" });
            AddColumn("dbo.Reports", "TestTitleId", c => c.Int(nullable: false));
            CreateIndex("dbo.Reports", "TestTitleId");
            AddForeignKey("dbo.Reports", "TestTitleId", "dbo.TestTitles", "Id", cascadeDelete: true);
            DropColumn("dbo.Reports", "TestId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Reports", "TestId", c => c.Int(nullable: false));
            DropForeignKey("dbo.Reports", "TestTitleId", "dbo.TestTitles");
            DropIndex("dbo.Reports", new[] { "TestTitleId" });
            DropColumn("dbo.Reports", "TestTitleId");
            CreateIndex("dbo.Reports", "TestId");
            AddForeignKey("dbo.Reports", "TestId", "dbo.Tests", "Id", cascadeDelete: true);
        }
    }
}
