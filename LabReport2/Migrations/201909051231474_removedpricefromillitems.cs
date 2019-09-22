namespace LabReport2.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class removedpricefromillitems : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Billitems", "Price");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Billitems", "Price", c => c.Int(nullable: false));
        }
    }
}
