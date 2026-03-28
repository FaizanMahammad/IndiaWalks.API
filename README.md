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



