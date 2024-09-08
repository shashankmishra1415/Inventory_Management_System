CREATE DEFINER=`inventorydev`@`%` PROCEDURE `EditProduct`(
	IN _id int , 
	IN _productSku varchar(64),
	IN _name varchar(500),
	IN _categoryId int,
	IN _manufacturerId int,
	IN _eanCode varchar(64), 
	IN _price double,
	IN _isActive int ,
	IN _userId int
)
BEGIN
	IF EXISTS(SELECT Id FROM product WHERE Id = _id) THEN
		UPDATE product
		  SET 
			ProductSKU = _productSku,
			Name = _name,
			CategoryId = _categoryId,
			ManufacturerId = _manufacturerId,
			EANCode = _eanCode,
			Price = _price,
			IsActive = _isActive,
			ModifiedOn = NOW(),
			ModifiedBy = _userId
		WHERE
			Id = _id ;
		SELECT 
			1 IsSuccess, 
            "Edited successfully!" Message;
	ELSE
		SELECT 
			0 IsSuccess, 
            "Id not found!" Message;
	END IF;
END