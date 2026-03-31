# IndiaWalksAPI
****************************************************************************************************************************************************************************
TABLES:-
::::::::::::::::::::::::::: Walks :::::::::::::::::::::::::::::::::::::::::::::::
--------------------------------------------------------------------------------------
Column Name         ||           	Column Type          ||         	Nullable?
Id (PK)	                  Unique Identifier (GUID)                  	N
Name                         	string	                                N
Description	                  string	                                N
LengthInKm	                  double	                                N
WalkImageUrl	                string	                                Y
RegionId (FK)           	Unique Identifier (GUID)	                  N
DifficultyId (FK)       	Unique Identifier (GUID)	                  N
--------------------------------------------------------------------------------------
::::::::::::::::::::::::: Regions ::::::::::::::::::::::::::::::::::::::::::::::::::::
--------------------------------------------------------------------------------------
Column Name        ||       	Column Type 	            ||        Nullable?
Id (PK)                   	Unique Identifier (GUID)                	N
Code	                            string	                            N
Name	                            string                            	N
RegionImageUrl	                  string	                            Y
-------------------------------------------------------------------------------------
::::::::::::::::::::::::: Difficulty - this is a fixed DB(Default VAlues) :::::::::::
-------------------------------------------------------------------------------------
Column Name	         ||          Column Type 	           ||       Nullable?
Id (PK)                   	Unique Identifier (GUID)	                N
Name	                            string	                            N
-------------------------------------------------------------------------------------
****************************************************************************************************************************************************************************
NUGET PACKAGES ADDED                                      ||            	VERSIONS
Microsoft.EntityFrameWorkCore.SqlServer  (Sql Connection)	||        7.0.20 => 9.0.11=> 8.0.22 -Because we're using .net Framework 8
Microsoft.EntityFrameWorkCore.Tools   (Migration)	        ||        7.0.20 => 10.0.1=> 8.0.22
Microsoft.EntityFrameworkCore.Design	                    ||        8.0.22
Microsoft.EntityFrameworkCore.Sqlite	                    ||        8.0.22
Microsoft.EntityFrameworkCore	                            ||        8.0.22
AutoMapper                                                ||       	13.0.1
Microsoft.AspNetCore.Authentication.JwtBearer	            ||        8.0.24
Microsoft.IdentityModel.Tokens	                          ||        8.15.0
System.IdentityModel.Tokens.Jwt	                          ||        8.15.0
Microsoft.AspNetCore.Identity.EntityFrameworkCore	        ||        8.0.22
Serilog	                                                  ||        4.3.1
Serilog.AspNetCore	                                      ||        10.0.0
Serilog.Sinks.Console	                                    ||        6.1.1
Serilog.Sinks.File	                                      ||        7.0.0

Microsoft.AspNetCore.Mvc.Versioning		                    ||                                                   -- To Have same path for different Version - Versioning (Auto)
Microsoft.AspNetCore.Mvc.Versioning.ApiExplorer		        ||                                                   -- To Make Swagger Support Versioning

****************************************************************************************************************************************************************************
OTHER INFO/ Project Info:
<Project Sdk="Microsoft.NET.Sdk.Web">

  <PropertyGroup>
    <TargetFramework>net8.0</TargetFramework>
    <Nullable>enable</Nullable>
    <ImplicitUsings>enable</ImplicitUsings>
  </PropertyGroup>

  <ItemGroup>
    <PackageReference Include="AutoMapper" Version="13.0.1" />
    <PackageReference Include="Microsoft.AspNetCore.Authentication.JwtBearer" Version="8.0.24" />
    <PackageReference Include="Microsoft.AspNetCore.Identity.EntityFrameworkCore" Version="8.0.22" />
    <PackageReference Include="Microsoft.EntityFrameworkCore" Version="8.0.22" />
    <PackageReference Include="Microsoft.EntityFrameworkCore.Design" Version="8.0.22">
      <PrivateAssets>all</PrivateAssets>
      <IncludeAssets>runtime; build; native; contentfiles; analyzers; buildtransitive</IncludeAssets>
    </PackageReference>
    <PackageReference Include="Microsoft.EntityFrameworkCore.Sqlite" Version="8.0.22" />
    <PackageReference Include="Microsoft.EntityFrameworkCore.SqlServer" Version="8.0.22" />
    <PackageReference Include="Microsoft.EntityFrameworkCore.Tools" Version="8.0.22">
      <PrivateAssets>all</PrivateAssets>
      <IncludeAssets>runtime; build; native; contentfiles; analyzers; buildtransitive</IncludeAssets>
    </PackageReference>
    <PackageReference Include="Microsoft.IdentityModel.Tokens" Version="8.15.0" />
    <PackageReference Include="Serilog" Version="4.3.1" />
    <PackageReference Include="Serilog.AspNetCore" Version="10.0.0" />
    <PackageReference Include="Serilog.Sinks.Console" Version="6.1.1" />
    <PackageReference Include="Serilog.Sinks.File" Version="7.0.0" />
    <PackageReference Include="Swashbuckle.AspNetCore" Version="6.6.2" />
    <PackageReference Include="System.IdentityModel.Tokens.Jwt" Version="8.15.0" />
  </ItemGroup>

  <ItemGroup>
    <Folder Include="Images\" />
    <Folder Include="Logs\" />
  </ItemGroup>

