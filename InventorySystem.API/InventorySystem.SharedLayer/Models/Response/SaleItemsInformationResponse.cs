namespace InventorySystem.SharedLayer.Models.Response
{
	public class SaleItemsInformationResponse
	{
		public int Id { get; set; }
		public string ItemSKU { get; set; }
		public string Manufacturer { get; set; }
		public string Category	 { get; set; }
		public string Name { get; set; }
		public int Quantity { get; set; }
		public int StockInHand { get; set; }
        public int NotScannedCount { get; set; }
		public int StatusId { get; set; }
    }
}
