CREATE TABLE [dbo].[WorkoutType] (
    [WorkoutTypeId] CHAR (1)      NOT NULL,
    [Name]          NVARCHAR (50) NOT NULL,
    CONSTRAINT [PK_WorkoutType] PRIMARY KEY CLUSTERED ([WorkoutTypeId] ASC)
);