</Project>

*****************************************************************************************************************************************************************************
HELPFULL SQL SCRIPT:


--Use [_.1IndiaWalksDb];
Use [IndiaWalksDb];
GO

Select * from Walks;
select * from Regions;
select * from Difficulties;

SELECT TOP (1000) [Id]
      ,[FileName]
      ,[FileDescription]
      ,[FileExtension]
      ,[FileSizeInBytes]
      ,[FilePath]
  FROM [IndiaWalksDb].[dbo].[Images]
-------------------------------------
Use [IndiaWalksAuthDb];
GO

SELECT TOP (1000) [Id]
      ,[Name]
      ,[NormalizedName]
      ,[ConcurrencyStamp]
  FROM [IndiaWalksAuthDb].[dbo].[AspNetRoles]

  SELECT TOP (1000) [Id]
      ,[UserName]
      ,[NormalizedUserName]
      ,[Email]
      ,[NormalizedEmail]
      ,[EmailConfirmed]
      ,[PasswordHash]
      ,[SecurityStamp]
      ,[ConcurrencyStamp]
      ,[PhoneNumber]
      ,[PhoneNumberConfirmed]
      ,[TwoFactorEnabled]
      ,[LockoutEnd]
      ,[LockoutEnabled]
      ,[AccessFailedCount]
  FROM [IndiaWalksAuthDb].[dbo].[AspNetUsers]
/*
reader@indiawalks.com , Reader@123
Writer@IndiaWalks.com , Writer@123
*/

  SELECT TOP (1000) [UserId]
      ,[RoleId]
  FROM [IndiaWalksAuthDb].[dbo].[AspNetUserRoles]
-------------------------------------

--select @@SERVERNAME;
--Insert into Regions (Id,Code,[Name],RegionImageUrl)
--values ('ebba2135-ffa9-4211-9f45-fcf2706f099f','PLMNR','Palamaner Region', NULL);

--delete from Regions;
--delete from Walks;
--delete from Difficulties;

