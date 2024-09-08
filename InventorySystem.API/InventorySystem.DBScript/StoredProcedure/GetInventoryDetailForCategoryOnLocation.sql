CREATE DEFINER=`inventorydev`@`%` PROCEDURE `GetInventoryDetailForCategoryOnLocation`(
	in _warehouseId int,
	in _categoryId int
)
begin
	select 
		m.Name,
		count(psn.Id) Quantity,
		sum(p.Price) TotalPrice
	from 
		 stockinwardbasicinformation sibi 
	inner join
		stockinwarditeminformation siii
	on
		sibi.Id = siii.StockInwardBasicInformationId
	inner join 	
		product p 
	on
		siii.ProductId = p.Id 
	inner join 
		productserialnumber psn 
	on
		siii.Id = psn.StockInwardItemInformationId 
	inner join 
		manufacturer m 
	on
		p.ManufacturerId = m.Id
	where 
		sibi.WarehouseId = _warehouseId
	and
		p.CategoryId = _categoryId
	and
		psn.IsScanned = 1
	and
		CheckForSalesOrderedOrNot(psn.Id)
	group by
		m.Id;
END