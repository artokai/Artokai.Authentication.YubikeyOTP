using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Artokai.Authentication.YubikeyOTP
{
    internal class YubikeyOTPTokenProvider<TUser> : IUserTwoFactorTokenProvider<TUser>
        where TUser : class
    {
        private readonly YubikeyOTPValidator _validator;

        public YubikeyOTPTokenProvider(YubikeyOTPValidator validator)
        {
            _validator = validator;
        }

        public async Task<bool> CanGenerateTwoFactorTokenAsync(UserManager<TUser> manager, TUser user)
        {
            var key = await manager.GetYubikeyOTPTokenAsync(user);
            return !string.IsNullOrWhiteSpace(key);
        }

        public Task<string> GenerateAsync(string purpose, UserManager<TUser> manager, TUser user)
        {
            return Task.FromResult(string.Empty);
        }

        public async Task<bool> ValidateAsync(string purpose, string token, UserManager<TUser> manager, TUser user)
        {
            var response = await _validator.Validate(token);
            if (!response.IsSuccess)
                return false;

            var storedId = await manager.GetYubikeyOTPTokenAsync(user);
            if (string.IsNullOrEmpty(storedId) || response.TokenId != storedId)
                return false;

            return true;
        }
    }
}
