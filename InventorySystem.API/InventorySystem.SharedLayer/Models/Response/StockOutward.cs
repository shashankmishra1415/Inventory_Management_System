
using InventorySystem.SharedLayer.Response;

namespace InventorySystem.SharedLayer.Models.Response
{
	public class StockOutwardList
	{
		public int Id { get; set; }
		public string Company { get; set; }	
		public string SalesOrderNo { get; set;}
		public string Location { get; set; }
		public string MovementType { get; set; }
		public string SalesDate { get; set; }
		public string OutType { get; set; }
	}

	public class StockOutward
	{
		public List<StockOutwardList> StockOutwardList { get; set; }	
		public PaginationResponse PaginationResponses { get; set; }

	}
}
