using InventorySystem.SharedLayer.Models.Request;
using InventorySystem.SharedLayer.Models.Response;

namespace InventorySystem.Application.Features.LoginFeature.Interfaces
{
    public interface ILoginFeature
    {
        public Task<Response> Login(LoginRequest credentials);
    }
}
