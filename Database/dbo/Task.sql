CREATE TABLE [dbo].[Task] (
    [TaskId]          UNIQUEIDENTIFIER DEFAULT (newid()) NOT NULL,
    [DueDate]         DATETIME2 (7)    NOT NULL,
    [TaskDescription] VARCHAR (MAX)    NOT NULL,
    [Completion]      BIT              NOT NULL,
    [CourseCode]      VARCHAR (10)     NOT NULL,
    PRIMARY KEY CLUSTERED ([TaskId] ASC),
    CONSTRAINT [FK_Task_ToCourse] FOREIGN KEY ([CourseCode]) REFERENCES [dbo].[Course] ([CourseCode])
);

