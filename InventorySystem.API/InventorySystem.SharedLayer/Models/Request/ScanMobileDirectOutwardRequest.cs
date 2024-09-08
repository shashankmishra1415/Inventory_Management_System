namespace InventorySystem.SharedLayer.Models.Request
{
    public class ScanMobileDirectOutwardRequest
    {
        public string SerialNumber { get; set; }
        public int SalesOrderId { get; set; }
    }
}
