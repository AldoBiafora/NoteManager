using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Formatting;
using System.Web;
using System.Web.Http;

namespace NoteManager.Config
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            config.MapHttpAttributeRoutes();

            #region Formatters

            var json = config.Formatters.JsonFormatter;
            json.SerializerSettings.DateFormatHandling = DateFormatHandling.IsoDateFormat;
            json.SerializerSettings.NullValueHandling = NullValueHandling.Include;
            json.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
            json.SerializerSettings.Converters.Add(new IsoDateTimeConverter
            {
                DateTimeFormat = @"yyyy-MM-ddTHH\:mm\:ss\Z"
            });

            #endregion

            //if (System.Configuration.ConfigurationManager.AppSettings["EnableApiRequestLogger"] == "true")
            //    config.MessageHandlers.Add(new RequestLoggingHandler());

            //config.Services.Add(typeof(IExceptionLogger), new CommonExceptionLogger());

            //config.Services.Replace(typeof(IExceptionHandler), new JsonExceptionHandler());
        }
    }
}