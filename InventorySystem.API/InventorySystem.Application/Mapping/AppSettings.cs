namespace InventorySystem.Application
{
    public class JWTSettings
    {
        public string Issuer { get; set; } = "";
        public string Audience { get; set; } = "";
        public string SecretKey { get; set; } = "";
        public int ExpireInMinutes { get; set; } = 0;
    }

    public class AmazonS3
    {
        public string BucketName { get; set; }
        public string AccessKey { get; set; }
        public string SecurityKey { get; set; }
		public string BaseUrl { get; set; }
	}
}
