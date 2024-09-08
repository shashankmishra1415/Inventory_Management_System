using System.Runtime.CompilerServices;

namespace InventorySystem.SharedLayer.Models.Response
{
	public class GlobalSearchWebResponse
    {
        public List<GlobalSearchWebStatus> globalSearchWebStatus { get; set; }
        public GlobalSearchWebDetail globalSearchWebDetail { get; set; }

    }
    public class GlobalSearchWebDetail
    {
        public int Id { get; set; }
        public string SerialNumber { get; set; }
        public string Name { get; set; }
        public string Manufacturer { get; set; }
        public double Price { get; set; }
        public string ProductSku { get; set; }
        public string Category { get; set; }
        public int VendorId { get; set; }
        public int InvoiceId { get; set; }
    }

    public class GlobalSearchWebStatus 
    {
        public string Transaction { get; set; }
        public DateTime ActionDate { get; set; }
        public string User { get; set; }
        public string Location { get; set; }
        public string PurchasedAndSoldNumber { get; set; }
        public string Dealer { get; set; }
    }
}
