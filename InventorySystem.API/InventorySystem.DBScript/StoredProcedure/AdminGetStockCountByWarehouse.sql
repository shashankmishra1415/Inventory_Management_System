CREATE DEFINER=`inventorydev`@`%` PROCEDURE `AdminGetStockCountByWarehouse`()
BEGIN
	SELECT 
		w.Id,
		w.LocationName,
		count(ps.Id)TotalProduct
	FROM 
		stockinwardbasicinformation ibi
	INNER JOIN
		stockinwarditeminformation iii
	ON
		ibi.Id = iii.StockInwardBasicInformationId 
	INNER JOIN 
		productserialnumber ps 
	ON
		iii.Id = ps.StockInwardItemInformationId 
	INNER JOIN 
		warehouse w 
	ON
		w.Id = ibi.WarehouseId 
	WHERE 
		ps.IsScanned = 1
	AND
		w.IsDeleted = 0
	AND
		CheckForSalesOrderedOrNot(ps.Id)
	GROUP BY
		w.Id;
END