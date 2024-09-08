namespace InventorySystem.SharedLayer.Models.Response
{
    public class GoodsByLocationResponse
    {
        public GoodsSummaryByLocation summary { get; set; }
        public List<GoodsDetailsByLocation> details { get; set; }

    }

    public class GoodsSummaryByLocation
    {
        public Decimal Quantity { get; set; }
        public Decimal Value { get; set; }
    }

    public class GoodsDetailsByLocation
    {
        public int ProductSkuId { get; set; }
        public string? ProductSkuName { get; set; }
        public Decimal StockQuantity { get; set; } = 0;
        public Decimal StockValue { get; set; } = 0;
        public Decimal? MTDValue { get; set; }
    }
}
