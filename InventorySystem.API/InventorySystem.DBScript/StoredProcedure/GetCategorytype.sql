CREATE DEFINER=`inventorydev`@`%` PROCEDURE `GetCategoryType`()
BEGIN
	SELECT 
		Id `Key`, Name `Value` 
	FROM 
		Category where IsDeleted=0;
END