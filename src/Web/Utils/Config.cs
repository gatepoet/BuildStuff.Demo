using System;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Web;

namespace Web.Utils
{
    public class Config
    {
        public string GetVersion()
        {
            var assembly = Assembly.GetAssembly(typeof(WebApiApplication));
            var versionInfo = FileVersionInfo.GetVersionInfo(assembly.Location);
            return versionInfo.ProductVersion;
        }

        public string GetEnvironment()
        {
            return ConfigurationManager.AppSettings["Environment"];
        }
    }
}