CREATE DEFINER=`inventorydev`@`%` PROCEDURE `GetTotalCountForSalesOrderDetails`()
BEGIN
	DECLARE Total int;
	DECLARE Scanned int;
	DECLARE Pending int;
	SELECT
		COUNT(*) INTO Total
	FROM 
		productserialnumber;
        
	SELECT
		COUNT(*) INTO Scanned 
	FROM 
		productserialnumber
	WHERE
		IsScanned=1;
        
	SELECT 
		COUNT(*) INTO Pending
	FROM
		productserialnumber
	WHERE
		IsScanned=0;
        
	SELECT 
		Total,
		Scanned,
        Pending;
END