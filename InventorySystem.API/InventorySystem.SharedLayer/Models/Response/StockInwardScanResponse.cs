namespace InventorySystem.SharedLayer.Models.Response
{
    public class GetPendingSerialNumberForScanningResponse
    {
        public string Id { get; set; }
        public string ProductSKU { get; set; }
        public string Name { get; set; }
        public string SerialNo { get; set; }
        public int IsScanCompleted { get; set; }
    }
}
