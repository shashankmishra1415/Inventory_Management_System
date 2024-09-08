CREATE DEFINER=`inventorydev`@`%` PROCEDURE `GetUserById`(
	IN _id int
)
BEGIN
	SELECT 
		up.`Name`,
		up.`Mobile`,
        up.`Email`,
		rp.`Id`As role,
		wp.`Id` As LocationName,
		lp.`UserName`,
        lp.`IsActive` as `Status`
	FROM 
		logindetail lp
    INNER JOIN
    	user up
    ON
		lp.userId= up.Id
    INNER JOIN
    	userrole rp
    ON
    	up.UserRoleId=rp.Id
    INNER JOIN
    	Warehouse wp
    ON
    	up.WarehouseId=wp.Id
    WHERE 
		up.Id =_id ;
END