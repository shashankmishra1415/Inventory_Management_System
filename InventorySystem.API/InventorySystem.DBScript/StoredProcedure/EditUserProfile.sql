CREATE DEFINER=`inventorydev`@`%` PROCEDURE `EditUserProfile`(
	IN _id int, 
    IN _email varchar(64), 
    IN _userName varchar(64), 
    IN _mobile varchar(64) 
)
BEGIN
	UPDATE 
		user u 
	JOIN 
		logindetail lp 
	ON 
		u.Id = lp.UserId
	SET  
		u.Email = _email, 
		u.Name = _userName,
        u.mobile = _mobile
	WHERE
		u.Id = _id; 
	
    Select 
		1 IsSuccess, 
        'Profile Update Succesfully' Message;
END