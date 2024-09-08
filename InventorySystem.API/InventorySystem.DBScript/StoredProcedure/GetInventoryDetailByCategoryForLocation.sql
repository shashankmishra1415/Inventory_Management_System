CREATE DEFINER=`inventorydev`@`%` PROCEDURE `GetInventoryDetailByCategoryForLocation`(
	IN _warehouseId int
)
BEGIN
	SELECT 
		c.Id,
		c.Name,
		tbl.TotalPrice,
        GetInventoryDetailByCategoryOnLocationFunction(_warehouseId,tbl.CategoryId) As JsonList
	FROM(
		SELECT 
			pp.CategoryId,
			SUM(pp.Price) TotalPrice
		FROM 
			stockinwardbasicinformation inv 
		INNER JOIN
			stockinwarditeminformation psp 
		ON
			inv.Id = psp.StockInwardBasicInformationId 
		INNER JOIN 	
			product pp 
		ON
			psp.ProductId = pp.Id 
		INNER JOIN 
			productserialnumber s 
		ON
			psp.Id = s.StockInwardItemInformationId 
		WHERE 
			inv.WarehouseId = _warehouseId
		AND
			s.IsScanned = 1
		AND
			CheckForSalesOrderedOrNot(s.Id)
		GROUP BY 
			pp.CategoryId
	)tbl
	INNER JOIN 
		category c
	ON
		tbl.CategoryId = c.Id;
END