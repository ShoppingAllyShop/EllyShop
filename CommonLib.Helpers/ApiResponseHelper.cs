using CommonLib.Constants;
using CommonLib.Models.Api;

namespace CommonLib.Helpers
{
    public static class ApiResponseHelper
    {
        public static ApiResponse FormatSuccess(object data)
        {
            return new ApiResponse
            {
                Result = data,
                Status = StatusConstants.Success,
                Message = string.Empty
            };
        }

        public static ApiResponse FormatError(string message)
        {
            return new ApiResponse
            {
                Message = message,
                Status = StatusConstants.Error
            };
        }
    }
}
