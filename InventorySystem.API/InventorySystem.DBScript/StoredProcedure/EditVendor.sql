CREATE DEFINER=`inventorydev`@`%` PROCEDURE `EditVendor`(
	IN _id int,
	IN _companyName varchar(64),
	IN _companyTypeId int,
	IN _vendorTypeId int,
	IN _gst varchar(16),
	IN _isActive int,
	IN _address varchar(256),
	IN _contactName varchar(64),
	IN _contactMobile varchar(16),
	IN _contactEmail varchar(64),
	IN _userId int
)
BEGIN
	IF EXISTS(SELECT Id FROM vendor WHERE Id = _id) THEN
		UPDATE 
			vendor 
		SET 
			CompanyName = _companyName, 
			ContactName = _contactName, 
			ContactMobile = _contactMobile, 
			ContactEmail = _contactEmail, 
			CompanyTypeId = _companyTypeId, 
			VendorTypeId = _vendorTypeId, 
			Address = _address, 
			GST = _gst, 
			IsActive = _isActive, 
			ModifiedOn = NOW(), 
			ModifiedBy = _userId
		WHERE
			Id = _id;
		SELECT 
			1 IsSuccess, 
			"Vendor Edited Successfully!" Message;
	ELSE
		SELECT 0 IsSuccess, 
        "Vendor not found." Message;
	END IF;
END