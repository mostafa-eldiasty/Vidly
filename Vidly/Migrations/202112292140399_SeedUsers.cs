namespace Vidly.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class SeedUsers : DbMigration
    {
        public override void Up()
        {
            Sql(@"
                    INSERT INTO [dbo].[AspNetUsers] ([Id], [UserName], [PasswordHash], [SecurityStamp], [Discriminator]) VALUES (N'dffc8af0-313d-4f8f-84b9-788b19d05106', N'guest', N'ADEHEQDvCuH7AxaUnn8ZOBlQOK1RKimWrSdqJIg1A+mRCMupcRSUTBQyaQYySQsxpw==', N'99297697-4d30-46fc-9644-67917d8b823f', N'ApplicationUser')
                    INSERT INTO [dbo].[AspNetUsers] ([Id], [UserName], [PasswordHash], [SecurityStamp], [Discriminator]) VALUES (N'e4ece08e-9fb2-4598-a560-93c75e7bc1c0', N'admin', N'APPm6EpLLuccYlF39Jf2nYJTpXr/ks77CgJW0g/5903SjUl8PVGOIUvPMSNdGi5MTg==', N'16accd5a-8388-457e-b791-b53bbe6c46b8', N'ApplicationUser')
    
                    INSERT INTO [dbo].[AspNetRoles] ([Id], [Name]) VALUES (N'38ae5db2-e212-43a9-b61e-4dcffa299e00', N'CanManageMovies')
                    
                    INSERT INTO [dbo].[AspNetUserRoles] ([UserId], [RoleId]) VALUES (N'e4ece08e-9fb2-4598-a560-93c75e7bc1c0', N'38ae5db2-e212-43a9-b61e-4dcffa299e00')
                ");
        }
        
        public override void Down()
        {
        }
    }
}
