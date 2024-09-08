CREATE DEFINER=`inventorydev`@`%` PROCEDURE `UpdateIsDamage`(
    IN _serialnumber varchar(16),
    IN _damageDate date,
    IN _userId int
)
BEGIN
    DECLARE id int;
    DECLARE serialId int;
    DECLARE serialNumberProfileId int;
    DECLARE dispatched int;
    DECLARE salesOrderId int;
    DECLARE locationId int;
    DECLARE customerId int;

    SELECT 
        Id
    INTO 
        serialNumberProfileId 
    FROM
        productserialnumber ps
    WHERE
        ps.SerialNumber = _serialNumber;

    SELECT 
        sob.VendorId
    INTO 
        customerId 
    FROM
        salesorderbasicinformation sob
    LEFT JOIN
        salesorderitemsinformation soi ON sob.Id = soi.SalesOrderBasicInformationId
    LEFT JOIN
        saleorderdispatchserialnumber sdn ON sdn.SalesOrderItemInformationId = soi.Id
    LEFT JOIN
        productserialnumber ps ON ps.Id = sdn.ProductSerialNumberId
    WHERE
        ps.Id = serialNumberProfileId
    ORDER BY 
        sdn.Id 
    DESC LIMIT 1;

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
    ORDER BY sod.Id 
    DESC LIMIT 1;

    IF(serialNumberProfileId IS NOT NULL AND dispatched=1)
    THEN
        IF EXISTS( 
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
            )
        THEN    
            SELECT 
                sodn.Id
            INTO
                id 
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
                sodn.IsDamage = 0;

            UPDATE saleorderdispatchserialnumber 
            SET 
                IsDamage = 1,
                DamageDate = _damageDate,
                ModifiedBy = _userId,
                IsDeleted = 1,
                ModifiedOn = NOW()
            WHERE
                Id = id;

            UPDATE productserialnumber 
            SET 
                IsDamage = 1,
                DamageDate = _damageDate,
                ModifiedOn = NOW(),
                ModifiedBy = _userId
            WHERE
                Id = serialNumberProfileId;

            SELECT 
                sob.Id,
                sob.WarehouseId
            INTO 
                salesOrderId,
                locationId 
            FROM
                saleorderdispatchserialnumber sodn
            INNER JOIN
                productserialnumber ps 
            ON 
                ps.Id = sodn.ProductSerialNumberId
            INNER JOIN
                salesorderitemsinformation soi 
            ON 
                sodn.SalesOrderItemInformationId = soi.Id
            INNER JOIN
                salesorderbasicinformation sob 
            ON 
                sob.Id = soi.SalesOrderBasicInformationId
            WHERE
                sodn.ProductSerialNumberId = serialNumberProfileId; 
                        
            CALL SaveSerialNumberHistory(serialNumberProfileId, 7, locationId, salesOrderId,customerId, _userId);

            SELECT 
                1 IsSuccess, 
                'Updated Successfully' Message;
        ELSE
            IF EXISTS( 
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
                    sodn.IsDeleted = 1
                AND
                    sodn.IsReturn = 1
                AND
                    sodn.IsDamage = 0
                ORDER BY sodn.Id DESC LIMIT 1       
                )
            THEN
                SELECT 
                    sodn Id
                INTO
                    id
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
                    sodn.IsDeleted = 1
                AND
                    sodn.IsReturn = 1
                AND
                    sodn.IsDamage = 0
                ORDER BY sodn.Id 
                DESC LIMIT 1;
        
                UPDATE saleorderdispatchserialnumber 
                SET 
                    IsDamage = 1,
                    DamageDate = _damageDate,
                    ModifiedBy = _userId,
                    ModifiedOn = NOW()
                WHERE
                    Id = id;
                    
                UPDATE productserialnumber 
                SET 
                    IsDamage = 1,
                    DamageDate = _damageDate,
                    ModifiedOn = NOW(),
                    ModifiedBy = _userId
                WHERE
                    Id = serialNumberProfileId;
                    
                SELECT 
                    sob.Id, 
                    sob.WarehouseId
                INTO
                    salesOrderId,
                    locationId 
                FROM
                    saleorderdispatchserialnumber sodn
                INNER JOIN
                    productserialnumber ps ON ps.Id = sodn.ProductSerialNumberId
                INNER JOIN
                    salesorderitemsinformation soi ON sodn.SalesOrderItemInformationId = soi.Id
                INNER JOIN
                    salesorderbasicinformation sob ON sob.Id = soi.SalesOrderBasicInformationId
                WHERE
                    sodn.ProductSerialNumberId = serialNumberProfileId
                ORDER BY sodn.Id
                DESC LIMIT 1;

                CALL SaveSerialNumberHistory(serialNumberProfileId, 7, locationId, salesOrderId,customerId, _userId);

                SELECT 
                    1 IsSuccess, 
                    'Updated Successfully' Message;
            ELSE
                SELECT 
                    0 IsSuccess, 
                    "Id doesn't exist" Message; 
            END IF;  
        END IF;       
    ELSEIF(serialNumberProfileId IS NOT NULL AND dispatched=0)
    THEN
        IF EXISTS
        ( 
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
            sodn.IsDispatched = 0
        AND
            sodn.IsDeleted = 0
        AND
            sodn.IsReturn = 0
        AND
            sodn.IsDamage = 0
        )
        THEN
            SELECT 
                sodn.Id
            INTO
                id
            FROM
                saleorderdispatchserialnumber sodn
            INNER JOIN 
                productserialnumber ps 
            ON
                sodn.ProductSerialNumberId = ps.Id
            WHERE 
                ps.SerialNumber = _serialnumber
            AND    
                sodn.IsDispatched = 0
            AND
                sodn.IsDeleted = 0
            AND
                sodn.IsReturn = 0
            AND
                sodn.IsDamage = 0;

            UPDATE saleorderdispatchserialnumber 
            SET 
                IsDamage = 1,
                DamageDate = _damageDate ,
                ModifiedBy = _userId,
                IsDeleted = 1,
                ModifiedOn = NOW()
            WHERE
                Id = id;
            
            UPDATE productserialnumber 
            SET 
                IsDamage = 1,
                DamageDate = _damageDate,
                ModifiedOn = NOW(),
                ModifiedBy = _userId
            WHERE
                Id = serialNumberProfileId;
                    
            SELECT 
                sob.Id,
                sob.WarehouseId
            INTO
                salesOrderId,
                locationId 
            FROM
                saleorderdispatchserialnumber sodn
            INNER JOIN
                productserialnumber ps ON ps.Id = sodn.ProductSerialNumberId
            INNER JOIN
                salesorderitemsinformation soi ON sodn.SalesOrderItemInformationId = soi.Id
            INNER JOIN
                salesorderbasicinformation sob ON sob.Id = soi.SalesOrderBasicInformationId
            WHERE
                sodn.ProductSerialNumberId = serialNumberProfileId; 
                        
            CALL SaveSerialNumberHistory(serialNumberProfileId, 5, locationId, salesOrderId,customerId, _userId);	
            SELECT 
                1 IsSuccess, 'Updated Successfully' Message;           
        ELSE
            SELECT 
                0 IsSuccess, "Id doesn't exist" Message; 
        END IF;    
    ELSE
        IF EXISTS
            (SELECT 
                ps.Id 
            FROM
                productserialnumber ps
            INNER JOIN
                stockinwarditeminformation sii
            ON
                sii.Id = ps.StockInwardItemInformationId
            INNER JOIN   
                stockinwardbasicinformation sib
            ON      
                sib.Id = sii.StockInwardBasicInformationId 
            WHERE
                ps.SerialNumber = _serialnumber
            AND 
                ps.IsDamage = 0
            )
        THEN
            SELECT 
                ps.Id 
            INTO 
                serialId
            FROM
                productserialnumber ps
            INNER JOIN
                stockinwarditeminformation sii
            ON
                sii.Id = ps.StockInwardItemInformationId
            INNER JOIN   
                stockinwardbasicinformation sib
            ON      
                sib.Id = sii.StockInwardBasicInformationId 
            WHERE
                ps.SerialNumber = _serialnumber
            AND 
                ps.IsDamage=0;

            UPDATE 
                productserialnumber 
            SET 
                IsDamage = 1,
                DamageDate = _damageDate,
                ModifiedBy = _userId,
                ModifiedOn = NOW()
            WHERE
                Id = serialId;

            SELECT 
                sib.Id,
                sib.WarehouseLocationId
            INTO
                salesOrderId,
                locationId 
            FROM
                productserialnumber ps
            INNER JOIN
                stockinwarditeminformation sii 
            ON 
                ps.StockInwardItemInformationId = sii.Id
            INNER JOIN
                stockinwardbasicinformation sib ON sib.Id = sii.StockInwardBasicInformationId
            WHERE
                ps.SerialNumber = _serialnumber;       

            CALL SaveSerialNumberHistory(serialNumberProfileId, 10, locationId, salesOrderId,customerId, _userId);
            SELECT 
                1 IsSuccess, 
                'Updated Successfully' Message;
        ELSE
            SELECT 
                0 IsSuccess, 
                "Id doesn't exist" Message; 
        END IF;
    END IF;  
END