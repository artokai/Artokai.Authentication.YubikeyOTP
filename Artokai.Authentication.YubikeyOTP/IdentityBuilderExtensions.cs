using Artokai.Authentication.YubikeyOTP;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using System;

namespace Microsoft.AspNetCore.Identity
{
    public static class IdentityBuilderExtensions
    {
        public static IdentityBuilder AddYubikeyOTPTokenProvider(this IdentityBuilder builder, Action<YubikeyOTPOptions> setupAction)
        {
            var userType = builder.UserType;
            var tokenProviderType = typeof(YubikeyOTPTokenProvider<>).MakeGenericType(userType);
            var retval = builder.AddTokenProvider(YubikeyOTPOptions.ProviderName, tokenProviderType);

            var services = builder.Services;
            services.AddSingleton<IConfigureOptions<YubikeyOTPOptions>, YubikeyOTPOptionsSetup>();
            if (setupAction != null)
            {
                services.Configure(setupAction);
            }

            services.AddTransient<YubikeyOTPValidator>();

            return retval;
        }

        public static IdentityBuilder AddYubikeyOTPTokenProvider(this IdentityBuilder builder)
        {
            return builder.AddYubikeyOTPTokenProvider(setupAction: null);
        }
    }
}
