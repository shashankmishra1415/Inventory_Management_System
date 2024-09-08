CREATE DEFINER=`inventorydev`@`%` PROCEDURE `GetByIdWarehouse`(IN _id int)
BEGIN
		SELECT 
			w.LocationName as LocationName,
			t.Id as `Type` , 
			w.Id,
			w.MaxCapacity as MaxCapacity, 
			a.Id  as IsActive,
			w.Address as Address,
			w.Description as Description
        FROM 
			warehouse w 
		LEFT JOIN 
			warehousetype t 
		ON 
			w.WarehouseTypeId = t.Id
		LEFT JOIN 
			activeprofile a
		ON 
			w.IsActive = a.Id 
		WHERE 
			w.Id = _id ; 
END