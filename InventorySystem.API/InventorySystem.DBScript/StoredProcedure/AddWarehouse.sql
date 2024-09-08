CREATE DEFINER=`inventorydev`@`%` PROCEDURE `AddWarehouse`(
	IN _locationName varchar(64),
	IN _warehouseTypeId int, 
	IN _maxCapacity decimal(10,2), 
	IN _address varchar(256), 
	IN _description varchar(512), 
	IN _isActive int,
	IN _userId int
)
BEGIN
	INSERT
    INTO 
		warehouse(
					LocationName,
                    WarehouseTypeId,
                    MaxCapacity,
                    Address,
                    Description,
                    IsActive,
                    CreatedOn,
                    CreatedBy)
	VALUES(
			_locationName ,
            _warehouseTypeId,
            _maxCapacity,
            _address,
            _description,
            _isActive, 
            NOW(), 
            _userId);
            
	SELECT
    1 IsSuccess,
    "Data Saved Successfully" Message;
END