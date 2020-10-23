namespace MovieRental.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class SeedUsers : DbMigration
    {
        public override void Up()
        {
            Sql(@"  
            
            INSERT INTO[dbo].[AspNetUsers] ([Id], [Email], [EmailConfirmed], [PasswordHash], [SecurityStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEndDateUtc], [LockoutEnabled], [AccessFailedCount], [UserName]) VALUES(N'361be54c-24b3-4d37-934f-76a64496c480', N'kacper@vp.pl', 0, N'AG9Xn0T95E1THli/+9cFkFK+YH2GrhUuni65Skajm7qvF2j5VnAPkJ6IkpKu5gJHwg==', N'41cb4db0-da71-4bc7-94a1-6646c8e057d3', NULL, 0, 0, NULL, 1, 0, N'kacper@vp.pl')
            INSERT INTO[dbo].[AspNetUsers] ([Id], [Email], [EmailConfirmed], [PasswordHash], [SecurityStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEndDateUtc], [LockoutEnabled], [AccessFailedCount], [UserName]) VALUES(N'798f51dd-f2a1-40c3-8509-0486c7e8dc19', N'admin@vp.pl', 0, N'AKTJ5whTayDVd2NbWN0tRVy9/IALRyKSYt6OZIDsE5mzP7+hMb+DzdaRbUdtTSIZEg==', N'55b86628-5a88-42df-9ebf-1b75dc274c53', NULL, 0, 0, NULL, 1, 0, N'admin@vp.pl')
            INSERT INTO [dbo].[AspNetRoles] ([Id], [Name]) VALUES (N'717c2c2c-cecc-4eae-81b7-b5ce5ce43457', N'CanManageMovies')
            INSERT INTO [dbo].[AspNetUserRoles] ([UserId], [RoleId]) VALUES (N'798f51dd-f2a1-40c3-8509-0486c7e8dc19', N'717c2c2c-cecc-4eae-81b7-b5ce5ce43457')

            ");
        }

        public override void Down()
        {
        }
    }
}
