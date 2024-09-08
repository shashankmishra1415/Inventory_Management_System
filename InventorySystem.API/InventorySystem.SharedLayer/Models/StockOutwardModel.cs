using InventorySystem.SharedLayer.Models.Response;

namespace InventorySystem.SharedLayer.Models
{
    public class StockOutwardModel
    {
        public StockOutwardList StockOutwardList { get; set; }
        public List<StockOutwardItemList> StockOutwardItem { get; set; }
    }

    public class StockOutwardItemList
    {
        public int Id { get; set; }
        public bool IsDispatced { get; set; }
        public string SerialNo { get; set; }
        public string ProductSKU { get; set; }
        public string Name { get; set; }
        public bool IsReturnToManufacturer { get; set; }
    }
}
