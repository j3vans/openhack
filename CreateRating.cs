using System;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Azure.WebJobs.Host;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
namespace OpenHackTeam16
{
    public static class CreateRating
    {
        [FunctionName("CreateRating")]
        public static async Task<IActionResult> Run([HttpTrigger(AuthorizationLevel.Anonymous, "get", "post", Route = null)] HttpRequest req, ILogger log)
        {
            log.LogInformation("C# HTTP trigger function processed a request.");
            string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
            ProductRating rating = JsonConvert.DeserializeObject<ProductRating>(requestBody);

            // HttpWebResponse queryResult = WebRequest.Create($"https://serverlessohproduct.trafficmanager.net/api/GetProduct?productId={rating.ProductId}").GetRes˚çponse();
            var client = new HttpClient();
            HttpResponseMessage queryResult = await client.GetAsync($"https://serverlessohproduct.trafficmanager.net/api/GetProduct?productId={rating.ProductId}");

            if (queryResult.IsSuccessStatusCode)
            {
                log.LogInformation($"Invalid product {rating.ProductId}");
                new BadRequestObjectResult($"Invalid product {rating.ProductId}");
            }

            if (rating.Rating < 0 || rating.Rating > 5)
            {
                log.LogInformation($"Invalid product rating {rating.Rating}");
                // new BadRequestObjectResult($"Invalid product rating {rating.Rating}");
            }
            return new BadRequestObjectResult($"Invalid product {rating.ProductId}");

            // string name = req.Query["name"];

            // dynamic data = JsonConvert.DeserializeObject(requestBody);
            // name = name ?? data?.name;

            // return name != null ?
            //     (ActionResult)new OkObjectResult($"Hello, {name}") :
            //     new BadRequestObjectResult("Please pass a name on the query string or in the request body");
        }
    }
    public class ProductRating
    {
        public string UserId { get; set; }
        public string ProductId { get; set; }
        public string LocationName { get; set; }
        public int Rating { get; set; }
        public string UserNotes { get; set; }
        public Guid Id { get; set; }
        public DateTime TimeStamp { get; set; }
    }
}