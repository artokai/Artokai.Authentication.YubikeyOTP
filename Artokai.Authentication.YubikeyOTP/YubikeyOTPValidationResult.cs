namespace Artokai.Authentication.YubikeyOTP
{
    public class YubikeyOTPValidationResult
    {
        public bool IsSuccess { get; set; }
        public string Message { get; set; }
        public string TokenId { get; set; }

        public static YubikeyOTPValidationResult Failure(string msg)
        {
            return new YubikeyOTPValidationResult { IsSuccess = false, Message = msg };
        }

        public static YubikeyOTPValidationResult Success(string tokenId)
        {
            return new YubikeyOTPValidationResult { IsSuccess = true, Message = "", TokenId = tokenId };
        }
    }
}

