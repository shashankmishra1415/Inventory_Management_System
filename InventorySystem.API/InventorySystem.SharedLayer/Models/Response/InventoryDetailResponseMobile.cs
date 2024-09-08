using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventorySystem.SharedLayer.Models.Response
{
    public class InventoryDetailResponseMobile
    {
        public TotalStock stock { get; set; }
        public InventoryDetail inventoryDetail { get; set; }
    }


    public class InventoryDetail
    {
        public int BrandCount { get; set; }

        public int CategoryCount { get; set; }

        public int SupplierCount { get; set; }
    }

    public class TotalStock
    {
        public int TotalStockInHand { get; set; }
        public int TotalPrice { get; set; }
    }
}
