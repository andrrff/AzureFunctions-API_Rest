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
using System.Net;
using Newtonsoft.Json.Linq;

namespace AndrrffCRUD.Controller
{
    public static class DeleteData
    {
        [FunctionName("RunDeleteData")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Function, "delete", Route = null)] HttpRequest req,
            ILogger log)
        {
            log.LogInformation("HTTP trigger function processed a request.");

            string  requestBody = await new StreamReader(req.Body).ReadToEndAsync();
            dynamic data        = JsonConvert.DeserializeObject(requestBody);
            var     client      = new HttpClient();

            string queryConsult = "/api/delete/" + data?._id;

            HttpResponseMessage responseMessage = GetConnection.Run(log).Result;

            if (responseMessage.IsSuccessStatusCode)
            {
                var response = new HttpResponseMessage();

                response = await client.DeleteAsync(Environment.GetEnvironmentVariable("URL") + queryConsult);

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
