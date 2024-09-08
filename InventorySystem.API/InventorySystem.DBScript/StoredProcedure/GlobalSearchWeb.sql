CREATE DEFINER=`inventorydev`@`%` PROCEDURE `GlobalSearchWeb`(
	IN _serialNumber varchar(16)
)
BEGIN
	DECLARE serialNumberId int DEFAULT(SELECT Id FROM productserialnumber WHERE SerialNumber = _serialNumber);
	IF(serialNumberId IS NOT NULL) THEN
        -- Serial Number Details
		SELECT
			snp.Id Id,
			snp.SerialNumber,
			pp.Name Name,
			m.Name Manufacturer,
			pp.Price,
			pp.ProductSku,
			c.Name Category
		FROM
			productserialnumber snp
				INNER JOIN
			stockinwarditeminformation psp ON psp.Id = snp.StockInwardItemInformationId
				INNER JOIN 
			stockinwardbasicinformation ip ON ip.Id = psp.StockInwardBasicInformationId
				LEFT JOIN
			vendor vp ON ip.VendorId = vp.Id
				INNER JOIN
			product pp ON psp.StockInwardBasicInformationId = pp.Id
				INNER JOIN
			Manufacturer m ON pp.ManufacturerId = m.Id
				INNER JOIN
			category c ON pp.CategoryId = c.Id
		WHERE
			snp.SerialNumber = _serialNumber;
                
		-- Serial Number Details
			SELECT 
				at.ActionType  `Transaction`, 
				snh.ActionDate, 
				u.`Name` `User`, 
				wp.LocationName Location, 
				snh.StockInOutOrderNumber PurchasedAndSoldNumber ,
                vp.CompanyName Dealer
			FROM 
				productserialnumberhistory snh 
			INNER JOIN 
				actiontype `at` 
			ON 
				snh.ActionTypeId = at.Id 
			INNER JOIN 
				warehouse wp 
			ON 
				snh.WarehouseId  = wp.Id 
			INNER JOIN 
				user u 
			ON 
				snh.CreatedBy = u.Id 
			INNER JOIN 
				vendor vp
			ON
				vp.Id = snh.BuyerSellerId 
			WHERE 
				snh.ProductSerialNumberId = serialNumberId;
	ELSE 
		-- SELECT 0 IsSuccess, 'Serial Number Not Found' AS Message;
        SELECT 0 Id LIMIT 0;
        SELECT '' `USER` LIMIT 0;
	END IF;
END