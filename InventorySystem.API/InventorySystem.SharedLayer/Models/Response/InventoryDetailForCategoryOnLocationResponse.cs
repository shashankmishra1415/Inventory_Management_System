namespace InventorySystem.SharedLayer.Models.Response
{
    public class InventoryDetailForCategoryOnLocationResponse
    {
        public string Name { get; set; }
        public int Quantity { get; set; }
        public Decimal TotalPrice { get; set; }
    }
}
