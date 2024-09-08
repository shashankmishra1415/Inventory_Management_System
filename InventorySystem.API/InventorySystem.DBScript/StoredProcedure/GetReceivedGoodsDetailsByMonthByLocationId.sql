CREATE DEFINER=`inventorydev`@`%` PROCEDURE `GetReceivedGoodsDetailsByMonthByLocationId`(
	IN _filterMonth date,
	IN _locationId int
)
BEGIN
        
DROP TEMPORARY TABLE IF EXISTS MtdValueView;
CREATE TEMPORARY TABLE MtdValueView AS (
	SELECT
		siii.ProductId, 
		(p.Price * SUM(siii.quantity)) MtdValue 
    FROM 
		stockinwarditeminformation siii
	INNER JOIN
		productserialnumber psn
	ON
		siii.Id = psn.StockInwardItemInformationId 
	INNER JOIN 
		stockinwardbasicinformation sibi
    ON 
		sibi.Id = siii.StockInwardBasicInformationId
	INNER JOIN 
		product p 
	ON 
		siii.ProductId = p.Id 
	WHERE 
		sibi.WarehouseId = _locationId
	AND 
		p.IsDeleted = 0 
	AND
		DATE(psn.ScanDate) <= _filterMonth
	GROUP BY 
		siii.ProductId
)	;
    DROP TEMPORARY TABLE IF EXISTS ProductSkuDetailsView;
  CREATE TEMPORARY TABLE ProductSkuDetailsView AS (
	SELECT
		siii.ProductId, 
		p.Name ProductSkuName, 
		SUM(siii.quantity) StockQuantity, 
		(p.Price * SUM(siii.quantity)) StockValue
   FROM 
		stockinwarditeminformation siii
	INNER JOIN
		productserialnumber psn
	ON
		siii.Id = psn.StockInwardItemInformationId
   INNER JOIN 
		stockinwardbasicinformation sibi
    ON 
		sibi.Id = siii.StockInwardBasicInformationId
	INNER JOIN 
		product p 
	ON 
		siii.ProductId = p.Id 
	WHERE 
		sibi.WarehouseId = _locationId
	AND 
		p.IsDeleted = 0 
	AND
		DATE(psn.ScanDate)BETWEEN DATE_FORMAT(_filterMonth, '%Y-%m-01') AND _filterMonth
	GROUP BY 
		siii.ProductId);
        
	SELECT 	
		mv.ProductId, 
		ProductSkuName, 
		StockQuantity, 
		StockValue,
        mv.MtdValue
    FROM     
		ProductSkuDetailsView ps
	INNER JOIN 
		MtdValueView mv
	ON mv.ProductId = ps.ProductId;
        
END