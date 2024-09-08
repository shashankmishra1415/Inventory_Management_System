namespace InventorySystem.SharedLayer.Models.Response
{
	public class UserProfileResponse
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string Role { get; set; }
        public string Mobile { get; set; }
        public string ProfileImageUrl { get; set; }
        public string LocationName { get; set; }
        public string CreatedOn { get; set; }
        public int LocationId { get; set;}
    }
}
