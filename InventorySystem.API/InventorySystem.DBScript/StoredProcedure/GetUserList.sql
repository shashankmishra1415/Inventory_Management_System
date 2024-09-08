CREATE DEFINER=`inventorydev`@`%` PROCEDURE `GetUserList`(
	IN _limit int,
    IN _offset int,
    IN _name varchar(64),
    IN _departmentId int,
    IN _warehouseId int,
    IN _mobile varchar(64),
    IN _status int
)
BEGIN
	DECLARE totalCount int;
	DECLARE iteration int;
	DECLARE pageNum int DEFAULT(_offset);
	SET _offset=(_offset-1)*_limit;
    
    SET @qury = 
     CONCAT('
			SELECT
				up.Id,
				up.Name,
				rp.Name As Role,
				wp.LocationName,
				up.Mobile,
				up.Status 		
			FROM 
				user up
				INNER JOIN           
				Warehouse wp
					ON 
				up.WarehouseId=wp.Id
				INNER JOIN
				userrole rp
					ON
				up.UserRoleId=rp.Id		
			WHERE 
				up.IsDeleted=0 ');
            
	 IF !ISNULL(_name) AND  _name<>''
     THEN
     SET @qury = CONCAT(@qury, "AND up.Name LIKE '%",_name,"%' ") ;
     END IF;
     
	 IF !ISNULL(_mobile) AND  _mobile<>''
     THEN
     SET @qury = CONCAT(@qury, "AND up.Mobile LIKE '%",_mobile,"%' ") ;
     END IF;    
     
	IF _departmentId <> 0 and _departmentId is not null
    THEN
	SET @qury = CONCAT(@qury, " AND up.UserRoleId=", _departmentId);
	END IF;
    
    IF _status <> 0 and _status is not null
    THEN
	SET @qury = CONCAT(@qury, " AND up.Status=", _status);
	END IF;
       
	IF _warehouseId <> 0 and _warehouseId is not null
    THEN
	SET @qury = CONCAT(@qury, " AND up.WarehouseId=", _warehouseId);
	END IF;
	
    SET @limits = CONCAT(@qury, " LIMIT ", _limit, " OFFSET ", _offset);
	PREPARE stmt FROM @limits;
	EXECUTE stmt;
	DEALLOCATE PREPARE stmt;
    
    SET @qury2 = CONCAT('SELECT COUNT(*) AS TotalRecord, ',_limit,' PageSize, ', pageNum ,' PageNum FROM (', @qury,' ) counts');
	PREPARE stmt2 FROM @qury2;
	EXECUTE stmt2;
    DEALLOCATE PREPARE stmt2;
END