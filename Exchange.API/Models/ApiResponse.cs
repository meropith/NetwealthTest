using System.Collections.Generic;
using System.Linq;
using System.Net;
namespace Exchange.API.Models
{
    public class ApiResponse<T> : Response
    {
        public ApiResponse(HttpStatusCode statusCode = HttpStatusCode.OK, params string[] messages)
        {
            Messages = messages.ToList();
            StatusCode = statusCode;
        }

        public ApiResponse(T data, HttpStatusCode statusCode = HttpStatusCode.OK, params string[] messages)
        {
            Messages = messages.ToList();
            Data = data;
            StatusCode = statusCode;
        }

        public ApiResponse()
        {
        }

        public T Data { get; set; }
    }

    public class Response
    {
        public HttpStatusCode StatusCode { get; set; }
        public string StatusTitle => StatusCode.ToString();
        public List<string> Messages { get; set; }          
    }
}
