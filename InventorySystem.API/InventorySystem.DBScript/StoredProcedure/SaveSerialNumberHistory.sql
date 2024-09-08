CREATE DEFINER=`inventorydev`@`%` PROCEDURE `SaveSerialNumberHistory`(
	IN _serialNumberId int,
	IN _actionTypeId int,
	IN _locationId int,
	IN _stockInOutOrderNumber int,
	in _buyerSellerId int,
	IN _userId int
)
BEGIN       
	DECLARE invoiceSalesOrderNo varchar(32);
	IF( _actionTypeId = 1 OR _actionTypeId = 8 OR _actionTypeId = 9 OR _actionTypeId = 10 ) 
	THEN
		SET invoiceSalesOrderNo = (
				SELECT 
					InvoiceNumber 
				FROM 
					stockinwardbasicinformation 
				WHERE 
					Id = _stockInOutOrderNumber) ;
	ELSE 
		SET invoiceSalesOrderNo = (
				SELECT 
					SalesOrderNumber 
				FROM 
					salesorderbasicinformation
				WHERE 
					Id = _stockInOutOrderNumber) ;
	END IF;

	INSERT INTO  
		productserialnumberhistory(
			ProductSerialNumberId, 
			ActionTypeId, 
			ActionDate, 
			WarehouseId, 
			StockInOutOrderNumber, 
			BuyerSellerId,
			CreatedBy, 
			CreatedOn)
	VALUES (
		_serialNumberId,
		_actionTypeId,
		NOW(),
		_locationId,
		invoiceSalesOrderNo,
		_buyerSellerId,
		_userId,
		NOW());
END