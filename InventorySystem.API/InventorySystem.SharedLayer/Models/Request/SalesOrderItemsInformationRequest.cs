namespace InventorySystem.SharedLayer.Models.Request
{
	public class SalesOrderItemsInformationRequest
	{
		public int SaleOrderId { get; set; }
		public int ProductSKU { get; set; }
		public int ItemQuantity { get; set; }
	}
	 public class SalesOrderItemsInformationJsonRequest
	{
		public string Data { get; set; }
	}
}
