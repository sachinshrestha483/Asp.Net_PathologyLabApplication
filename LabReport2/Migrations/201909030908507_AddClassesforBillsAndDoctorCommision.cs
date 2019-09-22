namespace LabReport2.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddClassesforBillsAndDoctorCommision : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Billitems",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        BillId = c.Int(nullable: false),
                        ReportId = c.Int(nullable: false),
                        Price = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.id)
                .ForeignKey("dbo.Bills", t => t.BillId, cascadeDelete: false)
                .ForeignKey("dbo.Reports", t => t.ReportId, cascadeDelete: false)
                .Index(t => t.BillId)
                .Index(t => t.ReportId);
            
            CreateTable(
                "dbo.Bills",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        DoctorId = c.Int(nullable: false),
                        PatientId = c.Int(nullable: false),
                        Date = c.DateTime(nullable: false),
                        BillBy = c.String(),
                        Discount = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Doctors", t => t.DoctorId, cascadeDelete: false)
                .ForeignKey("dbo.Patients", t => t.PatientId, cascadeDelete: false)
                .Index(t => t.DoctorId)
                .Index(t => t.PatientId);
            
            CreateTable(
                "dbo.Reports",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        PatientId = c.Int(nullable: false),
                        DoctorId = c.Int(nullable: false),
                        TestId = c.Int(nullable: false),
                        DateAdded = c.DateTime(nullable: false),
                        Date = c.DateTime(nullable: false),
                        AddedBy = c.String(),
                        Price = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Doctors", t => t.DoctorId, cascadeDelete: false)
                .ForeignKey("dbo.Patients", t => t.PatientId, cascadeDelete: false)
                .ForeignKey("dbo.Tests", t => t.TestId, cascadeDelete: false)
                .Index(t => t.PatientId)
                .Index(t => t.DoctorId)
                .Index(t => t.TestId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Billitems", "ReportId", "dbo.Reports");
            DropForeignKey("dbo.Reports", "TestId", "dbo.Tests");
            DropForeignKey("dbo.Reports", "PatientId", "dbo.Patients");
            DropForeignKey("dbo.Reports", "DoctorId", "dbo.Doctors");
            DropForeignKey("dbo.Billitems", "BillId", "dbo.Bills");
            DropForeignKey("dbo.Bills", "PatientId", "dbo.Patients");
            DropForeignKey("dbo.Bills", "DoctorId", "dbo.Doctors");
            DropIndex("dbo.Reports", new[] { "TestId" });
            DropIndex("dbo.Reports", new[] { "DoctorId" });
            DropIndex("dbo.Reports", new[] { "PatientId" });
            DropIndex("dbo.Bills", new[] { "PatientId" });
            DropIndex("dbo.Bills", new[] { "DoctorId" });
            DropIndex("dbo.Billitems", new[] { "ReportId" });
            DropIndex("dbo.Billitems", new[] { "BillId" });
            DropTable("dbo.Reports");
            DropTable("dbo.Bills");
            DropTable("dbo.Billitems");
        }
    }
}
