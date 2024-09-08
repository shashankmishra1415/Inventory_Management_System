CREATE DEFINER=`backup`@`%` PROCEDURE `GetStockOutwardList`(
IN _pageNum INT, 
IN _pageSize INT, 
IN _invoiceNo VARCHAR(32), 
IN _vendorCompanyNameId INT, 
IN _movementTypeId INT, 
IN _warehouseLocationId INT, 
IN _fromDate DATETIME,
IN _toDate DATETIME 
)
BEGIN 
	DECLARE totalCount int;
	DECLARE iteration int;
	DECLARE _offset INT; 
	DECLARE pageNum int DEFAULT(_offset);
    SET _offset = (_pageNum - 1) * _pageSize; 

    SET @query = CONCAT('SELECT
    sob.Id,
    vp.CompanyName Company,
    sob.SalesOrderNo,
    wp.LocationName Location,
    mt.MovementType,
    date_format(sob.DateofSale, "%d %b %Y %h:%i %p") SalesDate
FROM
    salesorderbasicinformation sob
        LEFT JOIN
    warehouseprofile wp ON wp.Id = sob.WarehouseId
        LEFT JOIN
    movementtype mt ON mt.Id = sob.MovementTypeId
        LEFT JOIN
    vendorprofile vp ON vp.Id = sob.CustomerId Where 1 = 1 ' ); 

    IF _invoiceNo IS NOT NULL AND _invoiceNo != '' THEN 
        SET @query = CONCAT(@query, ' AND sob.SalesOrderNo = ("', _invoiceNo,'") '); 
    END IF; 

    IF _vendorCompanyNameId > 0 THEN 
        SET @query = CONCAT(@query, ' AND sob.CustomerId = ', _vendorCompanyNameId,' '); 
    END IF; 

    IF _movementTypeId > 0 THEN 
        SET @query = CONCAT(@query, ' AND sob.MovementTypeId = ', _movementTypeId); 
    END IF; 

    IF _warehouseLocationId > 0 THEN 
        SET @query = CONCAT(@query, ' AND sob.WarehouseId = ', _warehouseLocationId); 
    END IF; 

    IF _fromDate IS NOT NULL AND _fromDate != '0001-01-01 00:00:00' THEN 
        SET @query = CONCAT(@query, ' AND (DATE(sob.DateofSale) BETWEEN DATE("',_fromDate,'") AND DATE("',_toDate,'")) '); 
    END IF; 

    SET @query = CONCAT(@query, ' ORDER BY  sob.Id LIMIT ', _offset, ', ', _pageSize); 
	
    PREPARE stmt FROM @query; 
    EXECUTE stmt; 
    DEALLOCATE PREPARE stmt; 

        SET @qury2 = CONCAT('SELECT COUNT(*) AS TotalRecord, ',_pageSize,' PageSize, ', _pageNum ,' PageNum FROM (', @query,' ) counts');
	PREPARE stmt2 FROM @qury2;
	EXECUTE stmt2;
    DEALLOCATE PREPARE stmt2;

END