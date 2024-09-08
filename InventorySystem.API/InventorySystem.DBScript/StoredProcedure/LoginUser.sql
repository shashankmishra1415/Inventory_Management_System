CREATE DEFINER=`inventorydev`@`%` PROCEDURE `LoginUser`(
	IN _email varchar(64), 
    IN _password varchar(64))
BEGIN
	DECLARE UserId int;
    DECLARE `Role` int;
    DECLARE Warehouse int;
    
    IF ( EXISTS (SELECT Id FROM logindetail WHERE UserName = _email))
    THEN
		IF (EXISTS (SELECT Id FROM logindetail WHERE UserName = _email AND `Password` = MD5(_password)) )
		THEN 	
			SELECT 
				up.Id,
				up.UserRoleId,
                up.WarehouseId
			INTO 
				UserId,
				`Role`,
				Warehouse
			FROM 
				logindetail AS lp
			INNER JOIN
				user up
			ON 
				lp.UserId = up.Id
			WHERE
				lp.UserName = _email 
			AND BINARY 
				lp.`Password` = MD5(_password); 

			SELECT
				1 IsSuccess,
				UserId Id,
				`Role` Role,
                Warehouse Warehouse;		
		ELSE	
			SELECT 
				0 IsSuccess,
				'Wrong Password' Message;
		END IF;
	ELSE 
		SELECT 
			0 IsSuccess,
			"User doesn't exists" Message;
	END IF;
END