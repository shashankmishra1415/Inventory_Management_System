CREATE DEFINER=`inventorydev`@`%` PROCEDURE `SaleOrderMarkComplete`(
IN _id int,
IN _userId int
)
BEGIN
UPDATE 
      salesorderbasicinformation sob
SET 
   sob.SaleOrderStatusId=1, 
   sob.ModifiedBy=_userId, 
   sob.ModifiedOn=NOW()
WHERE sob.Id=_id;
END