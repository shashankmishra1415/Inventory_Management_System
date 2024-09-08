CREATE DEFINER=`inventorydev`@`%` PROCEDURE `UpdateUser`(
    IN _id int,
    IN _name varchar(64),
    IN _email varchar(64),
    IN _mobile varchar(64),
    IN _status int,
    IN _departmentId int,
    IN _warehouseId int,
    IN _password varchar(64),
    IN _userId int
    )
BEGIN
    DECLARE `_rollback` BOOL DEFAULT 0; 
    DECLARE CONTINUE HANDLER FOR SQLEXCEPTION SET `_rollback` = 1;
    START TRANSACTION;

    UPDATE 
		`user`
    SET 
        `Name` = _name,
        Email = _email,
        Mobile = _mobile,
        `Status` = _status,
        UserRoleId = _departmentId,
        WareHouseId = _warehouseId,
        ModifiedBy = _userId,
        ModifiedOn = NOW()
    WHERE 
        Id = _id;    
        
    UPDATE 
		logindetail
    SET
        UserName = _name,
        Password = _password,
        IsActive = _status,
        ModifiedBy = _userId,
        ModifiedOn = NOW()
    WHERE 
        UserId =_id;
        
    IF `_rollback` 
    THEN 
        SELECT 
			0 IsSuccess,
			'Not Updated Successfully' Message; 
        ROLLBACK; 
    ELSE 
        SELECT 
			1 IsSuccess,
			'Updated Successfully' Message; 
        COMMIT; 
    END IF;
END