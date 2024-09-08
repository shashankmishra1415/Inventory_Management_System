CREATE DEFINER=`inventorydev`@`%` PROCEDURE `ScanAllInfoSalesOrder`(
IN _id int
)
BEGIN
	SELECT 
		sob.Id,
		sob.SalesOrderNumber,
		sob.DateofSale,
		v.CompanyName,
		mt.MovementType,
		w.LocationName,
		w.Id LocationId,
		sob.OutTypeId
	FROM
		salesorderbasicinformation sob
	LEFT JOIN
		movementtype mt
	ON
		sob.MovementTypeId = mt.Id
	LEFT JOIN
		vendor v
	ON
		sob.VendorId = v.Id
	INNER JOIN
		warehouse w
	ON
		sob.WarehouseId = w.Id
	WHERE
		sob.Id = _id;
		
	SELECT
		soi.Id,
		p.ProductSKU ItemSku, 
		m.Name Manufacturer, 
		c.Name Category, 
		p.Name `Name`, 
		ps.SerialNumber
	FROM
		salesorderitemsinformation soi
	INNER JOIN 
		product p
	ON 
		p.Id = soi.ProductId
	LEFT JOIN	
		manufacturer m
	ON 
		m.Id = p.ManufacturerId
	LEFT JOIN 
		category c
	ON 
		c.Id = p.CategoryId
	INNER JOIN 
		salesorderbasicinformation sb
	ON 
		soi.SalesOrderBasicInformationId = sb.Id
	INNER JOIN
		saleorderdispatchserialnumber sdn
	ON 
		sdn.SalesOrderItemInformationId = soi.Id
	INNER JOIN
		productserialnumber ps
	ON
		sdn.ProductSerialNumberId = ps.Id
	WHERE 
		soi.IsDeleted=0 
	AND
		sb.Id=_id
	ORDER BY sdn.Id DESC;
END