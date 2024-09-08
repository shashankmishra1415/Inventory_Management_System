CREATE DEFINER=`inventorydev`@`%` PROCEDURE `GetProductsOfDirectOutwardById`(
IN _id int
)
BEGIN
SELECT
	soi.Id,
    p.Name,
    p.ProductSKU,
    p.EANCode,
    c.Name as Category,
    m.Name as Manufacturer,
    soi.Quantity,
    sdn.IsDispatched,
    sdn.IsReturnToManufacturer
FROM
	salesorderitemsinformation soi
LEFT JOIN
	product p
ON
	p.Id=soi.ProductId
LEFT JOIN
	category c
ON
	p.CategoryId=c.Id
LEFT JOIN
	manufacturer m
ON
	p.ManufacturerId=m.Id
LEFT JOIN
	saleorderdispatchserialnumber sdn
ON
	sdn.SalesOrderItemInformationId=soi.Id
WHERE 
	soi.SalesOrderBasicInformationId=_id;
END