using InventorySystem.SharedLayer.Response;

namespace InventorySystem.SharedLayer.Models.Response
{
    public class ItemsResponse
    {
        public List<ItemsInfoResponse> Items { get; set; }
        public PaginationResponse PaginationResponses { get; set; }

        public class ItemsInfoResponse
        {
            public int Id { get; set; }
            public string ItemSKU { get; set; }
            public string Manufacturer { get; set; }
            public string Category { get; set; }
            public string Name { get; set; }
            public int Quantity { get; set; }
        }
    }
}
