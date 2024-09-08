CREATE DEFINER=`inventorydev`@`%` PROCEDURE `GetStockAuditDetails`(
	IN _id int,
	IN _offset int,
	IN _limit int,
	IN _ProductSKU varchar(64),
	IN _ManufacturerName int,
	IN _CategoryName int
)
BEGIN
	DECLARE totalCount int;
	DECLARE iteration int;
	DECLARE pageNum int DEFAULT(_offset);
	SET _offset=((_offset-1)*_limit);
    
   SET @qury = 
    CONCAT("SELECT 
        tbl.ProductId,
        p.ProductSKU,
        p.Name AS ProductName,
        m.Name AS ManufacturerName,
        c.Name AS CategoryName,
        tbl.Quantity,
        tbl.Scan
    FROM (
        SELECT 
            s.ProductId,
            COUNT(s.ProductId) AS Quantity,
            SUM(s.IsAuditDone) AS Scan,
			s.StockAuditInitiationId 
        FROM 
            stockaudititem s
        WHERE 
            s.StockAuditInitiationId = ",_id,"
        GROUP BY s.ProductId, s.StockAuditInitiationId
    ) tbl
    INNER JOIN stockauditinitiation sai ON sai.Id = tbl.StockAuditInitiationId
    INNER JOIN product p ON tbl.ProductId = p.Id 
    INNER JOIN category c ON p.CategoryId = c.Id 
    INNER JOIN manufacturer m ON p.ManufacturerId = m.Id 
    WHERE  sai.Id =" ,_id);
    
    IF _ProductSKU <> '' AND _ProductSKU is not null
    THEN
		SET  @qury = concat(@qury," AND (p.ProductSKU like '%",_ProductSKU,"%') ");
	END IF;
    
      IF _ManufacturerName <> '' AND _ManufacturerName is not null
    THEN
		SET  @qury = concat(@qury," AND (m.Id = ",_ManufacturerName,") ");
	END IF;
    
      IF _CategoryName <> 0 AND _CategoryName is not null
    THEN
		SET @qury = CONCAT(@qury, " AND  c.Id=", _CategoryName);
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