CREATE DEFINER=`inventorydev`@`%` PROCEDURE `GetByIdCategoryType`(
	IN _id int
)
BEGIN
	SELECT 
		`Name` As CategoryType
	FROM 
		Category
	WHERE 
		Id=_id;

END