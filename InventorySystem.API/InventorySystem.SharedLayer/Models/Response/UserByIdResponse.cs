namespace InventorySystem.SharedLayer.Models.Response
{
    public class UserByIdResponse
    {
        public string UserName { get; set; }
        public string Mobile { get; set; }
        public string Name { get; set; }
        public int LocationName { get; set; }
        public int Role { get; set; }
        public int Status { get; set; }
        public string Email { get; set; }
    }
}
