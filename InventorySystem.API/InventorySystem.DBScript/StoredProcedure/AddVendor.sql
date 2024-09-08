CREATE DEFINER=`backup`@`%` PROCEDURE `AddVendor`(
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
	INSERT INTO 
		vendor
		(
			CompanyName, 
			ContactName, 
			ContactMobile, 
			ContactEmail, 
			CompanyTypeId, 
			VendorTypeId, 
			Address, 
			GST, 
			IsActive, 
			CreatedOn, 
			CreatedBy
		)
	VALUES
		(
			_companyName,
			_contactName,
			_contactMobile,
			_contactEmail,
			_companyTypeId,
			_vendorTypeId,
			_address,
			_gst,
			_isActive,
			NOW(),
			_userId
		);
        
        SELECT 
			1 IsSuccess,
            "Data Saved Successfully" Message;
END