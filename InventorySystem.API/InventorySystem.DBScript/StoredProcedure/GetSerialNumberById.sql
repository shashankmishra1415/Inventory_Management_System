CREATE DEFINER=`backup`@`%` PROCEDURE `GetSerialNumberById`(
 IN _id int
 )
BEGIN
	SELECT 
		SerialNumber
	FROM
		productserialnumber
	WHERE
		id=_id;
END