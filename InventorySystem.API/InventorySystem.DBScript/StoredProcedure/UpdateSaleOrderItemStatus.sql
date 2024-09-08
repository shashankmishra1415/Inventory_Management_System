CREATE DEFINER=`inventorydev`@`%` PROCEDURE `UpdateSaleOrderItemStatus`(
    IN _id int,
    IN _statusId int,
    IN _userId int
)
BEGIN
    UPDATE
         salesorderbasicinformation
    SET 
        SaleOrderStatusId = 3,
        ModifiedBy = _userId,
        ModifiedOn = NOW()
    WHERE 
        Id = _id;

    SELECT 
        1 IsSuccess, 
        "Update Successfully" Message;
END