using NoteManager.Api.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using log4net;

namespace NoteManager.Api.Controllers
{
    public class BaseApiController : ApiController
    {
        public Autofac.IComponentContext IocContext { get; set; }

        protected ILog Logger
        {
            get
            {
                return ApiManager.GetLogger();
            }
        }
    }
}