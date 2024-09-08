
namespace DakshIndia.SharedLayer.Common
{

    public class EmailConfiguration
    {
        public string From { get; set; } = "";
        public string Password { get; set; } = "";
        public string Host { get; set; } = "";
        public bool EnableSSL { get; set; }
        public int Port { get; set; }
    }
}
