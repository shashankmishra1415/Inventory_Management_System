CREATE DEFINER=`inventorydev`@`%` PROCEDURE `GetSaleItemsInformationById`(
IN _id INT)
BEGIN
	  
	SELECT
		soii.Id,
		p.ProductSKU ItemSKU, 
		m.`Name` Manufacturer, 
		c.`Name` Category, 
		p.`Name` `Name`, 
		soii.Quantity Quantity,
		GetStockInHandInformationByProductId(soii.ProductId) StockInHand , 
		IscannedZero(soii.Id) NotScannedCount,
        sobi.SaleOrderStatusId
	FROM
		salesorderitemsinformation soii
	INNER JOIN 
		product p
	ON p.Id = soii.ProductId
	LEFT JOIN
		manufacturer m
	ON m.Id = p.ManufacturerId
	LEFT JOIN
		category c
	ON c.Id = p.CategoryId
	INNER JOIN 
		salesorderbasicinformation sobi
	ON soii.SalesOrderBasicInformationId = sobi.Id
	Where 
		soii.IsDeleted=0 
	AND
		sobi.Id=_id;
     
END