using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HSaaS.Account.Web
{
    public class HSaaSAccountOptions
    {
        /// <summary>
        /// Default value: "Windows".
        /// </summary>
        public string WindowsAuthenticationSchemeName { get; set; }

        public HSaaSAccountOptions()
        {
            //TODO: This makes us depend on the Microsoft.AspNetCore.Server.IISIntegration package.
            WindowsAuthenticationSchemeName = "Windows"; //Microsoft.AspNetCore.Server.IISIntegration.IISDefaults.AuthenticationScheme;
        }
    }
}
