CREATE DEFINER=`inventorydev`@`%` PROCEDURE `GetInventoryDetailAtLocation`(
	IN _warehouseId int
)
BEGIN
	DECLARE brandCount int DEFAULT 0;
	DECLARE categoryCount int DEFAULT 0;
	DECLARE supplierCount int DEFAULT 0;

	SELECT 
		SUM(1) TotalStockInHand,
		SUM(pp.Price)TotalPrice
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
		CheckForSalesOrderedOrNot(s.Id);
	
	SET brandCount=(
			SELECT 
				COUNT(DISTINCT 
				pp.ManufacturerId)
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
			);
		
	SET categoryCount=(
		SELECT 
			COUNT(DISTINCT 
			pp.CategoryId)
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
		);
	
	SET supplierCount=(
		SELECT 
			COUNT(DISTINCT 
			inv.VendorId)
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
		);
	
	SELECT brandCount BrandCount, categoryCount CategoryCount, supplierCount SupplierCount;
END