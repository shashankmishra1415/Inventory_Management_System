CREATE DEFINER=`inventorydev`@`%` PROCEDURE `DeleteUser`(
	IN _id int,
    IN _userId int
)
BEGIN
	UPDATE 
		`user`
	SET
		IsDeleted = 1,
        ModifiedBy=_userId,
        ModifiedOn = NOW()
	WHERE 
		Id = _id;
        
	SELECT 
		1 IsSuccess,
        "Deleted Successfully" Message;
END