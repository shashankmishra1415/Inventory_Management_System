CREATE DEFINER=`inventorydev`@`%` PROCEDURE `GetManufacturerType`()
BEGIN
	SELECT 
		Id `Key`, 
		`Name` `Value` 
	FROM 
		manufacturer 
	WHERE
		Isdeleted=0;

END