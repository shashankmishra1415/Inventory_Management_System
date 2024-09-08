namespace InventorySystem.SharedLayer.Models.Response
{
    public class ProductByIdStockInward
    {
        public int Id { get; set; }
        public string ProductSku { get; set; }
        public string Name { get; set; }
         public string CategoryName { get; set; }
         public string ManufactrerName { get; set; }
        public string EANCode { get; set; }
        public int Price { get; set; }
        public string? IsActive { get; set; }
    }
}
