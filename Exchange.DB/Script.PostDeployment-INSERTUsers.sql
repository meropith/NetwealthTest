/*
Post-Deployment Script Template							
--------------------------------------------------------------------------------------
 This file contains SQL statements that will be appended to the build script.		
 Use SQLCMD syntax to include a file in the post-deployment script.			
 Example:      :r .\myfile.sql								
 Use SQLCMD syntax to reference a variable in the post-deployment script.		
 Example:      :setvar TableName MyTable							
               SELECT * FROM [$(TableName)]					
--------------------------------------------------------------------------------------
*/
INSERT [dbo].[AspNetRoles] ([Id], [Name], [NormalizedName], [ConcurrencyStamp]) VALUES (N'8C7F9A4B-C6C7-4388-A77F-689DA17C7161', N'TierDB', N'TierDB', NULL)

INSERT [dbo].[AspNetRoles] ([Id], [Name], [NormalizedName], [ConcurrencyStamp]) VALUES (N'AD4DEF6C-FC3A-45ED-9EB6-BE7D5E089E14', N'FREE', N'FREE', NULL)

INSERT [dbo].[AspNetRoles] ([Id], [Name], [NormalizedName], [ConcurrencyStamp]) VALUES (N'DAA3C963-E486-4E92-A383-8C58EDA220F5', N'TierAPI', N'TierAPI', NULL)

INSERT [dbo].[AspNetUsers] ([Id], [UserName], [NormalizedUserName], [Email], [NormalizedEmail], [EmailConfirmed], [PasswordHash], [SecurityStamp], [ConcurrencyStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEnd], [LockoutEnabled], [AccessFailedCount]) VALUES (N'3f97dfe4-ab96-4a57-8d17-4d9159ac085b', N'tierdb@tierdb.com', N'TIERDB@TIERDB.COM', N'tierdb@tierdb.com', N'TIERDB@TIERDB.COM', 1, N'AQAAAAEAACcQAAAAEF0tPBnqt/277G9KbsqsTdRnx1CjXMBw5uysFoEeev5cHksNgTE43unQu62DPbmvfw==', N'ZD4VPACUB6BFAZ32UKMPA3GGTIKBYGI7', N'ead0b7bc-f7b3-432d-bc40-778511304707', NULL, 0, 0, NULL, 1, 0)

INSERT [dbo].[AspNetUsers] ([Id], [UserName], [NormalizedUserName], [Email], [NormalizedEmail], [EmailConfirmed], [PasswordHash], [SecurityStamp], [ConcurrencyStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEnd], [LockoutEnabled], [AccessFailedCount]) VALUES (N'53688bd4-9dae-4441-aaee-3234c943380e', N'tierapi@tierapi.com', N'TIERAPI@TIERAPI.COM', N'tierapi@tierapi.com', N'TIERAPI@TIERAPI.COM', 1, N'AQAAAAEAACcQAAAAEAkJ5RlgaWc7dm48m266RZrCXv7/c8KFsskT1/EVgIS4iEO3ZsRilEKBIOGKd4DREw==', N'KIYK6XYSEBTOKLS4ZES3IADQ2C76FIBP', N'207a1c91-10b1-4002-94f7-7b5878aeec03', NULL, 0, 0, NULL, 1, 0)

INSERT [dbo].[AspNetUsers] ([Id], [UserName], [NormalizedUserName], [Email], [NormalizedEmail], [EmailConfirmed], [PasswordHash], [SecurityStamp], [ConcurrencyStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEnd], [LockoutEnabled], [AccessFailedCount]) VALUES (N'b45853a1-c18a-41df-b18f-27ce88347adc', N'free@free.com', N'FREE@FREE.COM', N'free@free.com', N'FREE@FREE.COM', 1, N'AQAAAAEAACcQAAAAENPS5HndATGVvDfSJqDLbb5gn0moZ5Pt+DRjm7MbHpo7xYbdhAKvKh3pLPttrEhz8Q==', N'NRQTAK7GATN6UO7KVWCOY3SQNOMTIATK', N'dca46a2d-aff6-41ef-a04c-c9f6976b5bb2', NULL, 0, 0, NULL, 1, 0)

INSERT [dbo].[AspNetUserRoles] ([UserId], [RoleId]) VALUES (N'3f97dfe4-ab96-4a57-8d17-4d9159ac085b', N'8C7F9A4B-C6C7-4388-A77F-689DA17C7161')

INSERT [dbo].[AspNetUserRoles] ([UserId], [RoleId]) VALUES (N'b45853a1-c18a-41df-b18f-27ce88347adc', N'AD4DEF6C-FC3A-45ED-9EB6-BE7D5E089E14')

INSERT [dbo].[AspNetUserRoles] ([UserId], [RoleId]) VALUES (N'53688bd4-9dae-4441-aaee-3234c943380e', N'DAA3C963-E486-4E92-A383-8C58EDA220F5')