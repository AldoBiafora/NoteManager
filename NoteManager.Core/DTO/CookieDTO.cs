using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NoteManager.Core.DTO
{
    public class CookieDTO
    {
        public string Token { get; set; }
        public string CompleteName { get; set; }
        public int UserId { get; set; }
        public string Role { get; set; }
    }
}
