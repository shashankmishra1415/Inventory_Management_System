using FluentValidation;
using InventorySystem.Infrastructure.Repositories.Interface;
using InventorySystem.Infrastructure.Repositories.Interfaces;
using InventorySystem.SharedLayer.Models.Request;
using InventorySystem.SharedLayer.Models.Response;

namespace InventorySystem.Application.Features.ProductFeature
{
    public class ProductFeature : IProductFeature
    {
        public readonly IBaseRepository baseRepository;
        private readonly IProductRepository productRepository;
        private readonly IValidator<ProductRequest> productValidator;
        public ProductFeature(IBaseRepository baseRepository, IProductRepository productRepository, IValidator<ProductRequest> productValidator)
        {
            this.baseRepository = baseRepository;
            this.productRepository = productRepository;
            this.productValidator = productValidator;
        }

        public async Task<Response> Product(int id)
        {
            var response = new Response();
            response.Result = await baseRepository.GetSingleRecordById<ProductResponse>("ProductById", id);
            response.IsSuccess = 1;
            response.Message = "Data Fetched Successfully.";
            response.ResponseCode = 200;
            return response;
        }

        public async Task<Response> Product(ProductRequest request, int userId)
        {
            Response response = await baseRepository.Post<ProductRequest>("AddProduct", request, userId);
            response.IsSuccess = 1;
            response.ResponseCode = 201;
            response.Message = "Product Added Succesfully";
            return response;
        }

        public async Task<Response> Product(ProductRequest request, int id, int userid)
        {
            Response response = await baseRepository.Put<ProductRequest>("EditProduct", request, id, userid);
            response.IsSuccess = 1;
            response.Message = "Product Updated Succesfully";
            return response;
        }

        public async Task<Response> Product(int id, int userId)
        {
            Response response = await baseRepository.Delete("DeleteProduct", id, userId);
            response.IsSuccess = 1;
            response.Message = "Product Details deleted Successfully";
            return response;
        }

		public async Task<Response> TotalCount()
		{
			Response response = new Response();
			response.Result = await productRepository.GetSingleRecord<ProductStatusCountResponse>("GetProductStatusCount");
			response.IsSuccess = 1;
			response.Message = "Data Fetched Successfully.";
			response.ResponseCode = 200;
			return response;
		}

		public async Task<Response> Product(int pageNum, int pageSize, string productSku, string productName, string eanCode, int categoryId, int manufacturerId)
        {
            Response response = new Response();
            response.Result = await productRepository.Product(pageNum, pageSize, productSku, productName, eanCode, manufacturerId, categoryId);
            response.IsSuccess = 1;
            response.Message = "Data Fetched Successfully.";
            response.ResponseCode = 200;
            return response;
        }
    }
}
