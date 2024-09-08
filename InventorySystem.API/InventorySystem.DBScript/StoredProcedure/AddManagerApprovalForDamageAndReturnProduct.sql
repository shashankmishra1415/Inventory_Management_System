CREATE DEFINER=`backup`@`%` PROCEDURE `AddManagerApprovalForDamageAndReturnProduct`(
	IN _recordTypeId int,
	IN _recordId int,
    IN _returnDamageType int,
    IN _vendorId int,
    IN _userId int
)
BEGIN
	INSERT INTO `inventory_db`.`returndamageproductapproval`
	(`RecordTypeId`,
	`RecordId`,
	`ReturnDamageType`,
	`VendorId`,
	`Status`,
	`CreatedBy`)
	VALUES
	(_recordTypeId,
	_recordId,
	_returnDamageType,
	_vendorId,
	0,
	_userId);
    
    SELECT 
		1 IsSuccess,
        "Send Successfully" Message;
END