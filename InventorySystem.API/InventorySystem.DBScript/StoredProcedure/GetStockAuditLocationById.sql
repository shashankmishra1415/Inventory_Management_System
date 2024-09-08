CREATE DEFINER=`inventorydev`@`%` PROCEDURE `GetStockAuditLocationById`(
	IN _id int
)
BEGIN
	SELECT
			s.Id,
			w.LocationName WarehouseName,
			u.Name UserName,
			s.AuditInitiatedOn,
			CASE
				WHEN
				s.IsAuditDone = 1 
				AND 
				s.IsAuditPassed = 1 
				THEN 'Pass'
				WHEN 
				s.IsAuditDone = 1
				AND
				s.IsAuditPassed = 0 
				THEN 'Fail'
				WHEN 
				s.IsAuditDone = 0 
				AND
				s.IsAuditPassed = 0
				THEN
				'In Progress'
			END AuditStatus
		FROM 
			stockauditinitiation s 
		INNER JOIN
			warehouse w 
		ON
			s.WarehouseId = w.Id 
		INNER JOIN 
			user u 
		ON
			s.CreatedBy = u.Id 
		WHERE 
			1=1 AND s.Id=_id;
END