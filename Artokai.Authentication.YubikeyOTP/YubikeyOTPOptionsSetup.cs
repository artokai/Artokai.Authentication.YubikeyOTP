using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;

namespace Artokai.Authentication.YubikeyOTP
{
    internal class YubikeyOTPOptionsSetup : IConfigureOptions<YubikeyOTPOptions>
    {
        private const string CONFIGURATION_SECTION_NAME = "YubikeyOTP";
        private readonly IConfigurationSection _configsection;

        public YubikeyOTPOptionsSetup(IConfiguration configuration)
        {
            _configsection = configuration.GetSection(CONFIGURATION_SECTION_NAME);
        }

        public void Configure(YubikeyOTPOptions options)
        {
            _configsection.Bind(options);
        }
    }
}
