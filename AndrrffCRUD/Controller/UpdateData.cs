using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System.Net.Http;
using AndrrffCRUD.Model;
using System.Text;

namespace AndrrffCRUD.Controller
{
    public static class UpdateData
    {
        [FunctionName("RunUpdateData")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Function, "put", Route = null)] HttpRequest req,
            ILogger log)
        {
            log.LogInformation("HTTP trigger function processed a request.");

            string requestBody  = await new StreamReader(req.Body).ReadToEndAsync();
            dynamic data        = JsonConvert.DeserializeObject(requestBody);
            var client          = new HttpClient();


            HttpResponseMessage responseMessage = GetConnection.Run(log).Result;

            if (responseMessage.IsSuccessStatusCode)
            {
                var response    = new HttpResponseMessage();
                var json        = JsonConvert.SerializeObject(data);

                response = await client.PutAsync(Environment.GetEnvironmentVariable("URL") + "/api/put", new StringContent(json, Encoding.UTF8, "application/json"));

                string result = await response.Content.ReadAsStringAsync();
                return new OkObjectResult(result);
            }
            else
            {
                return new OkObjectResult(responseMessage);
            }
        }
    }
}
