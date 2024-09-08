CREATE DEFINER=`inventorydev`@`%` PROCEDURE `GetSalesDirectOutwardDetailsById`(
	IN _soiId INT,
    IN _sobId INT
)
BEGIN
	SELECT
		sobi.Id,
		v.CompanyName Company,
		sobi.SalesOrderNumber SalesOrderNumber,
		w.LocationName Location,
		mt.MovementType MovementType,
		sobi.DateofSale SalesDate
	FROM
		salesorderbasicinformation sobi
	LEFT JOIN
		warehouse w
	ON 
        w.Id = sobi.WarehouseId
	LEFT JOIN
		movementtype mt 
	ON 
		mt.Id = sobi.MovementTypeId
	LEFT JOIN
		vendor v 
	ON 
		v.Id = sobi.VendorId
	WHERE
		sobi.Id = _sobId;
        
SELECT 
		psn.SerialNumber,
        p.Name,
        p.ProductSKU,
        sodn.IsDispatched,
        p.EANCode,
        sodn.IsReturnToManufacturer
	FROM
		saleorderdispatchserialnumber sodn
	INNER JOIN
		productserialnumber psn
	ON
		sodn.ProductSerialNumberId=psn.Id
	LEFT JOIN
		salesorderitemsinformation soii
	ON
		sodn.SalesOrderItemInformationId=soii.Id
	LEFT JOIN
		product p
	ON
		soii.ProductId=p.Id
	WHERE
		sodn.IsDispatched=1
	AND
		sodn.SalesOrderItemInformationId=_soiId;
        
END