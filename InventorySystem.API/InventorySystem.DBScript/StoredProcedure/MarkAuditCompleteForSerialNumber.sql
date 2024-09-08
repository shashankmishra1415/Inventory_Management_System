CREATE DEFINER=`inventorydev`@`%` PROCEDURE `MarkAuditCompleteForSerialNumber`(
    IN _auditId int,
    IN _categoryId int,
    IN _serialNumber varchar(16),
    IN _userId int
)
BEGIN
    DECLARE productSerialNumberId int DEFAULT 0;
    SET 
		productSerialNumberId = (
	SELECT 
		Id
	FROM 
		productserialnumber psn
	WHERE 
		psn.SerialNumber = _serialNumber
	AND 
		CheckForSalesOrderedOrNot(psn.Id)
    );
    IF (
		productSerialNumberId IS NOT NULL
	) 
    AND (
		productSerialNumberId <> 0
	) 
	THEN 
	IF (
	SELECT 
		IsAuditDone
	FROM 
		stockaudititem
	WHERE 
		StockAuditInitiationId = _auditId
	AND 
		CategoryId = _categoryId
	AND 
		ProductSerialNumberId = productSerialNumberId
		) = 1 
	THEN
	SELECT 0 AS IsSuccess, 'Audit already done for this serial number.' AS Message;
	ELSE
	UPDATE 
		stockaudititem
	SET  
		IsAuditDone = 1,
		ModifiedOn = NOW(), 
		ModifiedBy = _userId
	WHERE 
		StockAuditInitiationId = _auditId
	AND 
		CategoryId = _categoryId
	AND 
		ProductSerialNumberId = productSerialNumberId;
	SELECT 1 AS IsSuccess, 'Audit successful.' AS Message;
	END IF;
    ELSE 
	SELECT 0 AS IsSuccess, 'This serial number is either dispatched or not in stock.' AS Message;
    END IF;
END