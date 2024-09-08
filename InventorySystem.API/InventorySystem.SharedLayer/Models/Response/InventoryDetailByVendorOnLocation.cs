namespace InventorySystem.SharedLayer.Models.Response
{
    public class InventoryDetailByVendorOnLocation
    {
        public string Name { get; set; }
        public Decimal TotalPrice { get; set; }
        public int Quantity { get; set; }
    }
}
