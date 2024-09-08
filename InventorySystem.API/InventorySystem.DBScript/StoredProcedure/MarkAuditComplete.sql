CREATE DEFINER=`inventorydev`@`%` PROCEDURE `GetAuditItemCountAsPerWarehouseId`(
	IN _Id int
)
BEGIN
	SELECT 
		tbl.AuditId,
		tbl.CategoryId,
		c.Name,
		tbl.TotalCount,
		tbl.TotalAuditDoneCount
	FROM (
		SELECT 
			s.Id AuditId,
			sai.CategoryId,
			count(sai.CategoryId)TotalCount,
			SUM(sai.IsAuditDone)TotalAuditDoneCount
		FROM 
			stockaudititem sai
		INNER JOIN
			stockauditinitiation s
		ON
			sai.StockAuditInitiationId = s.Id
		WHERE 
			s.WarehouseId = _Id
		AND
			s.IsAuditDone = 0
		GROUP BY s.Id,sai.CategoryId 
		ORDER BY 
			s.Id,sai.CategoryId
	)tbl
	INNER JOIN 
		category c 
	ON
		c.Id = tbl.CategoryId;
END