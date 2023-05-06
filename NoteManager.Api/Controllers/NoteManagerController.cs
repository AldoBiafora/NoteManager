using Core.Utils;
using FinanceManager.Api.Controllers;
using Newtonsoft.Json;
using NoteManager.Api.Core.Results;
using NoteManager.Core.DTO;
using NoteManager.Core.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;

namespace NoteManager.Api.Controllers
{
    public class NoteManagerController : BaseController
    {
        [RoutePrefix("api/data")]
        public class PluginController : BaseController
        {
            protected NoteManagerService _service = new NoteManagerService();

            [Route("login")]
            [HttpPost]
            public IHttpActionResult Login([FromBody] LoginRequestDTO loginUser)
            {
                try
                {
                    CookieDTO result = null;
                    LoginDTO user = _service.Login(loginUser);
                    if (user != null)
                    {
                        result = new CookieDTO()
                        {
                            Token = CipherUtils.EncryptPublic(JsonConvert.SerializeObject(user)),
                            CompleteName = string.Format("{0} {1}", user.Name, user.Lastname),
                            UserId = user.UserId,
                            Role = user.Role
                        };
                    }

                    return ApiJsonResult.Ok<CookieDTO>(Request, result);

                }
                catch (Exception ex)
                {
                    return ApiJsonResult.InternalServerError(Request, ex);
                }

            }
        }
    }
}
