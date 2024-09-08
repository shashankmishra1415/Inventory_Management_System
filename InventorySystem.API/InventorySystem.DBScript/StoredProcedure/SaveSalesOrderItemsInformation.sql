CREATE DEFINER=`inventorydev`@`%` PROCEDURE `SaveSalesOrderItemsInformation`(
	IN _productId int,
    IN _salesOrderBasicInformationId int,
	IN _quantity int,
	IN _userId int
)
BEGIN
	INSERT INTO 
		salesorderitemsinformation
			(
            ProductId, 
			SalesOrderBasicInformationId, 
			Quantity, 
			CreatedBy, 
			CreatedOn
            )
	VALUES
			(
            _productId,
            _salesOrderBasicInformationId,
			_quantity,
			_userId,
			NOW()
            );
            
END