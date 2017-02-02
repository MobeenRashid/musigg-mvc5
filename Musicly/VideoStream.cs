using System;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Web;

namespace Musicly
{
    public class VideoStream
    {
        private readonly string _filePath;

        public VideoStream(string fileName, string ext)
        {
            _filePath = HttpContext.Current.Server.MapPath($"~/Videos/{fileName}.{ext}");
        }

        public async void WriteToOutputStream(Stream outputStream, HttpContent content,
            TransportContext transportContext)
        {
            try
            {
                var buffer = new byte[65536];

                using (var fileStream = File.Open(_filePath, FileMode.Open, FileAccess.Read, FileShare.Read))
                {
                    var videoLength = (int)fileStream.Length;

                    int readedBytes = 1;

                    while (readedBytes > 0 && videoLength > 0)
                    {
                        var count = Math.Min(buffer.Length, videoLength);
                        readedBytes = fileStream.Read(buffer, 0, count);
                        await outputStream.WriteAsync(buffer, 0, readedBytes);
                        videoLength -= readedBytes;
                    }
                }
            }
            catch (HttpException)
            {

            }
            finally
            {
                outputStream.Close();
            }


        }
    }
}