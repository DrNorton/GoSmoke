CREATE TABLE [dbo].[Users] (
    [Id]            BIGINT         IDENTITY (1, 1) NOT NULL,
    [Phone]         NVARCHAR (12)  NOT NULL,
    [Password]      NVARCHAR (300) NULL,
    [DateRegister]  DATETIME       NOT NULL,
    [SecurityStamp] NVARCHAR (200) NULL,
    [Confirm]       BIT            DEFAULT ((0)) NOT NULL,
    [IsVkLink] BIT NOT NULL DEFAULT 0, 
    PRIMARY KEY CLUSTERED ([Id] ASC)
);
