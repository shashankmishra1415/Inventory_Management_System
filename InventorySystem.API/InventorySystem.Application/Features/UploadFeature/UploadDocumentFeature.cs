using Amazon.S3;
using Amazon;
using Amazon.S3.Model;
using InventorySystem.Application.Features.UploadFeature.Interfaces;
using InventorySystem.SharedLayer.Models.Request;
using InventorySystem.SharedLayer.Models.Response;
using Microsoft.Extensions.Options;
using InventorySystem.Application.Helpers;
using Microsoft.IdentityModel.Tokens;
using Microsoft.Extensions.Configuration;

namespace InventorySystem.Application.Features.UploadFeature
{
	public class UploadDocumentFeature : IUploadDocumentFeature
	{
		readonly AmazonS3 amazonS3;
		readonly IConfiguration configuration;
		public UploadDocumentFeature(IOptions<AmazonS3> amazonS3, IConfiguration configuration)
		{
			this.amazonS3 = amazonS3.Value;
			this.configuration = configuration;
		}
		public async Task<Response> UploadDoument(UploadDocumentRequest document)
		{
			string pathURL = "";
			string docName = !String.IsNullOrEmpty(document.DocumentName) ? (UnixTimeStampHelper.DateTimeToUnixTimestamp(DateTime.Now) + document.DocumentName.Trim()).Replace(" ", "_") : "";
			try
			{
				IAmazonS3 client;
				byte[] bytes = Convert.FromBase64String(document.Base64);
				string lowercaseDocumentName = !String.IsNullOrEmpty(document.DocumentName) ? document.DocumentName.ToLower() : "";			

				using (client = new AmazonS3Client(amazonS3.AccessKey, amazonS3.SecurityKey, RegionEndpoint.APSouth1))
				{
					var request = new PutObjectRequest
					{
						BucketName = amazonS3.BucketName,

						Key = string.Format("{0}/{1}", DateTime.Now.ToString("MMMM").ToLower(), docName)
					};
					using (var ms = new MemoryStream(bytes))
					{
						request.InputStream = ms;
						client.PutObjectAsync(request).Wait();
					}
					pathURL = request.Key;
				}
				return new Response() { IsSuccess = 1, Message = "Document Uploaded Successfully", ResponseCode = 201, Result = new { Url = (amazonS3.BaseUrl + pathURL) } };
			}
			catch
			{
				throw;
			}
		}

		public static string GetFileExtension(string base64String)
		{
			var data = base64String.Substring(0, 5);

			switch (data.ToUpper())
			{
				case "IVBOR":
					return "png";
				case "/9J/4":
					return "jpg";
				case "JVBER":
					return "pdf";
				case "AAAAF":
					return "mp4";
				case "AAABA":
					return "ico";
				case "UMFYI":
					return "rar";
				case "E1XYD":
					return "rtf";
				case "U1PKC":
					return "txt";
				case "MQOWM":
				case "77U/M":
					return "srt";
				case "0M8R4":
					return "doc";
				case "UEsDB":
					return "excel";
				case "77u/U":
					return "csv";
				case "UKLGR":
					return "webp";
				default:
					return string.Empty;
			}
		}
	}

}
