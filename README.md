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
****************************************************************************************************************************************************************************
OTHER INFO:
<Project Sdk="Microsoft.NET.Sdk.Web">

  <PropertyGroup>
    <TargetFramework>net8.0</TargetFramework>
*****************************************************************************************************************************************************************************



