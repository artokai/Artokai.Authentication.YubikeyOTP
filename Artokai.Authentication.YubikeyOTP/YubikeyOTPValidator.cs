using Microsoft.Extensions.Options;
using System;
using System.Threading.Tasks;
using YubicoDotNetClient;

namespace Artokai.Authentication.YubikeyOTP
{
    public class YubikeyOTPValidator
    {
        public string ApiKey { get; }
        public string ClientId { get; }

        public YubikeyOTPValidator(IOptions<YubikeyOTPOptions> options) : this(options?.Value?.ClientId, options?.Value?.ApiKey)
        {
        }

        public YubikeyOTPValidator(string clientId, string apiKey)
        {
            if (string.IsNullOrEmpty(clientId))
                throw new ArgumentException("ClientId cannot be null or empty!", "clientId");
            if (string.IsNullOrEmpty(apiKey))
                throw new ArgumentException("ApiKey cannot be null or empty!", "apiKey");

            ClientId = clientId;
            ApiKey = apiKey;
        }

        public async Task<YubikeyOTPValidationResult> Validate(string token)
        {
            try
            {
                var client = new YubicoClient(ClientId, ApiKey);
                if (!YubicoClient.IsOtpValidFormat(token))
                    return YubikeyOTPValidationResult.Failure("Invalid OTP format!");

                var response = await client.VerifyAsync(token);
                if (response.Status != YubicoResponseStatus.Ok)
                    return YubikeyOTPValidationResult.Failure(response.Status.ToString());

                return YubikeyOTPValidationResult.Success(response.PublicId);
            }
            catch (YubicoValidationFailure e)
            {
                return YubikeyOTPValidationResult.Failure(e.Message);
            }
        }
    }
}

