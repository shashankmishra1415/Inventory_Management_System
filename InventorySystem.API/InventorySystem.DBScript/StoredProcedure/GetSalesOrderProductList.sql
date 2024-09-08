CREATE DEFINER=`inventorydev`@`%` PROCEDURE `GetSalesOrderProductList`()
BEGIN
SELECT 
     Id,
     ProductSKU 
FROM 
     Product;
END