using NoteManager.Core.DTO;
using NoteManager.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NoteManager.Core.Services
{
    public class NoteManagerService
    {
        public LoginDTO Login(LoginRequestDTO loginUser)
        {
            try
            {
                LoginDTO result = null;
                using (NoteManagerEntities ctx = new NoteManagerEntities())
                {
                    if (ctx.Users.Any(i => i.Email.Equals(loginUser.Username) && i.IsCanceled == false))
                    {
                        Users user = ctx.Users.FirstOrDefault(i => i.Email.Equals(loginUser.Username));
                        // inserire cript password
                        if (user.Password.Equals(loginUser.Password))
                        {
                            result = new LoginDTO
                            {
                                Name = user.Name,
                                Lastname = user.Lastname,
                                Email = user.Email,
                                UserId = user.UserId,
                                Role = user.Role
                            };
                        }
                    }
                }
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
