USE Todo
--SELECT * FROM tblTasks

INSERT INTO tblTasks (Name, DueDate, [Priority], IsCompleted, Comment) 
VALUES 
	('Reserve place on plane', CONVERT(DATETIME, '2017-04-15 08:30:00', 120), 2, 0, null),
	('Enroll english courses', CONVERT(DATETIME, '2016-07-11 00:00:00', 120), 1, 0, null),
	('Visit IT conference', CONVERT(DATETIME, '2017-09-30 13:25:00', 120), 2, 0, 'Lviv IT Arena'),
	('Read book "Don Quixote"', CONVERT(DATETIME, '2016-06-02 10:19:00', 120), 1, 0, 'Author: Miguel de Cervantes'),
	('Watch the film "Time"', CONVERT(DATETIME, '2016-10-05 00:00:00', 120), 0, 0, null),
	('Make coursework', CONVERT(DATETIME, '2017-03-01 00:00:00', 120), 1, 0, 'Math'),
	('Book room in hotel', CONVERT(DATETIME, '2016-11-02 05:13:00', 120), 0, 0, 'Mr. Frank Leva'),
	('Make a test program "Todo"', CONVERT(DATETIME, '2016-08-29 08:00:00', 120), 2, 1, null), -- :)
	('Buy bike to brother birthday', CONVERT(DATETIME, '2017-04-12 00:00:00', 120), 0, 0, 'Ebay')
GO
