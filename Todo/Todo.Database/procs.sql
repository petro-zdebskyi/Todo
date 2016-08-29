USE Todo
GO

CREATE PROCEDURE spAddTask
	@name NVARCHAR(255),
	@dueDate NVARCHAR(255),
	@priority TINYINT,
	@comment NVARCHAR(255)
AS
BEGIN
	INSERT INTO tblTasks (Name, DueDate, [Priority], IsCompleted, Comment) 
	VALUES (@name , convert(datetime, @dueDate, 120), @priority, 0, @comment)

	SELECT SCOPE_IDENTITY()
END
GO

CREATE PROCEDURE spCompleteTask 
	@taskid INT
AS
BEGIN
	UPDATE tblTasks SET IsCompleted = 1 WHERE Id = @taskid
END
GO

CREATE PROCEDURE spGetTasks
AS
BEGIN
	SELECT * FROM tblTasks WHERE IsCompleted = 0
END
GO

CREATE PROCEDURE spUpdateTask 
	@id INT,
	@name NVARCHAR(255),
	@dueDate NVARCHAR(255),
	@priority TINYINT,
	@comment NVARCHAR(255)
AS
BEGIN
	UPDATE tblTasks SET 
		IsCompleted = 0, 
		Name = @name, 
		DueDate = convert(datetime, @dueDate, 120), 
		[Priority] = @priority, 
		Comment = @comment 
	WHERE Id = @id
END
GO

