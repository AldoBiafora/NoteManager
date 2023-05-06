using log4net;
using System.Net.Http;

namespace NoteManager.Api.Core
{
    public static class ApiManager
    {
        public static ILog GetLogger(HttpRequestMessage request = null)
        {
            return LogManager.GetLogger("API");
        }

        public static ILog GetRequestLogger(HttpRequestMessage request = null)
        {
            return LogManager.GetLogger("API_REQUEST_TRACING");
        }
    }
}