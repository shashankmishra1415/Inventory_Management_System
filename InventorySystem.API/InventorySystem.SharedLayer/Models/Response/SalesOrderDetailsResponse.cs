using InventorySystem.SharedLayer.Response;

namespace InventorySystem.SharedLayer.Models.Response
{
    public class SalesOrderDetailsResponse
    {
        public List<SalesBasicDetails> SalesDetail { get; set; }
        public PaginationResponse PaginationResponses { get; set; }
    }
    public class SalesBasicDetails
    {
        public int Id { get; set; }
        public string OrderNo { get; set; }
        public string Customer { get; set; }
        public string MovementType { get; set; }
        public string SourceLocation { get; set; }
        public DateTime DateTime { get; set; }
        public string Status { get; set; }
        public string OutType { get; set; }
    }
}

