using System.ComponentModel.DataAnnotations.Schema;

namespace InventorySystem.SharedLayer.Models.Response
{
    public class ScanAllInfoSalesOrder
    {
        public ScanAllSalesOrderInfo? scanAllSalesOrderInfo { get; set; }
        public List<ScanAllSalesOrderProductInfo>? scanAllSalesOrderProductInfo { get; set; }
    }

    public class ScanAllSalesOrderProductInfo
    {
        public int? Id{set; get;}
        public string? ItemSKU{set; get;}
        public string? Manufacturer{set; get;}
        public string? Category{set; get;}
        public string? Name{set; get;}
        public string? SerialNumber { set; get; }
    }

    public class ScanAllSalesOrderInfo
    {
        public int? Id{set; get;}
        public string? SalesOrderNumber{set; get;}
        public string? DateofSale{set; get;}
        public string? CompanyName{set; get;}
        public string? MovementType{set; get;}
        public string? LocationName { set; get;}
        public int? LocationId { set; get;}
        public int? OutTypeId { set; get; }
    }
}
