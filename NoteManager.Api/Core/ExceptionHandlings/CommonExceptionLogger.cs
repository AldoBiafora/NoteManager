using NoteManager.Api.Core;
using System.Web.Http.ExceptionHandling;

namespace NoteManager.Api.Core.ExceptionHandlings
{
    public class CommonExceptionLogger : ExceptionLogger
    {
        public override void Log(ExceptionLoggerContext context)
        {
            ApiManager.GetLogger(context.Request).Error("Unhandled Exception", context.Exception);
        }
    }
}