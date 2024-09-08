using InventorySystem.SharedLayer.Models.Request;
using InventorySystem.SharedLayer.Models.Response;

namespace InventorySystem.Application.Features.UploadFeature.Interfaces
{
	public interface IUploadDocumentFeature
	{
		public Task<Response> UploadDoument(UploadDocumentRequest document);
	}
}
