using System;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web.Http;

namespace Musicly.Controllers.APIs
{

    public class VideosController : ApiController
    {
   
        public HttpResponseMessage Get(string fileName, string ext)
        {
            var videoStream = new VideoStream(fileName, ext);

            var responce = Request.CreateResponse();

            responce.Content = new PushStreamContent((Action<Stream,HttpContent,TransportContext>)videoStream.WriteToOutputStream,
                new MediaTypeHeaderValue($"video/{ext}"));

            return responce;
        }
    }
}
