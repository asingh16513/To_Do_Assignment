using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.Extensions.Logging;
using System;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace ToDoService.Middleware
{
    /// <summary>
    /// Middleware for logging requests and responses
    /// </summary>
    public class RequestLogging
    {
        private readonly RequestDelegate _next;
        private readonly ILogger _logger;
        public RequestLogging(RequestDelegate next, ILoggerFactory loggerFactory)
        {
            _next = next;
            _logger = loggerFactory.CreateLogger<RequestLogging>();
        }

        public async Task Invoke(HttpContext context)
        {
            //First, get the incoming request
            var request = await FormatRequest(context.Request);

            _logger.LogInformation(request);

            //Copy a pointer to the original response body stream
            var originalBodyStream = context.Response.Body;

            //Create a new memory stream...
            using var responseBody = new MemoryStream();
            //...and use that for the temporary response body
            context.Response.Body = responseBody;

            //Continue down the Middleware pipeline, eventually returning to this class
            await _next(context);

            //Format the response from the server
            var response = await FormatResponse(context.Response);

            _logger.LogInformation(response);

            //Copy the contents of the new memory stream (which contains the response) to the original stream, which is then returned to the client.
            await responseBody.CopyToAsync(originalBodyStream);
        }

        /// <summary>
        /// Used to get request parameteres from 
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        private async Task<string> FormatRequest(HttpRequest request)
        {
            request.EnableBuffering();
            var url = UriHelper.GetDisplayUrl(request);
            var buffer = new byte[Convert.ToInt32(request.ContentLength)];
            await request.Body.ReadAsync(buffer, 0, buffer.Length);
            var bodyAsText = Encoding.UTF8.GetString(buffer);
            request.Body.Seek(0, SeekOrigin.Begin);
            string allkeypair = "";
            IHeaderDictionary headers = request.Headers;

            foreach (var headerValuePair in headers)
            {
                allkeypair += "\n" + headerValuePair.Key + ":" + headerValuePair.Value;

            }
            return $" Logging Request {url} -- {request.Method} -- {allkeypair} -- {request.Host} -- {request.Path} -- {request.QueryString} -- {bodyAsText}";
        }

        private async Task<string> FormatResponse(HttpResponse response)
        {
            response.Body.Seek(0, SeekOrigin.Begin);
            string text = await new StreamReader(response.Body).ReadToEndAsync();
            response.Body.Seek(0, SeekOrigin.Begin);
            string allkeypair = "";

            IHeaderDictionary headers = response.Headers;

            foreach (var headerValuePair in headers)
            {
                allkeypair += "\n" + headerValuePair.Key + ":" + headerValuePair.Value;

            }
            return $"  Logging Response {allkeypair} -- {response.StatusCode} -- {text}";
        }
    }
}