/*  ----Seed Data-----
USE [_.1IndiaWalksDb]
GO
DELETE FROM [_.1IndiaWalksDb].[dbo].[Regions];
GO
DELETE FROM [_.1IndiaWalksDb].[dbo].[Difficulties];
GO
DELETE FROM [_.1IndiaWalksDb].[dbo].[Walks];
GO
INSERT [dbo].[Difficulties] ([Id], [Name]) VALUES (N'f808ddcd-b5e5-4d80-b732-1ca523e48434', N'Hard')
GO
INSERT [dbo].[Difficulties] ([Id], [Name]) VALUES (N'54466f17-02af-48e7-8ed3-5a4a8bfacf6f', N'Easy')
GO
INSERT [dbo].[Difficulties] ([Id], [Name]) VALUES (N'ea294873-7a8c-4c0f-bfa7-a2eb492cbf8c', N'Medium')
GO
INSERT [dbo].[Regions] ([Id], [Code], [Name], [RegionImageUrl]) VALUES (N'906cb139-415a-4bbb-a174-1a1faf9fb1f6', N'NSN', N'Nelson', NULL)
GO
INSERT [dbo].[Regions] ([Id], [Code], [Name], [RegionImageUrl]) VALUES (N'f7248fc3-2585-4efb-8d1d-1c555f4087f6', N'AKL', N'Auckland', NULL)
GO
INSERT [dbo].[Regions] ([Id], [Code], [Name], [RegionImageUrl]) VALUES (N'14ceba71-4b51-4777-9b17-46602cf66153', N'BOP', N'Bay Of Plenty', NULL)
GO
INSERT [dbo].[Regions] ([Id], [Code], [Name], [RegionImageUrl]) VALUES (N'6884f7d7-ad1f-4101-8df3-7a6fa7387d81', N'NTL', N'Northland', NULL)
GO
INSERT [dbo].[Regions] ([Id], [Code], [Name], [RegionImageUrl]) VALUES (N'f077a22e-4248-4bf6-b564-c7cf4e250263', N'STL', N'Southland', NULL)
GO
INSERT [dbo].[Regions] ([Id], [Code], [Name], [RegionImageUrl]) VALUES (N'cfa06ed2-bf65-4b65-93ed-c9d286ddb0de', N'WGN', N'Wellington', NULL)
GO
Insert into [_.1IndiaWalksDb].[dbo].[Walks]
(Id, Name, Description, LengthInKm, WalkImageUrl, DifficultyId, RegionId)
values
('327aa9f7-26f7-4ddb-8047-97464374bb63', 'Mount Victoria Loop', 'This scenic walk takes you around the top of Mount Victoria, offering stunning views of Wellington and its harbor.', 3.5, NULL, '54466F17-02AF-48E7-8ED3-5A4A8BFACF6F','CFA06ED2-BF65-4B65-93ED-C9D286DDB0DE');
GO
Insert into [_.1IndiaWalksDb].[dbo].[Walks]
(Id, Name, Description, LengthInKm, WalkImageUrl, DifficultyId, RegionId)
values
('1cc5f2bc-ff4b-47c0-a475-1add56c6497b', 'Makara Beach Walkway', 'This walk takes you along the wild and rugged coastline of Makara Beach, with breathtaking views of the Tasman Sea.', 8.2, NULL, 'EA294873-7A8C-4C0F-BFA7-A2EB492CBF8C','CFA06ED2-BF65-4B65-93ED-C9D286DDB0DE');
GO
Insert into [_.1IndiaWalksDb].[dbo].[Walks]
(Id, Name, Description, LengthInKm, WalkImageUrl, DifficultyId, RegionId)
values
('09601132-f92d-457c-b47e-da90e117b33c', 'Botanic Garden Walk', 'Explore the beautiful Botanic Garden of Wellington on this leisurely walk, with a wide variety of plants and flowers to admire.', 2, NULL, '54466F17-02AF-48E7-8ED3-5A4A8BFACF6F','CFA06ED2-BF65-4B65-93ED-C9D286DDB0DE');
GO
Insert into [_.1IndiaWalksDb].[dbo].[Walks]
(Id, Name, Description, LengthInKm, WalkImageUrl, DifficultyId, RegionId)
values
('30d654c7-89ac-4704-8333-5065b740150b', 'Mount Eden Summit Walk', 'This walk takes you to the summit of Mount Eden, the highest natural point in Auckland, with panoramic views of the city.', 2, NULL, '54466F17-02AF-48E7-8ED3-5A4A8BFACF6F','F7248FC3-2585-4EFB-8D1D-1C555F4087F6');
GO
Insert into [_.1IndiaWalksDb].[dbo].[Walks]
(Id, Name, Description, LengthInKm, WalkImageUrl, DifficultyId, RegionId)
values
('f7578324-f025-4c86-83a9-37a7f3d8fe81', 'Cornwall Park Walk', 'Explore the beautiful Cornwall Park on this leisurely walk, with a wide variety of trees, gardens, and animals to admire.', 3, NULL, '54466F17-02AF-48E7-8ED3-5A4A8BFACF6F','F7248FC3-2585-4EFB-8D1D-1C555F4087F6');
GO
Insert into [_.1IndiaWalksDb].[dbo].[Walks]
(Id, Name, Description, LengthInKm, WalkImageUrl, DifficultyId, RegionId)
values
('bdf28703-6d0e-4822-ad8b-e2923f4e95a2', 'Takapuna to Milford Coastal Walk', 'This coastal walk takes you along the beautiful beaches of Takapuna and Milford, with stunning views of Rangitoto Island.', 5, NULL, 'EA294873-7A8C-4C0F-BFA7-A2EB492CBF8C','F7248FC3-2585-4EFB-8D1D-1C555F4087F6');
GO
Insert into [_.1IndiaWalksDb].[dbo].[Walks]
(Id, Name, Description, LengthInKm, WalkImageUrl, DifficultyId, RegionId)
values
('43132402-3d5e-467a-8cde-351c5c7c5dde', 'Centre of New Zealand Walkway', 'This walk takes you to the geographical centre of New Zealand, with stunning views of Nelson and its surroundings.', 1.0 , NULL, 'EA294873-7A8C-4C0F-BFA7-A2EB492CBF8C','906CB139-415A-4BBB-A174-1A1FAF9FB1F6');
GO
Insert into [_.1IndiaWalksDb].[dbo].[Walks]
(Id, Name, Description, LengthInKm, WalkImageUrl, DifficultyId, RegionId)
values
('1ea0b064-2d44-4324-91ee-6dd86c91b713', 'Maitai Valley Walk', 'Explore the picturesque Maitai Valley on this easy walk, with a tranquil river and native bush to enjoy.', 5.0 , NULL, 'EA294873-7A8C-4C0F-BFA7-A2EB492CBF8C','906CB139-415A-4BBB-A174-1A1FAF9FB1F6');
GO
Insert into [_.1IndiaWalksDb].[dbo].[Walks]
(Id, Name, Description, LengthInKm, WalkImageUrl, DifficultyId, RegionId)
values
('04ab77f0-e145-4fbf-b641-989df24e5573', 'Boulder Bank Walkway', 'This coastal walk takes you along the unique Boulder Bank, a long narrow bar of rocks that extends into Tasman Bay.', 8.0 , NULL, 'F808DDCD-B5E5-4D80-B732-1CA523E48434','906CB139-415A-4BBB-A174-1A1FAF9FB1F6');
GO
Insert into [_.1IndiaWalksDb].[dbo].[Walks]
(Id, Name, Description, LengthInKm, WalkImageUrl, DifficultyId, RegionId)
values
('b5aa2791-3616-4db6-ab33-c54d03d17f62', 'Mount Maunganui Summit Walk', 'This walk takes you to the summit of Mount Maunganui, with stunning views of the ocean and surrounding landscape.', 3.0 , NULL, 'EA294873-7A8C-4C0F-BFA7-A2EB492CBF8C','14CEBA71-4B51-4777-9B17-46602CF66153');
GO
Insert into [_.1IndiaWalksDb].[dbo].[Walks]
(Id, Name, Description, LengthInKm, WalkImageUrl, DifficultyId, RegionId)
values
('2d9d6604-bef9-4b0a-805d-630240a29595', 'The Papamoa Hills Regional Park Walk', 'Enjoy panoramic views of Tauranga and Mount Maunganui on this walk through the Papamoa Hills, with a mix of bush and open farmland.', 5.0 , NULL, 'EA294873-7A8C-4C0F-BFA7-A2EB492CBF8C','14CEBA71-4B51-4777-9B17-46602CF66153');
GO
Insert into [_.1IndiaWalksDb].[dbo].[Walks]
(Id, Name, Description, LengthInKm, WalkImageUrl, DifficultyId, RegionId)
values
('135a6e58-969f-47e1-8278-d7fbf2b3bd69', 'The White Pine Bush Track', 'Explore the lush and peaceful White Pine Bush on this easy walk, with a variety of native flora and fauna to discover.', 2.0 , NULL, 'EA294873-7A8C-4C0F-BFA7-A2EB492CBF8C','14CEBA71-4B51-4777-9B17-46602CF66153');
GO
Insert into [_.1IndiaWalksDb].[dbo].[Walks]
(Id, Name, Description, LengthInKm, WalkImageUrl, DifficultyId, RegionId)
values
('24ef9346-17e2-467e-bfc0-d062a9042bf1', 'The Bluff Hill Walkway', 'This walk takes you to the top of Bluff Hill, with panoramic views of Bluff and the surrounding coastline.', 6.0 , NULL, 'EA294873-7A8C-4C0F-BFA7-A2EB492CBF8C','F077A22E-4248-4BF6-B564-C7CF4E250263');
GO
Insert into [_.1IndiaWalksDb].[dbo].[Walks]
(Id, Name, Description, LengthInKm, WalkImageUrl, DifficultyId, RegionId)
values
('f2b56c63-eb99-475a-881c-278f3da03e3d', 'The Kepler Track', 'One of New Zealand s most famous walks, the Kepler Track offers stunning alpine vistas and takes you through a range of landscapes from peaceful beech forests to open tussock lands.', 32.0 , NULL, 'F808DDCD-B5E5-4D80-B732-1CA523E48434','F077A22E-4248-4BF6-B564-C7CF4E250263');
GO
Insert into [_.1IndiaWalksDb].[dbo].[Walks]
(Id, Name, Description, LengthInKm, WalkImageUrl, DifficultyId, RegionId)
values
('a7796ab6-5426-46af-b755-65d9b9e12978', 'The Hump Ridge Track', 'Experience the stunning scenery of the southern Fiordland and the coast on this challenging multi-day walk, with beautiful forest and alpine views.', 60.0 , NULL, 'F808DDCD-B5E5-4D80-B732-1CA523E48434','F077A22E-4248-4BF6-B564-C7CF4E250263');
*/

************************************************************************************************************************************************************************************************



