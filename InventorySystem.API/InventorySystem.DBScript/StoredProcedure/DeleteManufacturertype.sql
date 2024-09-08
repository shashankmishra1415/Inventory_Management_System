CREATE DEFINER=`inventorydev`@`%` PROCEDURE `DeleteManufacturertype`( 
	IN _id INT,
    IN _userId INT
)
BEGIN
  UPDATE 
		manufacturer
	SET
		IsDeleted=1,
		ModifiedOn=NOW(),
		ModifiedBy=_userId
	WHERE
		Id=_id;
        
	SELECT
		1 IsSuccess,
        'Data Deleted Successfully' Message;
END