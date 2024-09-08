namespace InventorySystem.SharedLayer.Models.Response
{
    public class InventoryDetailByBrandResponse
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public double TotalPrice { get; set; }
        public List<GetInventoryDetailJsonList> JsonList { get; set; }
    }

    public class GetInventoryDetailByBrandResponseJSON
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public double TotalPrice { get; set; }
        public string? JsonList { get; set; }
    }


    public class GetInventoryDetailJsonList
    {
        public string Name { get; set; }
        public double TotalPrice { get; set; }
        public double Price { get; set; }
        public int Quantity { get; set; }       
    }
}
