using InventorySystem.Infrastructure.Repositories.Interfaces;
using InventorySystem.SharedLayer.Models.Response;
using Dapper;
using System.Data;
using TravelAndExpense.Infrastructure.Common;

namespace InventorySystem.Infrastructure.Repositories
{
    public class BaseRepository : IBaseRepository
    {
        private readonly DbContext dbContext;
        public BaseRepository(DbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<List<T>> GetListById<T>(string procedureName, int id)
        {
            using (IDbConnection db = dbContext.GetConnection())
            {
                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("_id", id);
                return db.Query<T>(procedureName, parameters, commandType: CommandType.StoredProcedure).ToList();
            }
        }

        public async Task<List<T>> GetList<T>(string procedureName)
        {
            using (IDbConnection db = dbContext.GetConnection())
            {
                DynamicParameters parameters = new DynamicParameters();
                return db.Query<T>(procedureName, commandType: CommandType.StoredProcedure).ToList();
            }
        }

        public async Task<T> GetSingleRecordById<T>(string procedureName, int id)
        {
            using (IDbConnection db = dbContext.GetConnection())
            {
                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("_id", id);
                return db.Query<T>(procedureName, parameters, commandType: CommandType.StoredProcedure).SingleOrDefault();
            }
        }

        public async Task<T> GetSingleRecord<T>(string procedureName)
        {
            using (IDbConnection db = dbContext.GetConnection())
            {
                DynamicParameters parameters = new DynamicParameters();
                return db.Query<T>(procedureName, parameters, commandType: CommandType.StoredProcedure).SingleOrDefault();
            }
        }

        public async Task<Response> Post<T>(string procedureName, T model, int userId)
        {
            using (IDbConnection db = dbContext.GetConnection())
            {
                DynamicParameters parameters = DynamicParameterHelper.BuildParameters<T>(model);
                parameters.Add("_userId", userId);
                return db.Query<Response>(procedureName, parameters, commandType: CommandType.StoredProcedure).SingleOrDefault();
            }
        }

        public async Task<Response> Post<T>(string procedureName, T model)
        {
            using (IDbConnection db = dbContext.GetConnection())
            {
                DynamicParameters parameters = DynamicParameterHelper.BuildParameters<T>(model);
                return db.Query<Response>(procedureName, parameters, commandType: CommandType.StoredProcedure).SingleOrDefault();
            }
        }

        public async Task<Response> Put<T>(string procedureName, T model, int id)
        {
            using (IDbConnection db = dbContext.GetConnection())
            {
                DynamicParameters parameters = DynamicParameterHelper.BuildParameters<T>(model);
                parameters.Add("_id", id);
                return db.Query<Response>(procedureName, parameters, commandType: CommandType.StoredProcedure).SingleOrDefault();
            }
        }

        public async Task<Response> Put<T>(string procedureName, T model, int id, int userId)
        {
            using (IDbConnection db = dbContext.GetConnection())
            {
                DynamicParameters parameters = DynamicParameterHelper.BuildParameters<T>(model);
                parameters.Add("_id", id);
                parameters.Add("_userId", userId);
                return db.Query<Response>(procedureName, parameters, commandType: CommandType.StoredProcedure).SingleOrDefault();
            }
        }

        public async Task<Response> Patch<T>(string procedureName, T model, int id)
        {
            using (IDbConnection db = dbContext.GetConnection())
            {
                DynamicParameters parameters = DynamicParameterHelper.BuildParameters<T>(model);
                parameters.Add("_id", id);
                return db.Query<Response>(procedureName, parameters, commandType: CommandType.StoredProcedure).SingleOrDefault();
            }
        }

        public async Task<Response> Patch<T>(string procedureName, T model, int id, int userId)
        {
            using (IDbConnection db = dbContext.GetConnection())
            {
                DynamicParameters parameters = DynamicParameterHelper.BuildParameters<T>(model);
                parameters.Add("_id", id);
                parameters.Add("_userId", userId);
                return db.Query<Response>(procedureName, parameters, commandType: CommandType.StoredProcedure).SingleOrDefault();
            }
        }

        public async Task<Response> Patch(string procedureName, int id)
        {
            using (IDbConnection db = dbContext.GetConnection())
            {
                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("_id", id);
                return db.Query<Response>(procedureName, parameters, commandType: CommandType.StoredProcedure).SingleOrDefault();
            }
        }

        public async Task<Response> Patch(string procedureName, int id, int userId)
        {
            using (IDbConnection db = dbContext.GetConnection())
            {
                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("_id", id);
                parameters.Add("_userId", userId);
                return db.Query<Response>(procedureName, parameters, commandType: CommandType.StoredProcedure).SingleOrDefault();
            }
        }

        public async Task<Response> Delete(string procedureName, int id)
        {
            using (IDbConnection db = dbContext.GetConnection())
            {
                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("_id", id);
                return db.Query<Response>(procedureName, parameters, commandType: CommandType.StoredProcedure).SingleOrDefault();
            }
        }

        public async Task<Response> Delete(string procedureName, int id, int userId)
        {
            using (IDbConnection db = dbContext.GetConnection())
            {
                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("_id", id);
                parameters.Add("_userId", userId);
                return db.Query<Response>(procedureName, parameters, commandType: CommandType.StoredProcedure).SingleOrDefault();
            }
        }
    }
}
