namespace InventorySystem.SharedLayer.Response
{
    public class PaginationResponse
    {
        public int TotalRecord { get; set; }
        public int PageSize { get; set; }
        public int PageNum { get; set; }
    }
}
