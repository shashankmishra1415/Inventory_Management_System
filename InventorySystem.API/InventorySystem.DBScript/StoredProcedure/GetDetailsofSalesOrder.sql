CREATE DEFINER=`inventorydev`@`%` PROCEDURE `GetDetailsofSalesOrder`(
IN _offset int,
IN _limit int,
IN _orderNumber varchar(32),
IN _customerId int,
IN _movementTypeId int,
IN _warehouseId int,
IN _outType int,
IN _fromDate varchar(16),
IN _toDate varchar(16),
IN _statusId int
)
BEGIN
	DECLARE totalCount int;
	DECLARE iteration int;
	DECLARE pageNum int DEFAULT(_offset);
	SET _offset=((_offset-1)*_limit);
    
	SET @qury = 
	CONCAT('
		SELECT 
			s.`Id` Id,
			s.`SalesOrderNumber` OrderNo,
            v.`CompanyName` Customer,
			m.`MovementType` MovementType, 
			wp.`LocationName` SourceLocation,
			s.`DateofSale` As DateTime,
            so.StatusType Status,
            ot.TYPE OutType
		FROM 
			SalesOrderBasicInformation s
		INNER JOIN 
            outtype ot 
		ON 
			s.OutTypeId = ot.Id 
		INNER JOIN 
            vendor v
		ON
            v.Id=s.VendorId
		INNER JOIN
            salesordermovementtype m
		ON
            m.Id = s.MovementTypeId
		INNER JOIN
			warehouse wp
		ON
			wp.Id=s.WarehouseId
		INNER JOIN
            saleorderstatus so
		ON
            so.Id = s.SaleOrderStatusId
		WHERE 
            s.IsDeleted=0 ');    
           -- AND s.StatusId=2 
           
	IF _orderNumber <> '' AND _orderNumber IS NOT NULL
    THEN
		SET  @qury = concat(@qury," AND (s.SalesOrderNumber like '%",_orderNumber,"%') ");
	END IF;
    
    IF _customerId <> 0 AND _customerId IS NOT NULL
    THEN
		SET @qury = CONCAT(@qury, " AND  s.VendorId=", _customerId);
	END IF;
    
    IF _movementTypeId <> 0 AND _movementTypeId IS NOT NULL
    THEN
		SET @qury = CONCAT(@qury, " AND s.MovementTypeId=", _movementTypeId);
	END IF;
     
    IF _warehouseId <> 0 AND _warehouseId IS NOT NULL
    THEN
		SET @qury = CONCAT(@qury, " AND s.WarehouseId=", _warehouseId);
	END IF; 
    
	IF _outType <> 0 AND _outType IS NOT NULL
    THEN
		SET @qury = CONCAT(@qury, " AND s.OutTypeId=", _outType);
	END IF; 
    
	IF _fromDate IS NOT NULL AND !ISNULL(_toDate) 
	THEN
		SET @qury = CONCAT(@qury, ' AND s.DateofSale BETWEEN ''', _fromDate, ''' AND ''', _toDate, '''');
	END IF;
    
	IF _statusId <> 0 AND _statusId IS NOT NULL
    THEN
		SET @qury = CONCAT(@qury, " AND s.SaleOrderStatusId= ", _statusId);
	END IF; 
   
    SET @qury = CONCAT(@qury, " ORDER BY s.Id DESC LIMIT ", _limit, " OFFSET ", _offset);
	PREPARE stmt FROM @qury;
	EXECUTE stmt;
	DEALLOCATE PREPARE stmt;
    
    SET @qury2 = CONCAT('SELECT COUNT(1) AS TotalRecord, ',_limit,' PageSize, ', pageNum ,' PageNum FROM (', @qury,' ) counts');
	PREPARE stmt2 FROM @qury2;
	EXECUTE stmt2;
    DEALLOCATE PREPARE stmt2;
END