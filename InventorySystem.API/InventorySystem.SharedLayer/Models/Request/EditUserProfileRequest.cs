namespace InventorySystem.SharedLayer.Models.Request
{
    public class EditUserProfileRequest
    {
        public string Email { get; set; }
        public string UserName { get; set; }
        public string Mobile { get; set; }
        public string? Base64Image { get; set; }
        public string? DocumentName { get; set; }
	}
}
