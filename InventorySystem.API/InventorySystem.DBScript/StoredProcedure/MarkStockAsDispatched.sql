CREATE DEFINER=`inventorydev`@`%` PROCEDURE `MarkStockAsDispatched`(
	IN _id int,
    IN _userId int
)
BEGIN
	DECLARE serialProfileId int;
	DECLARE locationId int;
	DECLARE salesOrderId int;
	DECLARE actionType int;
	DECLARE customerId int;

	SELECT
		Id 
	INTO
		actionType
	FROM
		actiontype
	WHERE
		Id=3;
	SELECT 
		 sob.Id,
		 sob.WarehouseId,
		 sodn.ProductSerialNumberId,
		 sob.VendorId
	INTO 
		 salesOrderId,
		 locationId,
		 serialProfileId,
		 customerId
	FROM 
		  saleorderdispatchserialnumber sodn
	INNER JOIN 
		  productserialnumber snp 
	ON    
		  snp.Id = sodn.ProductSerialNumberId
	INNER JOIN 
		  salesorderitemsinformation soi	 
	ON     
		  sodn.SalesOrderItemInformationId = soi.Id
	INNER JOIN 
		   salesorderbasicinformation sob
	ON
		   sob.Id = soi.SalesOrderBasicInformationId
	WHERE 
		   sodn.Id=_id;

	UPDATE
		saleorderdispatchserialnumber
	SET
		IsDispatched = 1,
		DispatchDate = NOW(),
		ModifiedOn = NOW(),
		ModifiedBy = _userId
	WHERE
		Id = _id;
	 
	CALL SaveSerialNumberHistory(
								serialProfileId,
								actionType,
								locationId,
								salesOrderId,
								customerId,
								_userId
								);
		
	SELECT
		1 IsSuccess,
		'Dispatched successful' Message;
END