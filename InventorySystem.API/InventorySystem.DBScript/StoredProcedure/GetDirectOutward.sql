CREATE DEFINER=`inventorydev`@`%` PROCEDURE `GetDirectOutward`(
IN _pageNum int, 
IN _pageSize int
)
BEGIN 
	DECLARE totalCount int;
	DECLARE iteration int;
	DECLARE _offset int; 
	DECLARE pageNum int DEFAULT(_offset);
    SET _offset = (_pageNum - 1) * _pageSize; 
    SET @query = CONCAT('
    SELECT
		sob.Id,
		vp.CompanyName Company,
		sob.SalesOrderNumber,
		wp.LocationName Location,
		mt.MovementType,
		date_format(sob.DateofSale, "%d %b %Y %h:%i %p") SalesDate,
		ot.TYPE OutType
	FROM
		salesorderbasicinformation sob
	LEFT JOIN
		warehouse wp 
	ON 
		wp.Id = sob.WarehouseId
	LEFT JOIN
		movementtype mt 
	ON 
		mt.Id = sob.MovementTypeId
	LEFT JOIN
		vendor vp 
	ON 	
		vp.Id = sob.VendorId
	LEFT JOIN
		outtype ot 
	ON 
		ot.Id = sob.OutTypeId 
	Where 
		sob.OutTypeId = 1 '
     ); 
    
    SET @query = CONCAT(@query, ' ORDER BY  sob.Id LIMIT ', _offset, ', ', _pageSize); 
	
    PREPARE stmt FROM @query; 
    EXECUTE stmt; 
    DEALLOCATE PREPARE stmt; 
        SET @qury2 = CONCAT('SELECT COUNT(*) AS TotalRecord, ',_pageSize,' PageSize, ', _pageNum ,' PageNum FROM (', @query,' ) counts');
	PREPARE stmt2 FROM @qury2;
	EXECUTE stmt2;
    DEALLOCATE PREPARE stmt2;
END