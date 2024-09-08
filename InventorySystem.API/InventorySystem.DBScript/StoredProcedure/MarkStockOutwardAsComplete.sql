CREATE DEFINER=`inventorydev`@`%` PROCEDURE `MarkStockOutwardAsComplete`(
	IN _id int,
    IN _userId int
)
BEGIN
	UPDATE 
		salesorderbasicinformation
    SET
		SaleOrderStatusId = 1,
        ModifiedBy = _userId,
        ModifiedOn = NOW()
	WHERE
		Id = _id;
    SELECT 
		1 IsSuccess, 
		'Marked as complete.' Message;
END