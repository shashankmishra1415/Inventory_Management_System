namespace InventorySystem.SharedLayer.Models.Request
{
	public class SerialNumberHistoryRequest
	{
		public int SerialNumberProfileId { get; set; }
		public int ActionTypeId { get; set; }
		public int LocationId { get; set; }
		public int StockInOutOrderNumber { get; set; }
		public int BuyerSellerId { get; set; }
		public int SalesOrderId { get; set; }
	}
}
