CREATE DEFINER=`inventorydev`@`%` PROCEDURE `SaleOrderItemsImportExcelData`(
  IN _data json,
  IN _salesOrderId int,
  IN _userId int
)
BEGIN
	DECLARE totalCount int DEFAULT 0;
   	DECLARE iteration int DEFAULT 0;
    DECLARE serialNumber varchar(64) DEFAULT '';
   	DECLARE productSKUId int DEFAULT 0;
   	DECLARE serialNumberId int DEFAULT 0;
   	DECLARE locationId int;
    DECLARE customerId int;
    DECLARE actionType int; 
    DECLARE var int;
   	DECLARE salesItemsInformationTableId int DEFAULT 0;
    
	SELECT 
		CustomerId 
	INTO 
		customerId 
	FROM 
		salesorderbasicinformation sobi 
	WHERE
		sobi.Id = _salesOrderId;
	SELECT 
		WarehouseId,
        MovementTypeId 
	INTO 
		locationId, 
        var 
	FROM 
		salesorderbasicinformation sobi 
	WHERE 
		sobi.Id = _salesOrderId;
	IF(var = 1) 
    THEN
	SET 
		actionType = 2 ;
	END IF;
	IF(var = 2) 
	THEN
	SET 
		actionType = 6 ;
	END IF;
    
    DROP TEMPORARY TABLE IF EXISTS tmp_tbl;
    CREATE TEMPORARY TABLE IF NOT EXISTS tmp_tbl(
        Id INT PRIMARY KEY AUTO_INCREMENT,
        SerialNumber varchar(64),
        Errors varchar(1024)
    );
   INSERT INTO 
		tmp_tbl(
        SerialNumber
	)
    SELECT
		tbl.SerialNumber
	FROM JSON_TABLE(
		_data, 
		"$[*]" 
	COLUMNS(
		SerialNumber varchar(64) PATH "$.SerialNumber"
		)
	) 
    AS 
		tbl;
	SET 
		totalCount = (
    SELECT 
		count(1) 
	FROM 
		tmp_tbl
	);
	SET 
		iteration = 1;
	WHILE 
		iteration <= totalCount 
    DO
	SELECT 
		tbl.SerialNumber
	INTO
		serialNumber
	FROM 
		tmp_tbl tbl
	WHERE 
		Id = iteration;            
	SELECT 
		psn.Id,
		siii.ProductId 
	INTO 
		serialNumberId,
		productSKUId
	FROM 
		productserialnumber psn
	INNER JOIN 
		stockinwarditeminformation siii 
	ON
		psn.StockInwardItemInformationId = siii.Id 
	WHERE 
		psn.IsScanned = 1
	AND
		psn.SerialNumber = serialNumber;
	IF(
		serialNumberId IS NULL OR serialNumberId = 0
	)
    THEN
	UPDATE 
		tmp_tbl
	SET 
		Errors = 'Serial number does not exist.'
	WHERE 
		Id = iteration;
	ELSE
	IF(
	SELECT 
		COUNT(Id) 
	FROM
		saleorderdispatchserialnumber sodn
	WHERE 
		productserialnumberId = serialNumberId 
	AND 
		IsDeleted = 0)=0 
    THEN
	SET 
		salesItemsInformationTableId = (
	SELECT 
		Id
	FROM 
		salesorderitemsinformation soii 
	WHERE 
		SaleOrderId = _salesOrderId 
	AND 
		ProductSKU = productSKUId);
	IF(
		salesItemsInformationTableId IS NULL
	)
    THEN
	INSERT INTO 
		salesorderitemsinformation(
		ProductSKU,
        SaleOrderId, 
        ItemQuantity,
        IsDeleted,
        CreatedBy,
        CreatedOn
	)
	VALUES(
		productSKUId, 
        _salesOrderId, 
        0, 
        0,
        0, 
        NOW()
        );
	SET 
		salesItemsInformationTableId = LAST_INSERT_ID(); 
	END IF;
	UPDATE 
		salesorderitemsinformation
	SET 
		ItemQuantity = ItemQuantity + 1,
		ModifiedBy = _userId, 
		ModifiedOn = NOW()
	WHERE 
		Id = salesItemsInformationTableId;
	INSERT INTO 
		saleorderdispatchserialnumber(
        SalesOrderItemInformationId,
        productserialnumberId, 
        IsDispatched,
        IsDeleted,
        CreatedOn,
        CreatedBy
	)
	VALUES(
    salesItemsInformationTableId,
    serialNumberId,
    0,
    0,
    NOW(),
    _userId
    );
	CALL 
		SaveSerialNumberHistory(
        serialNumberId,
        actionType,
        locationId, 
        _salesOrderId,
        customerId, 
        _userId
	);
	ELSE
	UPDATE 
		tmp_tbl
	SET 
		Errors = 'Serial number already dispatched or associated to sales order.'
	WHERE 
		Id = iteration;
	END IF;
	END IF;
	SET 
		serialNumber = '';
	SET 
		productSKUId = 0;
	SET 
		serialNumberId = 0;
	SET 
		salesItemsInformationTableId = 0;
	SET 
		iteration = iteration + 1;
   END WHILE;
   SELECT
		tbl.SerialNumber SerialNumber, 
		tbl.Errors `Errors` 
   FROM 
		tmp_tbl tbl 
    WHERE Errors <> '';	
END