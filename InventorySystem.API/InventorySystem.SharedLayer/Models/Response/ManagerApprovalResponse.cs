using InventorySystem.SharedLayer.Response;

namespace InventorySystem.SharedLayer.Models.Response
{
    public class ManagerApprovalResponse
    {
        public List<ManagerApproval> ManagerApprovalList { get; set; }
        public PaginationResponse PaginationResponses { get; set; }
    }

    public class ManagerApproval
    {
        public int Id { get; set; }
        public string RecordType { get; set; }
        public string Vendor { get; set; }
        public int ReturnDamageType { get; set; }
        public int RecordId { get; set; }
        public string NumberOfRecord { get; set; }
    }

}
