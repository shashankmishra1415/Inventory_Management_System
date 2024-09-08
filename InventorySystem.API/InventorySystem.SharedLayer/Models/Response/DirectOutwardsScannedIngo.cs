using InventorySystem.SharedLayer.Models.Response;

namespace InventorySystem.SharedLayer.Models.Response
{
    public class DirectOutwardsScannedIngo
    {

        public string? SerialNo{ get; set; }
        public string? Name{ get; set; }
        public string? ProductSKU{get; set; }
        public bool? IsDispatced{ get; set; }
        public string? EANCode { get; set; }
        public bool? IsReturnToManufacturer { get; set; }
    }

    public class GetSalesOrderInfo
    {
        public int? Id { get; set; }
        public string? Company { get; set; }
        public string? SalesOrderNo { get; set; }
        public string? Location { get; set; }
        public string? MovementType { get; set; }
        public string? SalesDate { get; set; }
    }

    public class DirectOutwardsScannedIngoList
    {
        public GetSalesOrderInfo sobInfo { get; set; }
        public List<DirectOutwardsScannedIngo> Listt { get; set; }
    }
}

