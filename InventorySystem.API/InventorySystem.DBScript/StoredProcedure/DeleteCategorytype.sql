CREATE DEFINER=`inventorydev`@`%` PROCEDURE `DeleteCategorytype`(
	IN _id int,
    IN _userId int
)
BEGIN
	UPDATE 
		Category
	SET
		IsDeleted=1,
        ModifiedBy=_userId,
        ModifiedOn=NOW()
	WHERE 
		Id=_id;
        
	SELECT 
	1 IsSuccess,
    "Data Deleted Successfully" Message ;
END