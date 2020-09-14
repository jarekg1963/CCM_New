using CCM_New.Shared;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CCM_New.Client
{
    public class LoginService
    {
        public LoginService(NavigationManager _navigationManager, IJSRuntime _JSRuntime)
        {
            navigationManager = _navigationManager;
            jSRuntime = _JSRuntime;
        }

        public NavigationManager navigationManager;
        public IJSRuntime jSRuntime;

        public Session session { get; set; }
        public string sessionID { get; set; }
        public string sessionKey { get; set; } = "ccmSSO";


        public void setTitle(string title)
        {
            var t = $"CCM - {title}";
            jSRuntime.InvokeVoidAsync("setTitle", new object[] { t });

        }


        public bool isLogged { get; set; }

        public event Action refresh;
        public void CallRefresh()
        {
            this.refresh?.Invoke();
        }

        

        public string PrintDate(DateTime? dt)
        {
            try
            {
                if (dt == null) return "";
                return dt.Value.ToString("yyyy-MM-dd");
            }
            catch
            {
                return "";
            }
        }

   
        public int securityLevel { get; set; } = 0;
        public double? stawka { get; set; } = null;
        public double? stawka1 { get; set; } = null;
        public bool? stalyCennik { get; set; } = null;

        public class Session
        {
            public string sessionID { get; set; }
            public bool ok { get; set; } = false;
            public DateTime lastActivity { get; set; }
            public TblUsers user { get; set; }

        }
    }
}
