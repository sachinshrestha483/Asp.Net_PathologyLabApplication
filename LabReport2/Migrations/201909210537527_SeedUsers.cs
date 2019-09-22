namespace LabReport2.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class SeedUsers : DbMigration
    {
        public override void Up()
        {
            Sql(@"
INSERT INTO [dbo].[AspNetUsers] ([Id], [Email], [EmailConfirmed], [PasswordHash], [SecurityStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEndDateUtc], [LockoutEnabled], [AccessFailedCount], [UserName]) VALUES (N'84d09b00-ad85-4c5e-9725-ca658b592562', N'Manager@gmail.com', 0, N'ABrUcJem90GnoXGP9HVNRWk6HcdvROoCx8Kir/7CuJrGL9Dw7VQMkL9gr1M6xZq4jg==', N'e01281ee-02f6-4cf1-b5ea-a1eaf9a3ddc1', NULL, 0, 0, NULL, 1, 0, N'Manager@gmail.com')
INSERT INTO [dbo].[AspNetUsers] ([Id], [Email], [EmailConfirmed], [PasswordHash], [SecurityStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEndDateUtc], [LockoutEnabled], [AccessFailedCount], [UserName]) VALUES (N'8aafb1b8-4036-4d9b-9547-ba6d6f04ae2d', N'labtechnician2@gmail.com', 0, N'ALhBVjfBvlhbixQTSFm/WNDd6ms6JnjSz9v1T13jDtCuFMkx9g/N6PhMqylZ59pGlA==', N'0fc22f63-a7ef-48ea-acd7-8cbd5cc37c7d', NULL, 0, 0, NULL, 1, 0, N'labtechnician2@gmail.com')
INSERT INTO [dbo].[AspNetUsers] ([Id], [Email], [EmailConfirmed], [PasswordHash], [SecurityStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEndDateUtc], [LockoutEnabled], [AccessFailedCount], [UserName]) VALUES (N'b545b440-bbc1-4f0b-8f4f-a9fb020c47a6', N'labtechnician1@gmail.com', 0, N'AANRsBzjEhMy6IKl91D/deVEDVRlFa+RKbF2eDbR1iNmClNPwQG5HUQ7uxLhxBzzcw==', N'ca1730ec-3736-40ad-8ffd-ea815047d2b3', NULL, 0, 0, NULL, 1, 0, N'labtechnician1@gmail.com')


               ");

            Sql(@"
INSERT INTO [dbo].[AspNetRoles] ([Id], [Name]) VALUES (N'c1bdedb5-f536-4c7c-93b9-c20a96f89f35', N'CanCreateReadSeeReportAndBill')
INSERT INTO [dbo].[AspNetRoles] ([Id], [Name]) VALUES (N'1c0a95cc-171a-470c-8376-5ab523faa36b', N'CanManageEveryThing')

               ");

            Sql(@"
INSERT INTO [dbo].[AspNetUserRoles] ([UserId], [RoleId]) VALUES (N'84d09b00-ad85-4c5e-9725-ca658b592562', N'1c0a95cc-171a-470c-8376-5ab523faa36b')
INSERT INTO [dbo].[AspNetUserRoles] ([UserId], [RoleId]) VALUES (N'8aafb1b8-4036-4d9b-9547-ba6d6f04ae2d', N'c1bdedb5-f536-4c7c-93b9-c20a96f89f35')
INSERT INTO [dbo].[AspNetUserRoles] ([UserId], [RoleId]) VALUES (N'fb8db86d-a3bd-4f53-9872-624f2e11595c', N'c1bdedb5-f536-4c7c-93b9-c20a96f89f35')

                ");
        }
        
        public override void Down()
        {
        }
    }
}
