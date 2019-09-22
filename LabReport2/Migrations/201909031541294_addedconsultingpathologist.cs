namespace LabReport2.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addedconsultingpathologist : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ConsultingPathologists",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Degree = c.String(),
                        PostName = c.String(),
                        RegNo = c.Int(),
                        DigitalSignaturePhotoaddress = c.String(),
                    })
                .PrimaryKey(t => t.id);
            
            AddColumn("dbo.Reports", "DigitalSignature", c => c.Boolean(nullable: false));
            AddColumn("dbo.Reports", "ConsultingPathologistId", c => c.Int(nullable: false));
            CreateIndex("dbo.Reports", "ConsultingPathologistId");
            AddForeignKey("dbo.Reports", "ConsultingPathologistId", "dbo.ConsultingPathologists", "id", cascadeDelete: false);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Reports", "ConsultingPathologistId", "dbo.ConsultingPathologists");
            DropIndex("dbo.Reports", new[] { "ConsultingPathologistId" });
            DropColumn("dbo.Reports", "ConsultingPathologistId");
            DropColumn("dbo.Reports", "DigitalSignature");
            DropTable("dbo.ConsultingPathologists");
        }
    }
}
