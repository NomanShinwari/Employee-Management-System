using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MyLoginRegistration.Common;

namespace MyLoginRegistration.Common
{
    public class ApiResponse<T>
    {
        public bool IsSuccess { get; set; }
        public string Message { get; set; }
        public T Data { get; set; }
        public List<string> Errors { get; set; }

        // Factory methods for cleaner code
        public static ApiResponse<T> Success(T data, string message = "Success")
            => new ApiResponse<T> { IsSuccess = true, Data = data, Message = message };

        public static ApiResponse<T> Fail(string error)
            => new ApiResponse<T> { IsSuccess = false, Errors = new List<string> { error } };
    }
}
