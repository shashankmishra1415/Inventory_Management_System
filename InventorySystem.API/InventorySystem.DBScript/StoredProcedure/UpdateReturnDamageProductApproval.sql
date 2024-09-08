CREATE DEFINER=`backup`@`%` PROCEDURE `UpdateReturnDamageProductApproval`(
	IN _id int,
	IN _recordTypeId int,
    IN _recordId int,
    IN _returnDamageType int,
    IN _vendorId int,
    IN _status tinyint,
    IN _remarks varchar(500),
    IN _userId int
)
BEGIN
	UPDATE `inventory_db`.`returndamageproductapproval`
	SET
	`RecordTypeId` = _recordTypeId,
	`RecordId` = _recordId,
	`ReturnDamageType` = _returnDamageType,
	`VendorId` = _vendorId,
	`Status` = _status,
	`Remarks` = _remarks,
	`ModifiedBy` = _userId,
	`ModifiedOn` = NOW()
	WHERE `Id` = _id;
    
    SELECT 
		1 IsSuccess,
        "Data Updated Successfully" Message;
END