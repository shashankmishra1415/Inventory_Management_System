using InventorySystem.SharedLayer.Models.Response;

namespace InventorySystem.Infrastructure.Repositories.Interfaces
{
    public interface IBaseRepository
    {
        public Task<List<T>> GetListById<T>(string procedureName, int id);
        public Task<List<T>> GetList<T>(string procedureName);
        public Task<T> GetSingleRecordById<T>(string procedureName, int id);
        public Task<T> GetSingleRecord<T>(string procedureName);
        public Task<Response> Post<T>(string procedureName, T model);
        public Task<Response> Post<T>(string procedureName, T model, int userId);
        public Task<Response> Put<T>(string procedureName, T model, int id);
        public Task<Response> Put<T>(string procedureName, T model, int id, int userId);
        public Task<Response> Patch<T>(string procedureName, T model, int id);
        public Task<Response> Patch<T>(string procedureName, T model, int id, int userId);
        public Task<Response> Patch(string procedureName, int id);
        public Task<Response> Patch(string procedureName, int id, int userId);
        public Task<Response> Delete(string procedureName, int id);
        public Task<Response> Delete(string procedureName, int id, int userId);

    }
}
