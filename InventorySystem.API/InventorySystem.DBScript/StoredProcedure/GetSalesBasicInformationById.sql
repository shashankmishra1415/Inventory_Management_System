CREATE DEFINER=`inventorydev`@`%` PROCEDURE `GetSalesBasicInformationById`(
IN _id INT)
BEGIN
SELECT 
    Id,
    SalesOrderNumber,
    DateofSale,
    VendorId,
    MovementTypeId,
    WarehouseId,
    OutTypeId
FROM
    SalesOrderBasicInformation
WHERE
    Id = _id;
END