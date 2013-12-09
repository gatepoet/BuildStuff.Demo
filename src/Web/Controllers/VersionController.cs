using System.Diagnostics;
using System.Reflection;
using System.Web.Http;

namespace Web.Controllers
{
    public class VersionController : ApiController
    {
        // GET api/<controller>
        public string Get()
        {
            var assembly = Assembly.GetAssembly(typeof (WebApiApplication));
            var versionInfo = FileVersionInfo.GetVersionInfo(assembly.Location);
            return versionInfo.ProductVersion;
        }

        // GET api/<controller>/5
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<controller>
        public void Post([FromBody]string value)
        {
        }

        // PUT api/<controller>/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/<controller>/5
        public void Delete(int id)
        {
        }
    }
}