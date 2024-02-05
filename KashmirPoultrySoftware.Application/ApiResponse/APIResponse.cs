using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace KashmirPoultrySoftware.Application.ApiResponse
{
    public class APIResponse<T>
    {
        public bool IsSuccess { get; set; }
        public string Message { get; set; } = null!;
        public HttpStatusCode StatusCode { get; set; }
        public T? Result { get; set; }


        public static APIResponse<T> SuccessResponse(string message = "Success", HttpStatusCode statusCode = HttpStatusCode.OK,T ? result = default)
        {
            return new APIResponse<T>()
            {
                IsSuccess = true,
                Message = message,
                Result = result,
                StatusCode = statusCode
            };
        }




        public static APIResponse<T> ErrorResponse(string message = "Error", HttpStatusCode statusCode = HttpStatusCode.InternalServerError)
        {
            return new APIResponse<T>()
            {
                IsSuccess = false,
                Message = message,
                StatusCode = statusCode
            };
        }
    }
}
