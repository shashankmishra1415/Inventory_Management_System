CREATE DEFINER=`inventorydev`@`%` PROCEDURE `ResetPassword`(
	IN _id int,
    IN _oldPassword varchar(64),
    IN _newPassword varchar(64)
)
BEGIN
	IF EXISTS(
			SELECT
				Id 
			FROM
				logindetail ld 
			WHERE 
				ld.userId = _id 
			AND 
				ld.`password` = MD5(_oldPassword)
			  ) 
	THEN
		UPDATE 	
			`user` u 
		JOIN 
			logindetail ld 
		ON 
			u.Id = ld.userId
		SET 
			ld.`Password` = MD5(_newPassword)
		WHERE 
			u.Id = _id 
		AND 
			ld.`password` = MD5(_oldPassword);
        
		SELECT 
			1 AS IsSuccess,
			'Password Reset Successfully' AS Message;
	ELSE
		SELECT
			0 AS IsSuccess,
			'Wrong Password' AS Message;
	END IF ;
END