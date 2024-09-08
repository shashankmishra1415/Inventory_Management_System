namespace InventorySystem.SharedLayer.Models.Request
{
    public class SaveUserRequest
    {
        public string? Name { get; set; }
        public string? Email { get; set; }
        public string? Mobile { get; set; }
        public int? Status { get; set; } = 1;
        public int ? DepartmentId { get; set; }
        public int? WareHouseId { get; set; }         
    }
}
