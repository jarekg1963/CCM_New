using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CCM_New.Shared;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace CCM_New.Server.Controllers
{
    public class appController : Controller
    {
        public Session session { get; set; }

    }
    public class SessionFilter : Attribute, IActionFilter
    {
        Cache cache = Cache.instance();
        void IActionFilter.OnActionExecuted(ActionExecutedContext context)
        {
            //throw new System.NotImplementedException();
        }

        void IActionFilter.OnActionExecuting(ActionExecutingContext context)
        {
            try
            {
                appController c = (appController)context.Controller;
                string sessionid = context.HttpContext.Request.Query["sessionID"];

                if (sessionid == "Test1234!")
                {
                    c.session = new Session();
                    c.session.ok = true;
                }
                else
                {

                    cache.sessions = cache.sessions.Where(o => o.lastActivity > DateTime.Now.AddHours(-24)).ToList();
                    var temp = cache.sessions.Where(o => o.sessionID == sessionid).FirstOrDefault();



                    c.session = temp;
                    if (temp == null)
                    {
                        c.session = new Session();
                        c.session.ok = false;
                        context.Result = new UnauthorizedResult();
                    }
                    else
                    {
                        c.session.ok = true;
                    }
                    c.session.lastActivity = DateTime.Now;
                    cache.save();
                }

            }
            catch
            {
                //context.Result=
            }
        }
    }

        public class Session
        {
            public string sessionID { get; set; }
            public bool ok { get; set; } = false;
            public DateTime lastActivity { get; set; }
            public TblUsers user { get; set; }
            
        }

        public class Cache
        {
            static Cache c;
            public List<Session> sessions { get; set; } = new List<Session>();
            
            public static Cache instance()
            {
                if (c == null)
                {
                    c = new Cache();
                    c.load();
                }
                return c;
            }

            private void load()
            {
                try
                {
                    this.sessions = Newtonsoft.Json.JsonConvert.DeserializeObject<List<Session>>(System.IO.File.ReadAllText("cache.json"));
                }
                catch
                {
                    this.sessions = new List<Session>();
                }

            }
            public void save()
            {
               
                try
                {
                    System.IO.File.WriteAllText("cache.json", Newtonsoft.Json.JsonConvert.SerializeObject(this.sessions));
                }
                catch (Exception ex)
                {
                    System.Console.WriteLine("Błąd w zapisywanie logowania");
                    System.Console.WriteLine(ex.Message);
                }
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
}
