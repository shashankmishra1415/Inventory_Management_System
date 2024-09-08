using InventorySystem.SharedLayer.Response;

namespace InventorySystem.SharedLayer.Models.Response
{
    public class UserListResponse
    {
        public List<UserDetail> UserDetail { get; set; }
        public PaginationResponse PaginationResponses { get; set; }
    }
    public class UserDetail
    {
        public int? Id { get; set; }
        public string Name { get; set; }
        //public string Department { get; set; }
        public string UserName { get; set; }
        public string role { get; set; }
      //  public string WareHouse { get; set; }        
        public string LocationName { get; set; }        
        public string Mobile { get; set; }
        public string Status { get; set; }
    }
}