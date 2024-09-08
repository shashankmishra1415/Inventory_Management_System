CREATE DEFINER=`inventorydev`@`%` PROCEDURE `ScanAllSerialNumbersSalesOrderScannedUnscanned`(
IN _id int
)
BEGIN
	SELECT
		ps.Id,
		ps.SerialNumber
	FROM
		saleorderdispatchserialnumber sod
	INNER JOIN
		salesorderitemsinformation soi
	ON
		sod.SalesOrderItemInformationId = soi.Id
	INNER JOIN
		salesorderbasicinformation sob
	ON
		soi.SalesOrderBasicInformationId = sob.Id
	INNER JOIN
		productserialnumber ps
	ON
		sod.ProductSerialNumberId = ps.Id
	WHERE
		sob.Id = _id
	AND 
		sod.IsDispatched = 1;

    SELECT
		ps.Id,
		ps.SerialNumber
	FROM
		saleorderdispatchserialnumber sod
	INNER JOIN
		salesorderitemsinformation soi
	ON
		sod.SalesOrderItemInformationId = soi.Id
	INNER JOIN
		salesorderbasicinformation sob
	ON
		soi.SalesOrderBasicInformationId = sob.Id
	INNER JOIN
		productserialnumber ps
	ON
		sod.ProductSerialNumberId = ps.Id
	WHERE
		sob.Id=_id and sod.IsDispatched = 0;
END