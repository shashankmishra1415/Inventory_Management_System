CREATE DEFINER=`inventorydev`@`%` PROCEDURE `ProductById`(
	IN _id int
)
BEGIN
	SELECT 
		p.Id, 
        p.ProductSKU,
		p.Name, 
		c.Id AS CategoryId,
		m.Id AS ManufacturerId, 
		p.Name, 
		p.EANCode, 
		p.Price ,
		ap.Status AS IsActive
	FROM 
		product p 
		LEFT JOIN 
		category c
		ON
		p.CategoryId = c.Id
		LEFT JOIN 
		manufacturer m
		ON
		p.ManufacturerId = m.Id
		INNER JOIN 
		activeprofile ap
		ON 
		p.IsActive = ap.Id 
	WHERE 
		p.Id = _id 
		AND 
		p.IsDeleted = 0; 
END