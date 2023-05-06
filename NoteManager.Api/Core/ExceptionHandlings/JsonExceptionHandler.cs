using System.Collections.Generic;
using System.Net;
using System.Web.Http.ExceptionHandling;
using NoteManager.Api.Core.Models;
using NoteManager.Api.Core.Results;

namespace NoteManager.Api.Core.ExceptionHandlings
{
    public class JsonExceptionHandler : ExceptionHandler
    {
        public override void Handle(ExceptionHandlerContext context)
        {
            var response = new ApiResponse
            {
                Errors = new List<ApiErrorResponse>()
                {
                    new ApiErrorResponse(context.Exception.Message, context.Exception)
                },
                Status = (int)HttpStatusCode.InternalServerError
            };

            context.Result = ApiJsonResult.InternalServerError(context.Request, context.Exception);
        }
    }
}