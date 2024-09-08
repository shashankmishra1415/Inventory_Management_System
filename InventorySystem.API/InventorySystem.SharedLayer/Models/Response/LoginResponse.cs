namespace InventorySystem.SharedLayer.Models.Response
{
    public class LoginResponse : BaseResponse
    {
        public int Role { get; set; }
        public int Warehouse { get; set; }
    }
}
