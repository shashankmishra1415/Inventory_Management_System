CREATE DEFINER=`inventorydev`@`%` PROCEDURE `DeleteVendor`(
IN _Id int,
IN _UserId int
)
BEGIN
	IF EXISTS(
				SELECT
					Id
				FROM
					vendor
				WHERE
					Id = _id
                    ) 
		THEN
		UPDATE
			vendor
		SET
			IsDeleted = 1,
			ModifiedOn=NOW(),
			ModifiedBy=_UserId
		WHERE
			Id = _Id;		 
		SELECT 
			1 IsSuccess,
            "Deleted Vendor successfully." Message;
	ELSE
		SELECT 
			0 IsSuccess,
			"Vendor not found." Message;
	END IF;
END