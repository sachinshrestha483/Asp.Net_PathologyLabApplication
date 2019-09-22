namespace LabReport2.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ff : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.DefaultValues",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        DefaultValueName = c.String(),
                        DefaultalueId = c.Int(),
                        DefaultValueString = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.DefaultValues");
        }
    }
}
