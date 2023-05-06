using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NoteManager.Api.Core.Models
{
    public class ApiResponse
    {
        [JsonProperty("errors", NullValueHandling = NullValueHandling.Ignore)]
        public List<ApiErrorResponse> Errors { get; set; }

        [JsonProperty("error", NullValueHandling = NullValueHandling.Ignore)]
        public ApiErrorResponse Error { get; set; }

        [JsonProperty("info", NullValueHandling = NullValueHandling.Ignore)]
        public string Info { get; set; }

        [JsonProperty("status", NullValueHandling = NullValueHandling.Ignore)]
        public int Status { get; set; }
    }

    public class ApiResponse<T> : ApiResponse
    {
        [JsonProperty("data", NullValueHandling = NullValueHandling.Ignore)]
        public T Data { get; set; }
    }

    public class ApiListResponse<T> : ApiResponse
    {
        [JsonProperty("totalCount", NullValueHandling = NullValueHandling.Ignore)]
        public int? TotalCount { get; set; }

        [JsonProperty("count", NullValueHandling = NullValueHandling.Ignore)]
        public int? Count { get; set; }

        [JsonProperty("data", NullValueHandling = NullValueHandling.Ignore)]
        public List<T> Data { get; set; }
    }

    public class ApiErrorResponse
    {
        public ApiErrorResponse(string errorMessage)
        {
            ErrorMessage = errorMessage;
        }

        public ApiErrorResponse(string errorMessage, Exception exception)
        {
            ErrorMessage = errorMessage;
            ErrorDetail = exception.Message;
        }

        public ApiErrorResponse(string errorMessage, string errorDetail, params object[] args)
        {
            ErrorMessage = errorMessage;
            if (args != null && args.Length > 0)
                ErrorDetail = string.Format(errorDetail, args);
            else
                ErrorDetail = errorDetail;
        }

        [JsonProperty("message", NullValueHandling = NullValueHandling.Ignore)]
        public string ErrorMessage { get; set; }

        [JsonProperty("detail", NullValueHandling = NullValueHandling.Ignore)]
        public string ErrorDetail { get; set; }
    }
}