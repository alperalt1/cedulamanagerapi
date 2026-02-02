using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CedulaManager.Application.Common
{
    public class BaseResponse<T>
    {
        public bool Success { get; set; }
        public string Message { get; set; } = string.Empty;
        public T? Data { get; set; }
        public List<string>? Errors { get; set; }

        public static BaseResponse<T> Ok(T data, string message = "Operación exitosa")
            => new() { Success = true, Data = data, Message = message };

        public static BaseResponse<T> Failure(string message, List<string>? errors = null)
            => new() { Success = false, Message = message, Errors = errors };
    }
}
