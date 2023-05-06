using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NoteManager.Core.DTO
{
    public class UserDTO
    {
        public int UserId { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public Boolean IsCanceled { get; set; }
        public DateTime CanceledOn { get; set; }
        public string CanceledBy { get; set; }
    }
}
