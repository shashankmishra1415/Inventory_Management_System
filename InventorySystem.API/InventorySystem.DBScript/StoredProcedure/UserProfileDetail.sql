CREATE DEFINER=`inventorydev`@`%` PROCEDURE `UserProfileDetail`(IN _id int)
BEGIN
    SELECT 
        u.Id, 
        u.`Name` UserName, 
        u.Email Email  , 
        r.Name , 
        u.Mobile , 
        wp.LocationName, 
        wp.Id LocationId, 
        DATE_FORMAT(u.CreatedOn, '%d/%m/%Y') CreatedOn
    FROM 
        user u  
    INNER JOIN 
        logindetail l 
    ON 
        l.UserId = u.Id 
    INNER JOIN 
        userrole r 
    ON
        r.Id = u.UserRoleId
    INNER JOIN 
        warehouse wp 
    ON 
        wp.Id = u.WarehouseId
    WHERE 
        u.Id = _id  ;  
END