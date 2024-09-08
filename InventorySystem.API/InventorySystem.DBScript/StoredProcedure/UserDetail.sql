CREATE DEFINER=`backup`@`%` PROCEDURE `UserDetail`(IN _userId int)
BEGIN
	SELECT 
		u.Id, 
		u.Name, 
		w.LocationName,
        w.Id as LocationId,
		(SELECT COUNT(1) FROM stockinwardbasicinformation si WHERE si.WarehouseId = u.WarehouseId AND si.`Status` = 2  AND u.Id = _userId) StockInward,
		(SELECT COUNT(1) FROM salesorderbasicinformation sb WHERE sb.WarehouseId = u.WarehouseId AND sb.SaleOrderStatusId <> 1 AND u.Id = _userId ) StockOutWard,
		(SELECT COUNT(1) FROM stockauditinitiation sa WHERE sa.WarehouseId = u.WarehouseId AND sa.IsAuditDone = 0 AND u.Id = _userId) StockAudit  
	FROM 
		`user` u 
	INNER JOIN 
		warehouse w 
	ON 
		w.Id = u.WarehouseId
	WHERE 
		u.Id = _userId;
END