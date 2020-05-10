drop procedure sp_userlist 
go
CREATE PROCEDURE sp_userList
AS
SELECT username, password
FROM Users