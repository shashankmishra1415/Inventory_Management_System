CREATE DEFINER=`inventorydev`@`%` PROCEDURE `GetWarehouseList`(
	IN _limit int,
    IN _offset int,
    IN _typeId int,
    IN _capacity double,
    IN _statusId int,
    IN _locationName varchar(64)
)
BEGIN
	DECLARE totalCount int;
	DECLARE iteration int;
	DECLARE pageNum int DEFAULT(_offset);
	SET _offset=((_offset-1)*_limit);
    
    SET @qury = 
     CONCAT('
		SELECT 
			wp.`Id`,
			wt.`Type`,
			wp.`MaxCapacity` Capacity,
			wp.`LocationName`,
			wp.`IsActive` AS Status
		FROM 
			Warehouse wp
		LEFT JOIN
			WarehouseType wt
		ON
			wp.WarehouseTypeId=wt.Id
		 Where 
         wp.IsDeleted=0	');
    
           
	IF _locationName <> '' and _locationName is not null
    then
		set  @qury = concat(@qury," AND (wp.LocationName like '%",_locationName,"%') ");
	END IF;
    
    IF _typeId <> 0 and _typeId is not null
    THEN
		-- SET @qury = CONCAT(@qury, " AND wp.TypeId=", _typeId);
        SET @qury = CONCAT(@qury, " AND wt.Id =", _typeId);
	END IF;
    
    IF _statusId <> 0 and _statusId is not null
    THEN
		-- SET @qury = CONCAT(@qury, " AND wp.IsActive=", _statusId);
        SET @qury = CONCAT(@qury, " AND wp.IsActive=", _statusId);
	END IF;
    IF _capacity <> 0 and _capacity is not null
    THEN
		SET @qury = CONCAT(@qury, " AND wp.MaxCapacity=", _capacity);
	END IF;

    SET @limits = CONCAT(@qury, " LIMIT ", _limit, " OFFSET ", _offset);
    -- SELECT @qury;
	PREPARE stmt FROM @limits;
	EXECUTE stmt;
	DEALLOCATE PREPARE stmt;
    
    SET @qury2 = CONCAT('SELECT COUNT(1) AS TotalRecord, ',_limit,' PageSize, ', pageNum ,' PageNum FROM (', @qury,' ) counts');
	PREPARE stmt2 FROM @qury2;
	EXECUTE stmt2;
    DEALLOCATE PREPARE stmt2;
END