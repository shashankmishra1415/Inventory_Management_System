using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventorySystem.SharedLayer.Models.Response
{
    public class InventoryDetailByBrandLocation
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public double TotalPrice { get; set; }
        public List<BrandDetails> BrandDetails { get; set; }
    }

    public class GetInventoryDetailByBrandLocationJSON
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public double TotalPrice { get; set; }
        public string JsonList { get; set; }
    }

    public class BrandDetails
    {
        public string Name { get; set; }
        public double TotalPrice { get; set; }
        public double Price { get; set; }
        public int Quantity { get; set; }
    }
}
