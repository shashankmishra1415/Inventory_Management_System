CREATE DEFINER=`inventorydev`@`%` PROCEDURE `CheckDuplicateSalesOrderNumber`(
	IN _salesOrderNumber VARCHAR(64)
)
BEGIN
	DECLARE IsExists INT DEFAULT
						(
                        SELECT 
							COUNT(1)>0 
                        FROM 
							salesorderbasicinformation 
                        WHERE 
							SalesOrderNumber = _salesOrderNumber
						);
	SELECT IsExists;
END