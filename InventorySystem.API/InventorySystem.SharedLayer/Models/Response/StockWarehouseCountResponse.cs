using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventorySystem.SharedLayer.Models.Response
{
    public class StockWarehouseCountResponse
    {
        public int Id { get; set; }
        public string LocationName { get; set; }
        public int TotalProduct { get; set; }
    }
}
