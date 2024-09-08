namespace InventorySystem.SharedLayer.Models.Response
{
    public class Invoice
    {
    public int Id { get; set; }
    public string InvoiceNumber { get;set;}
	public string PurchaseOrderNumber { get;set;}
	public DateTime DateOfPurchase{get;set;}
	public int VendorId { get;set;}
	public int MoveTypeId { get;set;}
	public int WarehouseId { get;set;}
	public int ItemTypeId { get; set; }
    }
}