CREATE DEFINER=`inventorydev`@`%` PROCEDURE `GetReturnAndDamageManagerApprovalRecord`(
	IN _offset int,
    IN _limit int,
	IN _recordType int,
	IN _fromDate varchar(32),
    IN _toDate varchar(32)
)
BEGIN
	DECLARE totalCount int;
	DECLARE iteration int;
	DECLARE pageNum int DEFAULT(_offset);
	SET _offset=((_offset-1)*_limit);
    
	SET @qury= CONCAT('	SELECT 
			r.id,
			rt.Name RecordType,
			v.CompanyName Vendor,
			r.ReturnDamageType,
			r.RecordId,
			CASE 
				WHEN r.ReturnDamageType = 1 then 1
				ELSE GetNumberOfProductForDamageAndReturnMark(RecordId)
			END NumberOfRecord
		FROM 
			returndamageproductapproval r 
		INNER JOIN
			recordtype rt
		ON
			r.RecordTypeId = rt.Id
		INNER JOIN 
			vendor v 
		ON
			r.VendorId = v.Id 
		WHERE 
			r.Status = 0 ');
            
	IF _recordType <> 0 AND _recordType IS NOT NULL THEN 
		SET @qury=CONCAT(@qury," AND r.RecordTypeId= ",_recordType);
	END IF;
    
	IF !ISNULL(_fromDate) AND !ISNULL(_toDate) AND _fromDate <> '' AND _toDate <> '' THEN
        SET @qury = CONCAT(@qury, " AND DATE(r.CreatedOn) BETWEEN '", _fromDate, "' AND '", _toDate, "'");
    END IF;
    
    SET @limits = CONCAT(@qury,' ORDER BY r.CreatedOn DESC ', " LIMIT ", _limit, " OFFSET ", _offset);    
	PREPARE stmt FROM @limits;
	EXECUTE stmt;
    
	SET @qury2 = CONCAT('SELECT COUNT(1) AS TotalRecord, ',_limit,' PageSize, ', pageNum ,' PageNum FROM (', @qury,' ) counts');
    PREPARE stmt FROM @qury2;
    EXECUTE stmt;
    DEALLOCATE PREPARE stmt;
END