CREATE DEFINER=`inventorydev`@`%` PROCEDURE `UpdateSerialNumberSalesOrder`(
    IN _id int,
    IN _serialNumber varchar(16),
    IN _userId int
)
BEGIN
    DECLARE flag boolean;
    DECLARE serialNumberId int;
    DECLARE locationId int;
    DECLARE dispatchId int;
    DECLARE saleOrderId int;
    DECLARE customerId int;
    
    SET flag = (
        SELECT EXISTS(
            SELECT 
                ps.Id
            FROM 
                saleorderdispatchserialnumber sod
            INNER JOIN 
                salesorderitemsinformation soi ON sod.SalesOrderItemInformationId = soi.Id
            INNER JOIN 
                salesorderbasicinformation sob ON soi.SalesOrderBasicInformationId = sob.Id
            INNER JOIN 
                productserialnumber ps ON sod.ProductSerialNumberId = ps.Id
            WHERE 
                sob.Id = _id AND sod.IsDispatched = 0 AND ps.SerialNumber = _serialNumber
        )
    );
    
    IF flag = 1 
    THEN 
     SET serialNumberId = ( 
        SELECT 
			Id
		FROM 
			productserialnumber
		WHERE
			SerialNumber = _serialNumber
        );

		SELECT 
            sob.Id SaleOrderBasicId, 
            sob.VendorId, 
            sob.WarehouseId, 
            sod.Id SaleOrderDispatchId
        INTO
            saleOrderId, 
            customerId, 
            locationId, 
            dispatchId
        FROM 
			salesorderbasicinformation sob
		INNER JOIN 
			salesorderitemsinformation soi
		ON	
			sob.Id = soi.SalesOrderBasicInformationId
		INNER JOIN
			saleorderdispatchserialnumber sod
		ON 
			sod.SalesOrderItemInformationId = soi.Id
		INNER JOIN 
			productserialnumber ps
		ON 
			ps.Id = sod.ProductSerialNumberId
		WHERE
			ps.Id = serialNumberId
		ORDER BY
			sod.Id 
        DESC LIMIT 1;
            
        UPDATE saleorderdispatchserialnumber sod
        SET 
            IsDispatched = 1, 
            DispatchDate = NOW(), 
            ModifiedBy =_userId, 
            ModifiedOn = NOW()
        WHERE 
            Id = dispatchId;
       
        CALL SaveSerialNumberHistory(
            serialNumberId, 
            3, 
            locationId, 
            saleOrderId, 
            customerId, 
            _userId
        );
    END IF;
END