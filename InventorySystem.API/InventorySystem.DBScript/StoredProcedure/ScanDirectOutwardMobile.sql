CREATE DEFINER=`inventorydev`@`%` PROCEDURE `ScanDirectOutwardMobile`(
	IN _serialNumber varchar(16),
	IN _salesOrderId int,
	IN _userId int
	)
BEGIN
	DECLARE serialNumberId int;
	DECLARE productId int;
	DECLARE salesOrderItemId int;
	DECLARE customerId int;
	
	SELECT 
		VendorId
	INTO 
		customerId
	FROM 
		salesorderbasicinformation
	WHERE 
		Id = _salesOrderId;
		
	SELECT 
		ps.Id,
		si.ProductId
	INTO
		serialNumberId,
		productId
	FROM 
		productserialnumber ps 
	INNER JOIN
		stockinwarditeminformation si
	ON
		ps.StockInwardItemInformationId = si.Id 
	WHERE 
		ps.SerialNumber = _serialNumber
	AND
		ps.IsDamage=0
	AND
		ps.IsScanned = 1;
		
	IF(serialNumberId IS NOT NULL AND serialNumberId <> 0)
	THEN 
		IF(SELECT count(1) FROM saleorderdispatchserialnumber sod WHERE sod.ProductSerialNumberId = serialNumberId AND sod.IsDeleted = 0)>0 
		THEN 
			SELECT 
				0 IsSuccess, 
				'Serial Number is already added in sales order.' Message;
		ELSE 
			SET salesOrderItemId = (
				SELECT Id 
				FROM salesorderitemsinformation  
				WHERE SalesOrderBasicInformationId = _salesOrderId 
				AND ProductId = productId);

			IF(salesOrderItemId IS NULL)
			THEN
				INSERT INTO
					salesorderitemsinformation
					(ProductSKU, 
					SaleOrderId,
					Quantity,
					IsDeleted,
					CreatedBy,
					CreatedOn)
				VALUES(
					productId, 
					_salesOrderId, 
					0, 
					0, 
					_userId, 
					NOW());
			
				SET salesOrderItemId = last_insert_id(); 
			END IF;
			
			UPDATE 
				salesorderitemsinformation
			SET 
				Quantity = Quantity + 1,
				ModifiedBy = _userId,
				ModifiedOn = NOW()
			WHERE 
				Id = salesOrderItemId;
			
			INSERT INTO
				saleorderdispatchserialnumber(
				SalesOrderItemInformationId, 
				ProductSerialNumberId, 
				IsDispatced, 
				IsDeleted, 
				DispatchDate, 
				CreatedOn, 
				CreatedBy)
			VALUES(
				salesOrderItemId,
				serialNumberId,
				1,
				0,
				NOW(),
				NOW(),
				_userId);	

			SELECT 
				WarehouseId 
			INTO 
				@locationId 
			FROM 
				salesorderbasicinformation 
			WHERE 
				Id = _salesOrderId;

			CALL SaveSerialNumberHistory(serialNumberId, 3, @locationId, _salesOrderId,customerId, _userId);

			SELECT 
				1 IsSuccess, 
				'Serial Number added in sales order.' Message;
		END IF;
	ELSE
		SELECT 
			0 IsSuccess, 
			'Serial Number does not exists / Serial number is already damaged.' Message;
	END IF;
END