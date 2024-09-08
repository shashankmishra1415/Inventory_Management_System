CREATE DEFINER=`inventorydev`@`%` PROCEDURE `SaveUser`(
	IN _name varchar(64),
	IN _email varchar(64),
	IN _mobile varchar(64),
	IN _status int,
	IN _departmentId INT,
	IN _warehouseId INT
)
BEGIN
	DECLARE id int;
	INSERT INTO 
		`user`(
			`Name`,
			Mobile,
			Email,
			`Status`,
			WareHouseId,
			UserRoleId ,
			IsDeleted,
			CreatedOn,
			CreatedBy)
	VALUES(
		_name,
		_mobile,
		_email,
		_status,
		_warehouseId,
		_departmentId,
		0,
		NOW(),
		1);

	SELECT 
		LAST_INSERT_ID() 
	INTO 
		id;
	Call 
		SaveUserPassword(_name, _mobile, id);

	SELECT 
		id Id,
		_name UserName,
		_mobile `Password`;
   
END