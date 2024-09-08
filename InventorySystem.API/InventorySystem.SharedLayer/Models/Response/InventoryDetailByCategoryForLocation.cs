using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventorySystem.SharedLayer.Models.Response
{
    public class InventoryDetailByCategoryForLocation
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public double TotalPrice { get; set; }
        public List<Productdetails> Productdetails { get; set; }
    }
    
    public class GetInventoryDetailByCategoryForLocationJson
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public double TotalPrice { get; set; }
        public string JsonList { get; set; }
    }

    public class Productdetails
    {
        public string Name { get; set; }
        public int Quantity { get; set; }
        public double Price { get; set; }
        public double TotalPrice { get; set; }
    }
}
