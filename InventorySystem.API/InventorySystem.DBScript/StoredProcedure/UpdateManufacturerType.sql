CREATE DEFINER=`inventorydev`@`%` PROCEDURE `UpdateManufacturerType`(
	IN _id int, 
	IN _manufacturertype varchar(32),
	IN _userId int
)
BEGIN
	UPDATE 
		manufacturer
	SET 
		`Name` = _manufacturertype,
		ModifiedBy = _userId,
		ModifiedOn = now()
	WHERE 
		Id=_id;
        
	SELECT 
		1 IsSuccess, 
        "Data Updated Successfully" Message ;
END