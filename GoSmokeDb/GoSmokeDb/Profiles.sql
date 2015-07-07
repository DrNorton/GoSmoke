CREATE TABLE [dbo].[Profiles] (
    [Id]           BIGINT         NOT NULL,
    [FirstName]    NVARCHAR (50)  NULL,
    [LastName]     NVARCHAR (50)  NULL,
    [AvatarUrl]    NVARCHAR (250) NULL,
    [AboutMe]      NVARCHAR (250) NULL,
    [Birthday]     DATETIME       NULL,
    [HideBirthday] BIT            DEFAULT ((0)) NOT NULL,
    [Gender]       BIT            DEFAULT ((1)) NOT NULL,
    CONSTRAINT [PK_Profiles] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_Profiles_USERS] FOREIGN KEY ([Id]) REFERENCES [dbo].[Users] ([Id])
);

