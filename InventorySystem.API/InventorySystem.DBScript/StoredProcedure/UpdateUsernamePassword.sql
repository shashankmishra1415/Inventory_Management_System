CREATE DEFINER=`inventorydev`@`%` PROCEDURE `UpdateUsernamePassword`(
    IN _id int,
    IN _userId int,
    IN _username varchar(64),
    IN _password varchar(64)
)
BEGIN
    UPDATE 
		logindetail
    SET
        UserName = _username,
        `Password` = MD5(_password),
        ModifiedBy = _userId,
        ModifiedOn = NOW()
    WHERE 
        userId = _id;
	
    SELECT 
		1 IsSuccess,
        "Updated Successfully" Message;
END