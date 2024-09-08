CREATE DEFINER=`inventorydev`@`%` PROCEDURE `GetInventoryDetailByBrandForLocation`(
	IN _warehouseId int
)
BEGIN
	/*select 
		m.Id,
		m.Name,
		tbl.TotalPrice,
        GetDataForJsonInventory(_warehouseId,tbl.ManufacturerId) AS JsonList
	from(
		select 
			pp.ManufacturerId,
			sum(pp.Price) TotalPrice
		from 
			invoiceprofile inv 
		inner join
			productskuprofile psp 
		on
			inv.Id = psp.InvoiceNoId 
		inner join 	
			productsprofile pp 
		on
			psp.ProductSkuId = pp.Id 
		inner join 
			serialnumberprofile s 
		on
			psp.Id = s.ProductSkuProfileId 
		where 
			inv.WarehouseLocationId = _warehouseId
		and
			s.IsScanned = 1
		and
			CheckForSalesOrderedOrNot(s.Id)
		group by 
			pp.ManufacturerId
	)tbl
    
	inner join 
		manufacturer m
	on
		tbl.ManufacturerId = m.Id;*/
	SELECT 
		tbl.ManufacturerId Id,
		tbl.Manufacturer Name,
		JSON_ARRAYAGG(tbl.Products)JsonList,
		SUM(tbl.Price) TotalPrice
	FROM 
	(SELECT 
		tbl.ManufacturerId,
		tbl.Manufacturer,
		JSON_OBJECT("Name",tbl.ProductName,"TotalPrice",SUM(tbl.Price),"Quantity",SUM(tbl.Quantity))Products,
		SUM(tbl.Price)Price
	FROM 
	(
		SELECT 
			pp.ManufacturerId,
			m.Name Manufacturer,
			pp.Id ProductId,
			pp.Name ProductName,
			SUM(psp.Price)Price,
			COUNT(s.Id)Quantity
		FROM 
			stockinwardbasicinformation inv 
		INNER JOIN
			stockinwarditeminformation psp 
		ON
			inv.Id = psp.StockInwardBasicInformationId 
		INNER JOIN 	
			product pp 
		ON
			psp.ProductId = pp.Id 
		INNER JOIN 
			productserialnumber s 
		ON
			psp.Id = s.StockInwardItemInformationId 
		INNER JOIN 
			manufacturer m
		ON
			m.Id = pp.ManufacturerId 
		INNER JOIN
			(SELECT ProductSerialNumberId FROM saleorderdispatchserialnumber s WHERE IsDeleted = 0) sdn 
		ON
			sdn.ProductSerialNumberId = s.Id 
		WHERE 
			inv.WarehouseId = _warehouseId
		AND
			s.IsScanned = 1
		AND
			sdn.ProductSerialNumberId IS NULL
		GROUP BY psp.Id
	)tbl GROUP BY tbl.ManufacturerId,tbl.ProductName)tbl GROUP BY tbl.Manufacturer ORDER BY tbl.ManufacturerId;
END