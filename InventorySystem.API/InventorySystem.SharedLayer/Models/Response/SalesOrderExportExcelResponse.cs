using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventorySystem.SharedLayer.Models.Response
{
    public class SalesOrderExportExcelResponse
    {
        //public int Id { get; set; }
        //public int ProductSkuId { get; set; }
        public string ProductSKU { get; set; }
        public string SerialNumber { get; set; }
        public string DispatchDate { get; set; }
    }
}
