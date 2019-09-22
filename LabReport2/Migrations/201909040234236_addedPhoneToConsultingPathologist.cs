namespace LabReport2.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addedPhoneToConsultingPathologist : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ConsultingPathologists", "Phone", c => c.String(maxLength: 12));
        }
        
        public override void Down()
        {
            DropColumn("dbo.ConsultingPathologists", "Phone");
        }
    }
}
