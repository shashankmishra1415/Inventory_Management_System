namespace InventorySystem.SharedLayer.Models.Response
{
    public class UserDetailMobilResponse
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string LocationName { get; set; }
        public int LocationId { get; set; }
        public int StockInward { get; set; }
        public int StockOutWard { get; set; }
        public int StockAudit { get; set; }
    }
}
