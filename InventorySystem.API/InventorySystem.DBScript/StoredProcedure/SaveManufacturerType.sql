CREATE DEFINER=`inventorydev`@`%` PROCEDURE `SaveManufacturerType`(
	IN _manufacturerType varchar(32),
	IN _userId int
)
BEGIN
	INSERT
	INTO 
		manufacturer(
					Name,
					CreatedBy,
					CreatedOn
					)
	VALUES(
			_manufacturerType,
			_userId,
            NOW()
		);
	SELECT 
		1 IsSuccess,
		"Data Saved Successfully" Message ;
END