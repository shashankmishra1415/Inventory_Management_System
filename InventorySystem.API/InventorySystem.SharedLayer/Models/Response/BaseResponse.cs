using InventorySystem.SharedLayer.Models.Request;

namespace InventorySystem.SharedLayer.Models.Response
{
    public class BaseResponse 
    {
        public int Id { get; set; }
        public int IsSuccess { get; set; }
        public string? Message { get; set; }
        public int ResponseCode { get; set; }
    }
}