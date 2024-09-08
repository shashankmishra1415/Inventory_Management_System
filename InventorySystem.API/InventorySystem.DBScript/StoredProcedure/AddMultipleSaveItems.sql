CREATE DEFINER=`inventorydev`@`%` PROCEDURE `AddMultipleSaveItems`(
IN _userId int,
IN _data json
)
BEGIN
    
	INSERT INTO salesorderitemsinformation
			(SalesOrderBasicInformationId,ProductId,Quantity,IsDeleted,CreatedBy,CreatedOn)
	SELECT
        tbl.SalesOrderBasicInformationId As SaleOrderId,
		tbl.ProductId As ProductSKU,
		tbl.Quantity As ItemQuantity,
        0,
		_userId,
		NOW()
	FROM 
		JSON_TABLE(_data,'$[*]' COLUMNS(
            SalesOrderBasicInformationId int PATH "$.SaleOrderId",
			ProductId int PATH "$.ProductSKU",
			Quantity int PATH "$.ItemQuantity"
		)) AS tbl;
        	
END