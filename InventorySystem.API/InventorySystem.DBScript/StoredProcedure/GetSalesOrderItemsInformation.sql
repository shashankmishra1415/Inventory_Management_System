CREATE DEFINER=`inventorydev`@`%` PROCEDURE `GetSalesOrderItemsInformation`(
IN _offset int,
IN _limit int
)
BEGIN
DECLARE totalCount int;
DECLARE iteration int;
DECLARE pageNum int DEFAULT(_offset);
SET _offset=((_offset-1)*_limit);
    
     SET @qury = 
     CONCAT( '
         SELECT
				soii.Id Id,
				p.ProductSKU ItemSKU, 
				m.`Name` Manufacturer, 
				c.`Name` Category, 
				p.`Name` `Name`, 
				soii.Quantity Quantity
				FROM
				salesorderitemsinformation soii
				INNER JOIN
				manufacturer m
				INNER JOIN 
				category c
				INNER JOIN 
				product p
				ON p.Id = soii.ProductId
				Where soii.IsDeleted=0 ');
                
	SET @qury = CONCAT(@qury, " LIMIT ", _limit, " OFFSET ", _offset);
	PREPARE stmt FROM @qury;
	EXECUTE stmt;
	DEALLOCATE PREPARE stmt;
    
    SET @qury2 = CONCAT('SELECT COUNT(1) AS TotalRecord, ',_limit,' PageSize, ', pageNum ,' PageNum FROM (', @qury,' ) counts');
	PREPARE stmt2 FROM @qury2;
	EXECUTE stmt2;
    DEALLOCATE PREPARE stmt2;
END