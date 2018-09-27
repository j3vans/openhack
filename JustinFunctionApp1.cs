using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Azure.WebJobs.Host;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace BFYOC.GetProduct
{
    public static class JustinFunctionApp1
    {
        [FunctionName("JustinFunctionApp1")]
        public static async Task<IActionResult> Run([HttpTrigger(AuthorizationLevel.Anonymous, "get", "post", Route = null)] HttpRequest req, ILogger log)
        {
            log.LogInformation("C# HTTP trigger function processed a request.");

            string productId = req.Query["productId"];

            // string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
            // dynamic data = JsonConvert.DeserializeObject(requestBody);
            // productId = productId ?? data?.productId;

            log.LogInformation($"User passed");

            if (!string.IsNullOrEmpty(productId))
            {
                log.LogInformation($"User POST data: {productId}");
                return (ActionResult)new OkObjectResult($"The product productId for your product id {productId} is Starfruit Explosion");
            }
            else
            {
                log.LogError("User did not pass a valid productId");
                return new BadRequestObjectResult("Please pass a productId on the query string");
            }
        }
    }
}