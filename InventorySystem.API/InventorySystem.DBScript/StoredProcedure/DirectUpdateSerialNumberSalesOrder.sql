CREATE DEFINER=`inventorydev`@`%` PROCEDURE `DirectUpdateSerialNumberSalesOrder`(
	IN _serialNumber varchar(16),
	IN _warehouseId int,
	IN _userId int,
	IN _salesOrderBasicInformationId int
)
BEGIN
	DECLARE tmpProductSerialNumberId int; 
	DECLARE tmpProductId int;
	DECLARE tmpSalesOrderItemInformationId int;
	DECLARE tmpVendorId int;
	SELECT 
		s.Id,
		p.ProductId
	INTO
		tmpProductSerialNumberId,
		tmpProductId
	FROM
		productserialnumber s 
	INNER JOIN
		stockinwarditeminformation p 
	ON
		s.StockInwardItemInformationId = p.Id 
	INNER JOIN 	
		stockinwardbasicinformation i 
	ON
		p.StockInwardBasicInformationId = i.Id 
	WHERE 
	s.SerialNumber = _serialNumber
AND
	s.IsScanned = 1
AND
	i.WarehouseId = _warehouseId;
    
SELECT 
	sob.VendorId 
INTO 
	tmpVendorId 
FROM 
	salesorderbasicinformation sob
WHERE 
sob.Id =_salesOrderBasicInformationId;
IF(tmpProductSerialNumberId IS NOT NULL)THEN
	IF EXISTS(SELECT Id FROM saleorderdispatchserialnumber WHERE ProductSerialNumberId = tmpProductSerialNumberId AND IsDeleted = 0)THEN
		SELECT 'Serial number is already added to sales order.' Message;
	ELSE 
		-- check for this product is already added with this salesorder or not
		SET tmpSalesOrderItemInformationId=(
								SELECT 
									Id 
								FROM 
									salesorderitemsinformation s 
								WHERE 
									SalesOrderBasicInformationId  = _salesOrderBasicInformationId 
								AND 
									ProductId = tmpProductId
							);
		IF(tmpSalesOrderItemInformationId IS NULL)THEN
			INSERT INTO salesorderitemsinformation
			(ProductId, SalesOrderBasicInformationId,Quantity, IsDeleted, CreatedBy, CreatedOn)
			VALUES(tmpProductId, _salesOrderBasicInformationId, 1, 0, _userId, NOW());
			SET tmpSalesOrderItemInformationId = last_insert_id(); 
		ELSE
			UPDATE 
				salesorderitemsinformation
			SET 
				Quantity=Quantity+1,
				ModifiedBy=_userId, 
				ModifiedOn=NOW()
			WHERE 
				Id=tmpSalesOrderItemInformationId;
		END IF;
		
		CALL SaveSerialNumberHistory(tmpProductSerialNumberId, 3, _warehouseId, _salesOrderBasicInformationId, tmpVendorId, _userId);
		INSERT INTO saleorderdispatchserialnumber
		(SalesOrderItemInformationId, ProductSerialNumberId, IsDispatched, IsDeleted, IsReturn, IsDamage, IsReturnToManufacturer, DispatchDate, ReturnDate, DamageDate, CreatedOn, CreatedBy)
		VALUES(tmpSalesOrderItemInformationId, tmpProductSerialNumberId,1, 0, 0, 0, 0, NOW(), NULL, NULL, NOW(), _userId);
		
	
        SELECT
			s.Id,
			p.ProductSKU ItemSKU, 
			m.Name Manufacturer, 
			c.Name Category, 
			p.Name Name, 
			sp.SerialNumber ,
           "Serial Number Scanned" Message
		FROM
			salesorderitemsinformation s
		INNER JOIN 
			product p
		ON p.Id = s.ProductId
		LEFT JOIN
			manufacturer m
		ON m.Id = p.ManufacturerId
		LEFT JOIN 
			category c
		ON c.Id = p.CategoryId
		INNER JOIN 
			salesorderbasicinformation sb
		ON s.SalesOrderBasicInformationId = sb.Id
		INNER JOIN
			saleorderdispatchserialnumber sdn
		ON 
			sdn.SalesOrderItemInformationId=s.Id
		INNER JOIN
			productserialnumber sp
		ON
			sdn.ProductSerialNumberId=sp.Id
		WHERE 
			s.IsDeleted=0 
		AND
			sb.Id=_salesOrderBasicInformationId
		ORDER BY sdn.Id DESC
		LIMIT 1;
	END IF;
ELSE
	SELECT 'Serial number does not exists on this location.' Message;
END IF;
END