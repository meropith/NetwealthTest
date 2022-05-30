using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.Net;

namespace Exchange.API.Models
{
    public class ApiErrorResponse : Response
    {
        public object Data { get; set; } = null;

        public ApiErrorResponse(HttpStatusCode statusCode, params string[] messages)
        {
            StatusCode = statusCode;
            Messages = messages.ToList();
        }


        public ApiErrorResponse(ModelStateDictionary.ValueEnumerable modelStateValues)
        {
            Messages = HandleError(modelStateValues).ToList();
            StatusCode = HttpStatusCode.BadRequest;
        }

        private static string[] HandleError(ModelStateDictionary.ValueEnumerable modelStateValues)
        {
            var errors = new List<string>();

            if (!modelStateValues.Any()) return errors.ToArray();

            foreach (var modelState in modelStateValues)
            {
                if (!modelState.Errors.Any()) continue;

                errors.AddRange(modelState.Errors.Select(error => error.ErrorMessage));
            }

            return errors.ToArray();
        }
    }
}
