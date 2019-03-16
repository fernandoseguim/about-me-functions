using Microsoft.AspNetCore.Http;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Azure.WebJobs.Host;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace About.Me.Functions
{
    public static class LetsEncrypt
    {
        [FunctionName("LetsEncrypt")]
        public static Task<HttpResponseMessage> Run([HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "LetsEncrypt/.well-known/acme-challenge/{code}")]HttpRequest req, string code, TraceWriter log)
        {
            log.Info($"C# HTTP trigger function processed a request. {code}");

            var content = File.ReadAllText(Path.Combine(@"D:\home\site\wwwroot\.well-known\acme-challenge\", code));
            var resp = new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new StringContent(content, Encoding.UTF8, "text/plain")
            };
            return Task.FromResult(resp);
        }
    }
}
