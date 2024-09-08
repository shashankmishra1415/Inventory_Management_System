CREATE DEFINER=`inventorydev`@`%` PROCEDURE `GetDispatchedGoodsDetailsByMonthByLocationId`(
	IN _filterMonth date,
    IN _locationId int
)
BEGIN
	
    DROP TEMPORARY TABLE IF EXISTS MtdValueView;
	CREATE TEMPORARY TABLE MtdValueView AS (
    SELECT
		ps.ProductId, 
		(pp.Price * SUM(ps.quantity)) MtdValue
    FROM 
		stockinwarditeminformation ps
	INNER JOIN
		productserialnumber snp 
	ON
		ps.Id = snp.StockInwardItemInformationId 
	RIGHT JOIN 
		saleorderdispatchserialnumber sod
	ON
		sod.ProductSerialNumberId = snp.Id
	INNER JOIN 
		salesorderitemsinformation soii
    ON 
		soii.Id = sod.SalesOrderItemInformationId
	INNER JOIN
		salesorderbasicinformation sobi
	ON		
		soii.SalesOrderBasicInformationId = sobi.Id
	INNER JOIN 
		product pp 
	ON 
		ps.ProductId = pp.Id 
	WHERE 
		sobi.WarehouseId = _locationId
	AND 
		sod.IsDispatched = 1
	AND 
		pp.IsDeleted = 0 
	AND
		DATE(snp.ScanDate) < _filterMonth
	GROUP BY 
		ps.ProductId );
 DROP TEMPORARY TABLE IF EXISTS ProductSkuDetailsView;
  CREATE TEMPORARY TABLE ProductSkuDetailsView AS (
	SELECT
		ps.ProductId, 
		pp.Name ProductSkuName, 
		SUM(ps.quantity) StockQuantity, 
		(pp.Price * SUM(ps.quantity)) StockValue
    FROM 
		stockinwarditeminformation ps
	INNER JOIN
		productserialnumber snp 
	ON
		ps.Id = snp.StockInwardItemInformationId 
	RIGHT JOIN 
		saleorderdispatchserialnumber sod
	ON
		sod.ProductSerialNumberId = snp.Id
	INNER JOIN 
		salesorderitemsinformation soii
    ON 
		soii.Id = sod.SalesOrderItemInformationId
	INNER JOIN
		salesorderbasicinformation sobi
	ON		
		soii.SalesOrderBasicInformationId = sobi.Id
	INNER JOIN 
		product pp 
	ON 
		ps.ProductId = pp.Id 
	WHERE 
		sobi.WarehouseId = _locationId
	AND 
		sod.IsDispatched = 1
	AND
		pp.IsDeleted = 0 
	AND
		DATE(snp.ScanDate) BETWEEN DATE_FORMAT(_filterMonth, '%Y-%m-01') AND _filterMonth
	GROUP BY 
		ps.ProductId);
        
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