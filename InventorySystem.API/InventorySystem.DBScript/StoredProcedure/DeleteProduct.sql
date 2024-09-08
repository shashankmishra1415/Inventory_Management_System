CREATE DEFINER=`inventorydev`@`%` PROCEDURE `DeleteProduct`(
	IN _id INT,
    IN _userId INT
)
BEGIN
	 UPDATE 
		product
	 SET
		IsDeleted = 1,
        ModifiedBy = _userId,
        ModifiedOn = NOW()        
	 WHERE
		Id = _id;
        SELECT 1 IsSuccess, "Product Deleted Successfully" Message;
END