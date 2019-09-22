namespace LabReport2.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class MadeTest : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Tests",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        SubtestTitleId = c.Int(nullable: false),
                        Name = c.String(nullable: false, maxLength: 200),
                        MaxValue = c.Int(),
                        MinValue = c.Int(),
                        Unit = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.SubtestTitles", t => t.SubtestTitleId, cascadeDelete: true)
                .Index(t => t.SubtestTitleId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Tests", "SubtestTitleId", "dbo.SubtestTitles");
            DropIndex("dbo.Tests", new[] { "SubtestTitleId" });
            DropTable("dbo.Tests");
        }
    }
}
