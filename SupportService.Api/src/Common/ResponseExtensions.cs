using Microsoft.AspNetCore.Mvc;

namespace SupportService.Api.src.Utilities
{
    public class ApiResponse<T>
    {
        public bool Success { get; set; }
        public T Data { get; set; }
    }

    public static class ApiResponseExtensions
    {
        public static IActionResult ToApiErrorResponse(this Exception ex, int statusCode = 400)
        {
            return new ObjectResult(new ApiResponse<object>
            {
                Success = false,
                Data = new { error = ex.Message }
            })
            {
                StatusCode = statusCode
            };
        }
    }
}

   
    
