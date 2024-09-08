CREATE DEFINER=`inventorydev`@`%` PROCEDURE `GetSalesOutwardDetailsById`(
    IN _id int
)
BEGIN
	SELECT 
		sob.Id,
		vp.CompanyName Company,
		sob.SalesOrderNumber ,
		wp.LocationName Location,
		mt.MovementType MovementType,
		sob.DateofSale SalesDate,
		ot.TYPE OutType
	FROM
		salesorderbasicinformation sob
		LEFT JOIN
		warehouse wp
        ON
        wp.Id = sob.WarehouseId
		LEFT JOIN
		movementtype mt
        ON 
        mt.Id = sob.MovementTypeId
		LEFT JOIN
		vendor vp 
        ON
        vp.Id = sob.VendorId
		LEFT JOIN 
		outtype ot
        ON 
        sob.OutTypeId = ot.Id
	WHERE
		sob.Id = _id;
        
	SELECT 
		sod.Id,
		sod.IsDispatched,
		snp.SerialNumber,
		pp.ProductSKU,
		pp.Name,
		sod.IsReturnToManufacturer
	FROM
		saleorderdispatchserialnumber sod
		INNER JOIN
		salesorderitemsinformation soii
        ON 
        sod.SalesOrderItemInformationId = soii.Id
		INNER JOIN
		productserialnumber snp
        ON 
        sod.ProductSerialNumberId = snp.Id
		INNER JOIN
		stockinwarditeminformation psp
        ON 
        psp.Id = snp.StockInwardItemInformationId
		INNER JOIN
		product pp
        ON 
        pp.Id = psp.StockInwardBasicInformationId
	WHERE
		soii.SalesOrderBasicInformationId = _id
	ORDER BY 
		sod.IsDispatched;
END