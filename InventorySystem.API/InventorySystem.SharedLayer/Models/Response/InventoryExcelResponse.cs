namespace InventorySystem.SharedLayer.Models.Response
{
    public class InventoryExcelResponse
    {
        public AllProductCount totalProduct { get; set; }
        public List<ExcelResponse> ProductInformation { get; set; }
    }

    public class ExcelResponse
    {
        public string ProductSKU { get; set; }
        public string Name { get; set; }
        public int ReceivedInventory { get; set; }
        public int DispatchedInventory { get; set; }
        public int PendingInventory { get; set; }
        public int DamageInventory { get; set; }
        public int StockQuantity { get; set; }
        public decimal TotalPrice { get; set; }
    }

    public class AllProductCount {

        public int ReceivedInventory { get; set; }
        public int DispatchedInventory { get; set; }
        public int PendingInventory { get; set; }
        public int DamageInventory { get; set; }
        public int StockQuantity { get; set; }
        public decimal TotalPrice { get; set; }
    }

}
