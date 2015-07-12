CREATE TABLE [dbo].[GroupMembers]
(
	 [Id] BIGINT  IDENTITY (1, 1) NOT NULL , 
     [UserId] BIGINT NOT NULL,
	 [GroupId] BIGINT NOT NULL,

	 CONSTRAINT [FK_GroupMembers_Groups] FOREIGN KEY ([GroupId]) REFERENCES [dbo].[Groups] ([Id]),
	 CONSTRAINT [FK_GroupMembers_USERS] FOREIGN KEY ([UserId]) REFERENCES [dbo].[Users] ([Id]),
	 PRIMARY KEY ([Id])
)
