CREATE DEFINER=`inventorydev`@`%` PROCEDURE `GetStockAuditDetailsById`(
	IN _id int
)
BEGIN
	SELECT 
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
            s.StockAuditInitiationId = _id
        GROUP BY s.ProductId, s.StockAuditInitiationId
    ) tbl
    INNER JOIN
    stockauditinitiation sai
    ON 
    sai.Id = tbl.StockAuditInitiationId
    INNER JOIN
    product p
    ON
    tbl.ProductId = p.Id 
    INNER JOIN
    category c 
    ON 
    p.CategoryId = c.Id 
    INNER JOIN 
    manufacturer m 
    ON 
    p.ManufacturerId = m.Id 
    Where 
    sai.Id = _id;
END