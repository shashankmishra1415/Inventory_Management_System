CREATE DEFINER=`inventorydev`@`%` PROCEDURE `GetInventoryDetailByVendorForLocation`(
	IN _warehouseId int
)
BEGIN

	SELECT 
		v.Id,
		v.CompanyName Name,
		tbl.TotalPrice,
        InventoryByVendorlocationFunction(_warehouseId, tbl.VendorId) As JsonList
	FROM(
		SELECT 
			sibi.VendorId,
			sum(p.Price) TotalPrice
		FROM 
			stockinwardbasicinformation sibi 
		INNER JOIN
			stockinwarditeminformation siii 
		ON
			sibi.Id = siii.StockInwardBasicInformationId 
		INNER JOIN 	
			product p
		ON
			siii.ProductId = p.Id 
		INNER JOIN 
			productserialnumber psn
		ON
			siii.Id = psn.StockInwardItemInformationId 
		WHERE 
			sibi.WarehouseId = _warehouseId
		AND
			psn.IsScanned = 1
		AND
			CheckForSalesOrderedOrNot(psn.Id)
		GROUP BY 
			sibi.VendorId
	)tbl
	INNER JOIN 
		vendor v 
	ON
		tbl.VendorId = v.Id;
END