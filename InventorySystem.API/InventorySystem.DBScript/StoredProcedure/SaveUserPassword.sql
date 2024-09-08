CREATE DEFINER=`inventorydev`@`%` PROCEDURE `SaveUserPassword`(
	INOUT _name VARCHAR(64),
	INOUT _mobile VARCHAR(64),
	INOUT _id int
)
BEGIN
	DECLARE `password` varchar(15);
	DECLARE username varchar(15);
	
	SELECT 
		CONCAT(substring(_name,1,4),substring(_mobile,1,4))
		`Username` 
	INTO
	 	username;
	
	SELECT 
		CONCAT(substring(_name,1,4),"@",substring(_mobile,1,4))
		`Password` 
	INTO
	 	`password`;
	
	INSERT INTO 
		logindetail(
			UserName,
			`Password`,
			IsActive,
			CreatedBy,
			CreatedOn , 
			userId
		)
	VALUES(
			username, 
			MD5(`password`), 
			1, 
			1, 
			Now() , 
			_id);

	SET 
		_name = username;
	SET 
		_mobile = password;
END