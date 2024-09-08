namespace InventorySystem.SharedLayer.Models.Request
{
	public class SalesOrderBasicInformationRequest
	{
		public string SalesOrderNo { get; set; }
		public DateTime DateofSale { get; set; }
		public int CustomerId { get; set; }
		public int MovementTypeId { get; set; }
		public int WarehouseId { get; set; }
		public int OutType { get; set; }
	}
}
