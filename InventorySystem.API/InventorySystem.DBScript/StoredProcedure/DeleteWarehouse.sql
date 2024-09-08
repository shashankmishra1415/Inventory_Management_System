CREATE DEFINER=`inventorydev`@`%` PROCEDURE `DeleteWarehouse`(
	IN _id int,
    IN _userId int
)
BEGIN
	 UPDATE 
		warehouse
	 SET
		IsDeleted = 1,
        ModifiedOn = Now(),
        ModifiedBy=_userId
	 WHERE
		Id = _id;	 
	 SELECT 
		1 IsSuccess,
        "Deleted Warehouse Successfully." Message;
END