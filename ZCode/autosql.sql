USE [YingSheng]
GO

/****** 1.建表语句 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[Adv](
	[Id] [int] PRIMARY KEY IDENTITY(1,1) NOT NULL,
	[Class1Id] [int] NOT NULL,
	[Class2Id] [int] NOT NULL,
	[Adv_Url] [nvarchar](200) NOT NULL,
	[Adv_Name] [nvarchar](50) NOT NULL,
	[Adv_Img] [nvarchar](200) NOT NULL,
	[Adv_desc] [nvarchar](400) NOT NULL,
	[Adv_Type] [tinyint] NOT NULL,
	[CourseId] [bigint] NOT NULL,
	[Price] [money] NOT NULL,
	[Userid] [bigint] NOT NULL,
	[InstructorName] [nvarchar](20) NOT NULL,
	[Width] [smallint] NOT NULL,
	[Height] [smallint] NOT NULL,
	[StartTime] [datetime] NULL,
	[EndTime] [datetime] NULL
)

GO

/****** 2.查询脚本 ******/
SELECT [Id]
      ,[Class1Id]
      ,[Class2Id]
      ,[Adv_Url]
      ,[Adv_Name]
      ,[Adv_Img]
      ,[Adv_desc]
      ,[Adv_Type]
      ,[CourseId]
      ,[Price]
      ,[Userid]
      ,[InstructorName]
      ,[Width]
      ,[Height]
      ,[StartTime]
      ,[EndTime]
  FROM [YingSheng].[dbo].[Adv]
GO

/****** 3.插入脚本 ******/
INSERT INTO [YingSheng].[dbo].[Adv]
           ([Class1Id]
           ,[Class2Id]
           ,[Adv_Url]
           ,[Adv_Name]
           ,[Adv_Img]
           ,[Adv_desc]
           ,[Adv_Type]
           ,[CourseId]
           ,[Price]
           ,[Userid]
           ,[InstructorName]
           ,[Width]
           ,[Height]
           ,[StartTime]
           ,[EndTime])
     VALUES
           (<Class1Id, int,>
           ,<Class2Id, int,>
           ,<Adv_Url, nvarchar(200),>
           ,<Adv_Name, nvarchar(50),>
           ,<Adv_Img, nvarchar(200),>
           ,<Adv_desc, nvarchar(400),>
           ,<Adv_Type, tinyint,>
           ,<CourseId, bigint,>
           ,<Price, money,>
           ,<Userid, bigint,>
           ,<InstructorName, nvarchar(20),>
           ,<Width, smallint,>
           ,<Height, smallint,>
           ,<StartTime, datetime,>
           ,<EndTime, datetime,>)
GO

/****** 4.更新脚本 ******/
UPDATE [YingSheng].[dbo].[Adv]
   SET [Class1Id] = <Class1Id, int,>
      ,[Class2Id] = <Class2Id, int,>
      ,[Adv_Url] = <Adv_Url, nvarchar(200),>
      ,[Adv_Name] = <Adv_Name, nvarchar(50),>
      ,[Adv_Img] = <Adv_Img, nvarchar(200),>
      ,[Adv_desc] = <Adv_desc, nvarchar(400),>
      ,[Adv_Type] = <Adv_Type, tinyint,>
      ,[CourseId] = <CourseId, bigint,>
      ,[Price] = <Price, money,>
      ,[Userid] = <Userid, bigint,>
      ,[InstructorName] = <InstructorName, nvarchar(20),>
      ,[Width] = <Width, smallint,>
      ,[Height] = <Height, smallint,>
      ,[StartTime] = <StartTime, datetime,>
      ,[EndTime] = <EndTime, datetime,>
 WHERE <搜索条件,,>
GO

/****** 5.删除脚本 ******/
DELETE FROM [YingSheng].[dbo].[Adv]
      WHERE <搜索条件,,>
GO