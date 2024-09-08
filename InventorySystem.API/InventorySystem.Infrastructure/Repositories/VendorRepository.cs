using Dapper;
using InventorySystem.Infrastructure.Repositories.Interface;
using InventorySystem.SharedLayer.Models.Response;
using InventorySystem.SharedLayer.Response;
using System.Data;

namespace InventorySystem.Infrastructure.Repositories
{
    public class VendorRepository : IVendorRepository
    {
        private readonly DbContext dbContext;
        public VendorRepository(DbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<VendorListResponse> Vendor(int pageNum, int pageSize, string? companyName, string contactName, int typeId, int vendorTypeId, int statusId)
        {
            using (IDbConnection db = dbContext.GetConnection())
            {
                var parameters = new DynamicParameters();
                parameters.Add("_limit", pageSize);
                parameters.Add("_offset", pageNum);
                parameters.Add("_companyName", companyName);
                parameters.Add("_typeId", typeId);
                parameters.Add("_vendorTypeId", vendorTypeId);
                parameters.Add("_contactName", contactName);
                parameters.Add("_statusId", statusId);
                var list = db.QueryMultiple("GetVendorList", parameters, commandType: CommandType.StoredProcedure);
                VendorListResponse response = new VendorListResponse();
                response.VendorDetail = list.Read<VendorDetail>().ToList();
                response.PaginationResponses = list.Read<PaginationResponse>().SingleOrDefault();
                return response;
            }
        }
    }
}