using System.Threading.Tasks;

namespace Microsoft.AspNetCore.Identity
{
    public static class UserManagerExtensions
    {
        private const string LoginProviderName = "[YubikeyOTP]";
        private const string TokenName = "public_id";

        public static async Task<string> GetYubikeyOTPTokenAsync<TUser>(this UserManager<TUser> manager, TUser user) where TUser : class
        {
            return await manager.GetAuthenticationTokenAsync(user, LoginProviderName, TokenName);
        }

        public static async Task<IdentityResult> SetYubikeyOTPTokenAync<TUser>(this UserManager<TUser> manager, TUser user, string value) where TUser : class
        {
            return await manager.SetAuthenticationTokenAsync(user, LoginProviderName, TokenName, value);
        }

        public static async Task<IdentityResult> RemoveYubikeyOTPTokenAsync<TUser>(this UserManager<TUser> manager, TUser user) where TUser : class
        {
            return await manager.RemoveAuthenticationTokenAsync(user, LoginProviderName, TokenName);
        }
    }
}
