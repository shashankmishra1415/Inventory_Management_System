CREATE DEFINER=`inventorydev`@`%` PROCEDURE `DeleteSalesOrderItemInformation`(
	IN _id int,
	IN _userId int
)
BEGIN
	DECLARE `_rollBack` BOOL DEFAULT 0; 
	DECLARE CONTINUE HANDLER FOR SQLEXCEPTION SET `_rollBack` = 1;
	START
		transaction;
	UPDATE 
		salesorderitemsinformation 
	SET 
		IsDeleted = 1,
		ModifiedBy = _userId,
		ModifiedOn = NOW()
	WHERE
		Id = _id;
		
	IF 
		`_rollBack`
	THEN 
		SELECT
			'NOT DELETED' AS Message; 
	ROLLBACK; 
    
	ELSE
		SELECT
			'DELETED' AS Message; 
	COMMIT; 
	END IF;
END