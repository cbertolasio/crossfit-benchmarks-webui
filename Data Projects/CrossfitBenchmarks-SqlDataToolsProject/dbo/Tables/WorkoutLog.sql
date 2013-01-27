CREATE TABLE [dbo].[WorkoutLog] (
    [WorkoutLogId]      BIGINT             IDENTITY (1, 1) NOT NULL,
    [WorkoutId]         INT                NOT NULL,
    [UserId]            INT                NOT NULL,
    [Score]             NVARCHAR (50)      NULL,
    [DateCreated]       DATETIMEOFFSET (7) CONSTRAINT [DF_WorkoutLog_DateCreated] DEFAULT (getutcdate()) NULL,
    [IsAPersonalRecord] BIT                NOT NULL,
    [Note]              NVARCHAR (1024)    NULL,
    CONSTRAINT [PK_WorkoutLog] PRIMARY KEY CLUSTERED ([WorkoutLogId] ASC),
    CONSTRAINT [FK_WorkoutLog_User] FOREIGN KEY ([UserId]) REFERENCES [dbo].[User] ([UserId]) ON DELETE CASCADE,
    CONSTRAINT [FK_WorkoutLog_Workout] FOREIGN KEY ([WorkoutId]) REFERENCES [dbo].[Workout] ([WorkoutId])
);





