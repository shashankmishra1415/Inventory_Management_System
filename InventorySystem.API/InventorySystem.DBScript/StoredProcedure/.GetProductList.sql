CREATE DEFINER=`inventorydev`@`%` PROCEDURE `GetProductList`(
	IN _productSku varchar(64), 
    IN _productName varchar(500),  
    IN _categoryId int, 
    IN _manufacturerId int,
    IN _eancode varchar(64),  
    IN _limit int, 
    IN _offset int
)
BEGIN
	
    DECLARE totalCount int;
	DECLARE iteration int;
	DECLARE pageNum int DEFAULT(_offset);
	SET _offset=((_offset-1)*_limit);
	
	SET @qury = concat("SELECT 
		p.Id,
		p.ProductSku,
		p.Name,
		pc.Name AS CategoryName,
		mn.Name AS ManufactrerName,
		p.EanCode,
		p.Price,
		a.Status AS IsActive
	FROM
		product p
			Left JOIN
		category pc ON p.CategoryId = pc.Id
			LEFT JOIN
		manufacturer mn ON p.ManufacturerId = mn.Id
			INNER JOIN
		activeprofile a ON p.IsActive = a.Id
	WHERE
		p.IsDeleted = 0 ");
    
	IF !ISNULL(_productSku) AND _productSku <> '' THEN 
		SET @qury = CONCAT(@qury, " AND p.ProductSku LIKE '%", _productSku, "%' "); 
    END IF; 
    IF !ISNULL(_productName) AND _productName <> '' THEN 
		SET @qury = CONCAT(@qury, " AND p.Name Like '%", _productName, "%' ") ; 
    END IF ; 
    IF (_categoryId != 0) AND _categoryId is not null THEN 
		SET @qury = CONCAT(@qury, "AND pc.Id =" , _categoryId) ; 
	END IF ; 
    IF (_manufacturerId != 0) AND _manufacturerId is not null THEN 
		SET @qury = CONCAT(@qury, " AND mn.Id =" , _manufacturerId) ; 
    END IF ; 
    IF !ISNULL(_eancode) AND _eancode <> '' THEN 
		SET @qury = CONCAT(@qury, " AND p.EANCode LIKE '%", _eancode, "%' "); 
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