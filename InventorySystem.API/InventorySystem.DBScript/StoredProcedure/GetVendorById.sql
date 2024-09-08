CREATE DEFINER=`inventorydev`@`%` PROCEDURE `GetVendorById`(
	IN _id int
)
BEGIN
	SELECT
		vp.Id,
		vp.CompanyName,
		vp.ContactName,
		vp.ContactMobile,
		vp.ContactEmail,
		vp.Address,
		vp.GST,
		vp.CompanyTypeId,
		vp.VendorTypeId,
		vp.IsActive
	FROM
		vendor vp
	WHERE
		vp.IsDeleted = 0 AND vp.Id = _id;
END