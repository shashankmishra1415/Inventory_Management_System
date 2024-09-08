CREATE DEFINER=`inventorydev`@`%` PROCEDURE `UpdateIsReturnSaleOrderDispatchSerialNumber`(
  IN _serialnumber varchar(16), 
  IN _userId int
)
BEGIN 
    DECLARE id int;
    DECLARE serialnumberprofileId int;
    DECLARE dispatched int;
    DECLARE actionTypeId int;
    DECLARE locationId int;
    DECLARE salesorderId int;
    DECLARE customerId int;

    SELECT 
        Id
    INTO 
        serialnumberprofileId 
    FROM 
        productserialnumber 
    WHERE 
        SerialNumber = _serialNumber;

    SELECT 
        sod.IsDispatched 
    INTO 
        dispatched 
    FROM 
        saleorderdispatchserialnumber sod 
    INNER JOIN 
        productserialnumber ps 
    ON
		ps.Id = sod.ProductSerialNumberId 
    WHERE 
        ps.SerialNumber = _serialnumber 
    AND 
        sod.IsDeleted = 0;

    SELECT
        Id 
    INTO 
        actionTypeId 
    FROM 
        actiontype 
    WHERE 
        Id = 4;

    IF( serialnumberprofileId IS NOT NULL AND dispatched = 1) 
    THEN 
        IF EXISTS (
                SELECT 
                    sodn.Id 
                FROM 
                    saleorderdispatchserialnumber sodn 
                INNER JOIN 
                    productserialnumber ps 
                ON 
                    sodn.ProductSerialNumberId = ps.Id 
                WHERE 
                    ps.SerialNumber = _serialnumber 
                AND 
                    sodn.IsDispatched = 1 
                AND 
                    sodn.IsDeleted = 0 
                AND 
                    sodn.IsReturn = 0 
                AND 
                    sodn.IsDamage = 0 
                LIMIT 1)
        THEN 
			SELECT 
				sodn.Id INTO id 
			FROM 
				saleorderdispatchserialnumber sodn 
			INNER JOIN 
				productserialnumber ps 
            ON
                sodn.ProductSerialNumberId = ps.Id 
			WHERE 
				ps.SerialNumber = _serialnumber 
			AND 
				sodn.IsDispatched = 1 
			AND 
				sodn.IsDeleted = 0 
			AND 
				sodn.IsReturn = 0 
			AND 
				sodn.IsDamage = 0 
			LIMIT 1;

			SELECT 
				sob.Id, 
				sob.WarehouseId, 
				sob.CustomerId INTO salesorderId, 
				locationId, 
				customerId 
			FROM 
				saleorderdispatchserialnumber sodn 
			INNER JOIN 
				productserialnumber ps ON ps.Id = sodn.ProductSerialNumberId 
			INNER JOIN
				salesorderitemsinformation soi ON sodn.SaleOrderItemInformationId = soi.Id 
			INNER JOIN 
				salesorderbasicinformation sob ON sob.Id = soi.SalesOrderBasicInformationId 
			WHERE 
				sodn.ProductSerialNumberId = serialnumberprofileId 
			AND 
				sodn.IsDispatched = 1 
			AND 
				sodn.IsDeleted = 0 
			AND 
				sodn.IsReturn = 0 
			AND 
				sodn.IsDamage = 0 
			LIMIT 1;

			UPDATE 
				saleorderdispatchserialnumber 
			SET 
				IsReturn = 1, 
				IsDeleted = 1, 
				ModifiedBy = _userId, 
				ModifiedOn = NOW() 
			WHERE 
				Id = id;
				
			CALL SaveSerialNumberHistory( serialnumberprofileId, actionTypeId, locationId, salesorderId, customerId,  _userId);

			SELECT 
				1 IsSuccess, 
				"Updated Successfully" Message;
        ELSE 
            SELECT 
				0 IsSuccess, 
				"Id doesn't exist" Message;
        END IF;
    ELSE 
        SELECT 
			0 IsSuccess, 
			"Id doesn't exist" Message;
    END IF;
END