using System;
using System.Threading.Tasks;
using Microsoft.Azure.WebJobs;
using Microsoft.Extensions.Logging;
using System.Net.Http;
using AndrrffCRUD.Model;

namespace AndrrffCRUD.Controller
{
    public static class GetConnection
    {
        [FunctionName("GetConnection")]
        public static async Task<HttpResponseMessage> Run(ILogger log, string _value = "")
        {
            var client      = new HttpClient();
            var response    = new HttpResponseMessage();

            response = await client.GetAsync(Environment.GetEnvironmentVariable("URL") + _value);
            log.LogInformation(String.Format("{0}", response));

            return response;
        }
    }
}
