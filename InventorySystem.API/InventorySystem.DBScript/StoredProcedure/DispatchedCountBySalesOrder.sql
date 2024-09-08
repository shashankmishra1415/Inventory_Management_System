CREATE DEFINER=`inventorydev`@`%` PROCEDURE `DispatchedCountBySalesOrder`(
	IN _salesOrderId int
)
BEGIN
	SELECT 
		COUNT(1) TotalCount,
        SUM(sods.IsDispatched) ScannedCount
	FROM
		saleorderdispatchserialnumber sods
		INNER JOIN
        salesorderitemsinformation soii 
		ON 
        sods.SalesOrderItemInformationId = soii.Id
	WHERE
		sods.IsDeleted = 0
	AND 
		soii.SalesOrderBasicInformationId = _salesOrderId;
END