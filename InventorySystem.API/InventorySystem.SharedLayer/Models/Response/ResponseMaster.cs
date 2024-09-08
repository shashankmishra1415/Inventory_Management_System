namespace InventorySystem.SharedLayer.Models.Response
{
    public class UserConfiguration
    {
        public int Id { get; set; }
        public string UserName { get; set; }
    }
    public class JwtTokenResponse
    {
        public string? Token { get; set; }
        public bool IsRefreshToken { get; set; }
    }
}
