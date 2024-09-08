CREATE DEFINER=`inventorydev`@`%` PROCEDURE `EditWarehouse`(
	IN _id int,
	IN _locationName varchar(64),
	IN _warehouseTypeId  int, 
	IN _maxCapacity decimal(10,2), 
	IN _address varchar(256), 
	IN _description varchar(512), 
	IN _isActive int,
	IN _userId int
)
BEGIN
	IF EXISTS(SELECT * FROM warehouse WHERE Id = _id) THEN
		UPDATE warehouse
		SET 
			LocationName = _locationName,
			WarehouseTypeId = _warehouseTypeId,
			MaxCapacity = _maxCapacity,
			Address = _address,
			Description = _description,
			IsActive = _isActive,
			ModifiedOn = NOW(),
			ModifiedBy = _userId
		WHERE
			Id = _id;
        
		SELECT 
			1 IsSuccess, 
            "Edited successfully!" Message;
	ELSE
		SELECT 
        0 IsSuccess, 
        "Id not found!" Message;
	END IF;
END