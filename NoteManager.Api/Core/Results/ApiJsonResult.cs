using NoteManager.Api.Core.Models;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http;

namespace NoteManager.Api.Core.Results
{
    public class ApiJsonResult : IHttpActionResult
    {
        private static string API_JSON_USER = "ApiJsonResult";

        private readonly HttpRequestMessage _request;
        private readonly HttpStatusCode _status; 
        private object _result;
        private Action<HttpResponseMessage> _configResponse;

        public ApiJsonResult(HttpRequestMessage request, HttpStatusCode status, ApiResponse result, Action<HttpResponseMessage> configResponse)
        {
            _request = request;
            _status = status;
            _result = result;
            _configResponse = configResponse;
        }

        public Task<HttpResponseMessage> ExecuteAsync(CancellationToken cancellationToken)
        {
            var response = _request.CreateResponse(_status, _result, GlobalConfiguration.Configuration.Formatters.JsonFormatter);

            if (_configResponse != null)
                _configResponse(response);

            return Task.FromResult(response);
        }

        #region Static Entry Point

        public static ApiJsonResult ToStatusCode(HttpRequestMessage request, HttpStatusCode statusCode, string message, Action<HttpResponseMessage> config = null)
        {
            var result = new ApiResponse<string>
            {
                Info = message,
                Status = (int)statusCode
            };

            return new ApiJsonResult(request, statusCode, result, config);
        }

        public static ApiJsonResult Ok<T>(HttpRequestMessage request, T dto, Action<HttpResponseMessage> config = null)
        {
            var result = new ApiResponse<T>
            {
                Data = dto,
                Status = (int)HttpStatusCode.OK,
            };

            return new ApiJsonResult(request, HttpStatusCode.OK, result, config);
        }

        public static ApiJsonResult Ok<T>(HttpRequestMessage request, List<T> dto, int totalCount, Action<HttpResponseMessage> config = null)
        {
            var result = new ApiListResponse<T>
            {
                Data = dto,
                Count = dto.Count,
                TotalCount = totalCount,
                Status = (int)HttpStatusCode.OK,
            };

            return new ApiJsonResult(request, HttpStatusCode.OK, result, config);
        }

        public static ApiJsonResult NotAcceptable(HttpRequestMessage request, string message, Action<HttpResponseMessage> config = null)
        {
            var result = new ApiResponse
            {
                Info = message,
                Status = (int)HttpStatusCode.NotAcceptable
            };

            return new ApiJsonResult(request, HttpStatusCode.Unauthorized, result, config);
        }

        public static ApiJsonResult Unauthorized(HttpRequestMessage request, string message, Action<HttpResponseMessage> config = null)
        {
            var result = new ApiResponse
            {
                Info = message,
                Status = (int)HttpStatusCode.Unauthorized
            };

            return new ApiJsonResult(request, HttpStatusCode.Unauthorized, result, config);
        }

        public static ApiJsonResult BadRequest(HttpRequestMessage request, string message, Action<HttpResponseMessage> config = null)
        {
            var result = new ApiResponse
            {
                Info = message,
                Status = (int)HttpStatusCode.BadRequest
            };

            return new ApiJsonResult(request, HttpStatusCode.BadRequest, result, config);
        }

        public static ApiJsonResult NotFound(HttpRequestMessage request, string message, Action<HttpResponseMessage> config = null)
        {
            var result = new ApiResponse
            {
                Info = message,
                Status = (int)HttpStatusCode.NotFound
            };

            return new ApiJsonResult(request, HttpStatusCode.NotFound, result, config);
        }

        public static ApiJsonResult InternalServerError(HttpRequestMessage request, Exception ex, Action<HttpResponseMessage> config = null)
        {
            var result = new ApiResponse
            {
                Error = new ApiErrorResponse("An unexpected error occurred - " + ex.Message, ex),
                Status = (int)HttpStatusCode.InternalServerError
            };

            return new ApiJsonResult(request, HttpStatusCode.InternalServerError, result, config);
        }

        public static ApiJsonResult InternalServerError(HttpRequestMessage request, string message, Action<HttpResponseMessage> config = null)
        {
            var result = new ApiResponse
            {
                Error = new ApiErrorResponse(message),
                Status = (int)HttpStatusCode.InternalServerError
            };

            return new ApiJsonResult(request, HttpStatusCode.InternalServerError, result, config);
        }

        #endregion
    }
}