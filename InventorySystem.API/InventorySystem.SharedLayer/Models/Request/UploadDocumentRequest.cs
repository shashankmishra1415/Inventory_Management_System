namespace InventorySystem.SharedLayer.Models.Request
{
	public class UploadDocumentRequest
	{
		public string? DocumentName { get; set; }
		public string? Base64 { get; set; }
	}
}
