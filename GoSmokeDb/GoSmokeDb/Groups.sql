﻿CREATE TABLE [dbo].[Groups]
(
	 [Id]  BIGINT   IDENTITY (1, 1) NOT NULL, 
     [Name] NVARCHAR(50) NOT NULL UNIQUE,
	 PRIMARY KEY ([Id])
	
)
