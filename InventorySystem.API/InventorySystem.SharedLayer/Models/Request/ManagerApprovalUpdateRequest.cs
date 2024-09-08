namespace InventorySystem.SharedLayer.Models.Request
{
    public class ManagerApprovalUpdateRequest
    {
        public int RecordTypeId { get; set; }
        public int RecordId { get; set; }
        public int ReturnDamageType { get; set; }
        public int VendorId { get; set; }
        public int Status { get; set; }
        public string Remarks { get; set; }
    }
}
