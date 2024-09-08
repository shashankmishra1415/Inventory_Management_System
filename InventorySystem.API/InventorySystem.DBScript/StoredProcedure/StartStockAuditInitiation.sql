CREATE DEFINER=`inventorydev`@`%` PROCEDURE `StartStockAuditInitiation`(
	IN _warehouseId int,
	IN _auditInitiatedOn datetime,
	IN _userId int
)
BEGIN
	DECLARE auditId int DEFAULT 0;

	IF(SELECT count(1) 
		FROM stockauditinitiation 
		WHERE WarehouseId = _warehouseId 
		AND IsAuditDone = 0)
	THEN
		SELECT 
			0 IsSuccess, 
			'Stock Audit already in progress' Message;
	ELSE 
		INSERT INTO 
			stockauditinitiation(
				WarehouseId,
				AuditInitiatedOn, 
				IsAuditPassed, 
				IsAuditDone, 
				CreatedOn, 
				CreatedBy)
		VALUES(
				_warehouseId, 
				_auditInitiatedOn, 
				0, 
				0, 
				NOW(), 
				_userId);
		
		SET auditId = LAST_INSERT_ID(); 
		
		INSERT INTO 
			stockaudititem(
				StockAuditInitiationId,
				CategoryId,
				ProductId,
				ProductSerialNumberId,
				IsAuditDone,
				CreatedOn,
				CreatedBy)
		SELECT 
			auditId,
			p2.CategoryId,
			p2.Id ProdId,
			ps.Id,
			0,
			NOW(),
			_userId
		FROM 
			productserialnumber ps 
		INNER JOIN
			stockinwarditeminformation sii 
		ON
			ps.StockInwardItemInformationId = sii.Id 
		INNER JOIN 
			stockinwardbasicinformation sib
		ON
			sii.InvoiceNumber = sib.Id 
		INNER JOIN 
			product p
		ON
			sii.ProductId = p.Id 
		WHERE 
			ps.IsScanned = 1
		AND
			sib.WarehouseId = _warehouseId
		AND
			CheckForSalesOrderedOrNot(ps.Id);
	
		SELECT 
			1 IsSuccess, 
			'Stock Audit started successfully' Message;
	END IF;
END