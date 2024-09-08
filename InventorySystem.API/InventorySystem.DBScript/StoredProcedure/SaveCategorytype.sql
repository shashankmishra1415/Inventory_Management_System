CREATE DEFINER=`inventorydev`@`%` PROCEDURE `SaveCategorytype`(
	IN _categoryType varchar(32),
	IN _userId int
)
BEGIN 
	INSERT INTO 
	category(
		Name,
        CreatedBy,
        CreatedOn
	)
	VALUES(
		_categoryType,
		 _userId,
        NOW()
    );
	SELECT 
		1 IsSuccess,
		"Data Saved Successfully" Message ;
END