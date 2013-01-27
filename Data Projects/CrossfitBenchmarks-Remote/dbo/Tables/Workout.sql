CREATE TABLE [dbo].[Workout] (
    [WorkoutId]     INT            IDENTITY (1, 1) NOT NULL,
    [Name]          NVARCHAR (255) NULL,
    [WorkoutTypeId] CHAR (1)       NOT NULL,
    CONSTRAINT [PK_Workout] PRIMARY KEY CLUSTERED ([WorkoutId] ASC),
    CONSTRAINT [FK_Workout_WorkoutType] FOREIGN KEY ([WorkoutTypeId]) REFERENCES [dbo].[WorkoutType] ([WorkoutTypeId]) ON DELETE CASCADE
);

