CREATE DEFINER=`inventorydev`@`%` PROCEDURE `GetByIdManufacturerType`(
IN _id int
)
BEGIN
	SELECT 
		`Name` AS ManufacturerType
	FROM 
		Manufacturer
	WHERE 
		Id=_id ;
END