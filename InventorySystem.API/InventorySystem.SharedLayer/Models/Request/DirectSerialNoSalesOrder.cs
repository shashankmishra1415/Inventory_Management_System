namespace InventorySystem.SharedLayer.Models.Request
{
    public class DirectSerialNoSalesOrder
    {
        public string SerialNumber{ get; set; }
        public int WarehouseId{ get; set; }
        public int SalesOrderBasicInformationId{ get; set; }
    }
}
