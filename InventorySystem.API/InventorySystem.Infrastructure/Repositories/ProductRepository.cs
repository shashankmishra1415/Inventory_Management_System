using Dapper;
using InventorySystem.Infrastructure.Repositories.Interface;
using InventorySystem.SharedLayer.Models.Response;
using InventorySystem.SharedLayer.Response;
using System.Data;

namespace InventorySystem.Infrastructure.Repositories
{
    public class ProductRepository : BaseRepository, IProductRepository
    {
        private readonly DbContext dbContext;
        public ProductRepository(DbContext dbContext) :base(dbContext) 
        {
            this.dbContext = dbContext;
        }

        public async Task<ProductListResponse> Product(int pageNum, int pageSize, string productSku, string productName, string eanCode, int categoryId, int manufacturerId)
        {
            using (IDbConnection db = dbContext.GetConnection())
            {
                var parameters = new DynamicParameters();
                parameters.Add("_limit", pageSize);
                parameters.Add("_offset", pageNum);
                parameters.Add("_productSku", productSku);
                parameters.Add("_productName", productName);
                parameters.Add("_categoryId", categoryId);
                parameters.Add("_manufacturerId", manufacturerId);
                parameters.Add("_eancode", eanCode);
                var list = db.QueryMultiple("GetProductList", parameters, commandType: CommandType.StoredProcedure);
                ProductListResponse Response = new ProductListResponse();
                Response.ProductsDetail = list.Read<GetAllProduct>().ToList();
                Response.PaginationResponses = list.Read<PaginationResponse>().SingleOrDefault();
                return Response;
            }
        }
    }
}
