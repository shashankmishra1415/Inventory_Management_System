CREATE DEFINER=`inventorydev`@`%` PROCEDURE `SaveSalesOrderBasicInformation`(
	IN _salesOrderNo varchar(32),
	IN _dateOfSale varchar(32),
	IN _customerId int,
	IN _movementTypeId int,
	IN _warehouseId int,
	IN _outType int,
	IN _userId int
)
BEGIN
	INSERT INTO 
		SalesOrderBasicInformation
        (
			SalesOrderNumber, 
			DateOfSale, 
			VendorId, 
			MovementTypeId, 
			WarehouseId,
			OutTypeId,
			SaleOrderStatusId,
			CreatedBy, 
			CreatedOn
		)
	VALUES
		(
			_salesOrderNo,
			_dateOfSale,
			_customerId,
			_movementTypeId,
			_warehouseId,
			_outType,
	IF ( 
		_outType = 1, 3, 2 ),
		_userId,
		NOW()
	);
	SELECT 
		last_insert_id() id,
        'Sales Order Added Successfully.' Message, 
        1 IsSuccess;
END