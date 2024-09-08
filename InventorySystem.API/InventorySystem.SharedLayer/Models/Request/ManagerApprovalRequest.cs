namespace InventorySystem.SharedLayer.Models.Request
{
    public class ManagerApprovalRequest
    {
        public int RecordTypeId { get; set; }
        public int RecordId { get; set; }
        public int ReturnDamageType { get; set; }
        public int VendorId { get; set; }
    }
}
