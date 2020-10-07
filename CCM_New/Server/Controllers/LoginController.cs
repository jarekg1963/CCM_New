using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CCM_New.Server.Data;
using CCM_New.Shared;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CCM_New.Server.Controllers
{
    public class LoginController : ControllerBase
    {
        CCMContext db;
        Cache cache = Cache.instance();
            
        public LoginController(CCMContext context)
        {
            this.db = context;
        }

        [HttpPost]
        [Route("api/login")]
        public Session login([FromBody] Session s1)
        {
            var u = s1.user;
            var h = Cache.CreateMD5(u.UserPassword);
            var u1 = db.TblUsers.Where(o => o.UserName == u.UserName && o.UserPassword == h).FirstOrDefault();
            if (u1 == null)
            {
                return new Session() { ok = false };
            }
            var s = new Session()
            {
                ok = true,
                sessionID = Guid.NewGuid().ToString(),
                user = u1,
                lastActivity = DateTime.Now
            };

            s.user.UserPassword = "";
            
            cache.sessions.Add(s);
            cache.save();

            return s;
        }


     
        public static string CreateMD5(string input)
        {
            // Use input string to calculate MD5 hash
            using (System.Security.Cryptography.MD5 md5 = System.Security.Cryptography.MD5.Create())
            {
                byte[] inputBytes = System.Text.Encoding.ASCII.GetBytes(input);
                byte[] hashBytes = md5.ComputeHash(inputBytes);

                // Convert the byte array to hexadecimal string
                StringBuilder sb = new StringBuilder();
                for (int i = 0; i < hashBytes.Length; i++)
                {
                    sb.Append(hashBytes[i].ToString("X2"));
                }
                return sb.ToString();
            }
        }

    }
    public class SessionController : appController
    {
        [HttpGet]
        [Route("api/session")]
        [SessionFilter]
        public Session GetSession([FromQuery] string sessionID)
        {
            // System.Console.WriteLine(Newtonsoft.Json.JsonConvert.SerializeObject(this.session));
            if (this.session == null)
            {
                return new Session()
                {
                    ok = false
                };
            }
            return this.session;
        }

    }

}
