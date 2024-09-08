CREATE DEFINER=`backup`@`%` PROCEDURE `AddLog`(
	IN Message VARCHAR(500),
	IN Level VARCHAR(500),
	IN Exception VARCHAR(500),
	IN trace VARCHAR(500),
	IN Logger VARCHAR(500),
	IN CreatedOn DATETIME
)
BEGIN
	INSERT INTO Logs
	(Message, `Level`, `Exception`, StackTrace, Logger,CreatedOn)
	VALUES(Message, Level, Exception, StackTrace, Logger,CreatedOn);

END