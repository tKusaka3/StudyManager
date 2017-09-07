CREATE TABLE [dbo].[Attendance] (
    [AttId]      UNIQUEIDENTIFIER DEFAULT (newid()) NOT NULL,
    [AttRecord]  VARCHAR (50)     NOT NULL,
    [CourseCode] VARCHAR (10)     NOT NULL,
    [AttDate]    DATETIME2 (7)    NOT NULL,
    PRIMARY KEY CLUSTERED ([AttId] ASC),
    CONSTRAINT [CK_Attendance_AttRecord] CHECK ([AttRecord]='Holiday' OR [AttRecord]='Absence' OR [AttRecord]='Attend'),
    CONSTRAINT [FK_Attendance_ToCourse] FOREIGN KEY ([CourseCode]) REFERENCES [dbo].[Course] ([CourseCode])
);

