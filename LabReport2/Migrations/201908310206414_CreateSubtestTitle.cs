namespace LabReport2.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CreateSubtestTitle : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.SubtestTitles",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                        TestTitleId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.TestTitles", t => t.TestTitleId, cascadeDelete: true)
                .Index(t => t.TestTitleId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.SubtestTitles", "TestTitleId", "dbo.TestTitles");
            DropIndex("dbo.SubtestTitles", new[] { "TestTitleId" });
            DropTable("dbo.SubtestTitles");
        }
    }
}
