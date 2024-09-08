namespace InventorySystem.SharedLayer.Models.Response
{
    public class BasicInformationResponse
    {
        public int Id { get; set; }
        public string SalesOrderNumber { get; set; }
        public DateTime DateofSale { get; set; }
        public int VendorId { get; set; }
        public int MovementTypeId { get; set; }
        public int WarehouseId { get; set; }
        public int OutTypeId { get; set; }
    }
}
