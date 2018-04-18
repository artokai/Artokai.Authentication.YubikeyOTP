namespace Artokai.Authentication.YubikeyOTP
{
    public class YubikeyOTPOptions
    {
        public static readonly string ProviderName = "YubikeyOTP";

        public string ClientId { get; set; }
        public string ApiKey { get; set; }
    }
}
