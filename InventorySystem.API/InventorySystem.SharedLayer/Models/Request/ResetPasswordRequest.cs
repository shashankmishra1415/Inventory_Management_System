namespace InventorySystem.SharedLayer.Models.Request
{
    public class ResetPasswordRequest
    {
        public string oldPassword { get; set; }
        public string newPassword { get; set; }
    }
}
