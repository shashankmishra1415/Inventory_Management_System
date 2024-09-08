CREATE DEFINER=`inventorydev`@`%` PROCEDURE `GetStockAuditLocation`(
	IN _offset int,
	IN _limit int,
	IN _warehouseId int,
	IN _fromDate datetime,
	IN _toDate datetime,
	IN _userId int,
	IN _status int
)
BEGIN
	DECLARE totalCount int;
	DECLARE iteration int;
	DECLARE pageNum int DEFAULT(_offset);
	SET _offset=((_offset-1)*_limit);
    
     SET @qury = 
     CONCAT("SELECT 
				s.Id,
				w.LocationName WarehouseName,
				u.Name UserName,
				s.AuditInitiatedOn,
				CASE
					WHEN s.IsAuditDone = 1 AND s.IsAuditPassed = 1 THEN 'Pass'
					WHEN s.IsAuditDone = 1 AND s.IsAuditPassed = 0 THEN 'Fail'
					WHEN s.IsAuditDone = 0 AND s.IsAuditPassed = 0 THEN 'In Progress'
				END AuditStatus
			FROM 
				stockauditinitiation s 
			INNER JOIN
				warehouse w 
			ON
				s.WarehouseId = w.Id 
			INNER JOIN 
				user u 
			ON
				s.CreatedBy = u.Id 
			WHERE 
				1=1 
			");    
           
           
	IF _warehouseId <> 0 AND !ISNULL(_warehouseId) 
    THEN
		SET @qury = CONCAT(@qury, " AND  w.Id= ", _warehouseId);
	END IF;
    
    IF !ISNULL(_fromDate) AND !ISNULL(_toDate)  THEN
    SET @qury = CONCAT(@qury, " AND s.AuditInitiatedOn BETWEEN '", _fromDate, "' AND '", _toDate, "'");
END IF;
    
	IF _userId <> 0 AND !ISNULL(_userId)
    THEN
		SET @qury = CONCAT(@qury, " AND  u.Id= ", _userId);
	END IF;
    
    IF _status <> 0 AND !ISNULL(_status)
    THEN
		CASE
        WHEN _status = 1 #PASS
			THEN
			SET  @qury = CONCAT(@qury, " AND s.IsAuditDone = 1 AND s.IsAuditPassed = 1 ");
		
        WHEN _status = 2  #INPROGRESS
			THEN
            SET  @qury = CONCAT(@qury, " AND s.IsAuditDone = 0 AND s.IsAuditPassed = 0 ");
        
        WHEN _status = 3  #FAIL
			THEN
            SET  @qury = CONCAT(@qury, " AND s.IsAuditDone = 1 AND s.IsAuditPassed = 0  ");
        
        END CASE;
	END IF;
   
    SET @qury = CONCAT(@qury, " LIMIT ", _limit, " OFFSET ", _offset);
	PREPARE stmt FROM @qury;
	EXECUTE stmt;
	DEALLOCATE PREPARE stmt;
    
    SET @qury2 = CONCAT('SELECT COUNT(1) AS TotalRecord, ',_limit,' PageSize, ', pageNum ,' PageNum FROM (', @qury,' ) counts');
	PREPARE stmt2 FROM @qury2;
	EXECUTE stmt2;
    DEALLOCATE PREPARE stmt2;
END