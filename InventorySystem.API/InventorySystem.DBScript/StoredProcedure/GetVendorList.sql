CREATE DEFINER=`inventorydev`@`%` PROCEDURE `GetVendorList`(
	IN _limit int,
    IN _offset int,
    IN _companyName varchar(64),
    IN _typeId int,
    IN _vendorTypeId int,
    IN _contactName varchar(64),
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
			vp.Id,
			vp.CompanyName,
			ct.CompanyType,
			vt.VendorType,
			vp.ContactName,
            vp.IsActive
		FROM 
			vendor vp
		LEFT JOIN
			companytype ct
		ON
			vp.CompanyTypeId=ct.Id
		LEFT JOIN
			vendortype vt
		ON
			vp.VendorTypeId=vt.Id
		WHERE 
			vp.IsDeleted=0 ');
    
	IF _companyName <> '' AND _companyName IS NOT NULL 
    THEN
		SET @qury = concat(@qury," AND vp.CompanyName like '%",_companyName,"%' ");
	END IF;
    
    IF _typeId <> 0 and _typeId is not null
    THEN
		SET @qury = CONCAT(@qury, " AND vp.CompanyTypeId=", _typeId);
	END IF;
    
    IF _statusId <> 0 and _statusId is not null
    THEN
		SET @qury = CONCAT(@qury, " AND vp.IsActive=", _statusId);
	END IF;
     
    IF _vendorTypeId <> 0 and _vendorTypeId is not null
    THEN
		SET @qury = CONCAT(@qury, " AND vp.VendorTypeId=", _vendorTypeId);
	END IF;
    
    IF _contactName <> '' AND _contactName IS NOT NULL 
    THEN
		SET @qury = concat(@qury," AND vp.ContactName like '%",_contactName,"%' ");
	END IF;
    
	SET @limits = CONCAT(@qury, " LIMIT ", _limit, " OFFSET ", _offset);
	PREPARE stmt FROM @limits;
	EXECUTE stmt;
	DEALLOCATE PREPARE stmt;
    
    SET @qury2 = CONCAT('SELECT COUNT(1) AS TotalRecord, ',_limit,' PageSize, ', pageNum ,' PageNum FROM (', @qury,' ) counts');
	PREPARE stmt2 FROM @qury2;
	EXECUTE stmt2;
    DEALLOCATE PREPARE stmt2;
END