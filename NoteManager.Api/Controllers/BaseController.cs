using Core.Utils;
using Newtonsoft.Json;
using NoteManager.Core.DTO;
using NoteManager.Core.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;

namespace FinanceManager.Api.Controllers
{
    public class BaseController : ApiController
    {
        public UserDTO CurrentUser
        {
            get
            {
                UserDTO result = null;

                string[] keys = HttpContext.Current.Request.Cookies.AllKeys;

                string jsonString;
                if (keys.Any(k => k.Equals(AppConstants.TOKEN_NAME)))
                {
                    jsonString = HttpContext.Current.Request.Cookies[AppConstants.TOKEN_NAME].Value;

                    if (!string.IsNullOrEmpty(jsonString))
                    {
                        jsonString = HttpUtility.UrlDecode(jsonString);
                        jsonString = CipherUtils.DecryptPublic(jsonString);
                        result = JsonConvert.DeserializeObject<UserDTO>(jsonString);

                    }
                }

                if (result == null)
                {
                    throw new Exception(String.Format("No cookie named '{0}' found on request", AppConstants.TOKEN_NAME.ToString()));
                }

                return result;
            }
        }
    }
}
