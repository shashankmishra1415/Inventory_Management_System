CREATE DEFINER=`inventorydev`@`%` PROCEDURE `UpdateCategoryType`(
    IN _id int, 
    IN _categorytype varchar(32),
    IN _userId int
    )
BEGIN
    UPDATE 
        category
    SET 
        `Name`=_categorytype,
        ModifiedBy=_userId,
        ModifiedOn=NOW()
    WHERE 
        Id=_id;
    
    SELECT
        1 IsSuccess, 
        "Data Updated Successfully" Message ;
END