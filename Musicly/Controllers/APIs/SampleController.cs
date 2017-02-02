using System.Web.Http;

namespace Musicly.Controllers.APIs
{
    public class SampleController : ApiController
    {
        public string Get()
        {
            return "Hello";
        }
    }
}
