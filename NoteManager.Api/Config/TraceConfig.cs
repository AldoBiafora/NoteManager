﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;

namespace NoteManager.Api.Config
{
    public static class TraceConfig 
    {
        public static void Register(HttpConfiguration config)
        {
            //config.EnableSystemDiagnosticsTracing();
        }
    }
}