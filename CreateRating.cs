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
            bool invalidinput = false;
            string errorString = "";
            string getProductURL = "https://serverlessohproduct.trafficmanager.net/api/GetProduct?productId=";
            string getUserURL = "https://serverlessohuser.trafficmanager.net/api/GetUser?userId=";
            log.LogInformation("C# HTTP trigger function processed a request.");
            string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
            ProductRating rating = JsonConvert.DeserializeObject<ProductRating>(requestBody);
            HttpWebResponse queryResult;
            var webReq = HttpWebRequest.CreateHttp(getProductURL + rating.ProductId);

            try
            {
                queryResult = (HttpWebResponse)webReq.GetResponse();
                if (queryResult.StatusCode != HttpStatusCode.OK)
                {
                    errorString += $"Invalid product {rating.ProductId}\n";
                    log.LogInformation(errorString);
                }
            }
            catch
            {
                errorString += $"Invalid product {rating.ProductId}\n";
                log.LogInformation(errorString);
                invalidinput = true;
            }

            webReq = HttpWebRequest.CreateHttp(getUserURL + rating.UserId);

            try
            {
                queryResult = (HttpWebResponse)webReq.GetResponse();
                if (queryResult.StatusCode != HttpStatusCode.OK)
                {
                    errorString += $"Invalid User {rating.UserId}\n";
                    log.LogInformation(errorString);
                }
            }
            catch
            {
                errorString += $"Invalid User {rating.UserId}\n";
                log.LogInformation(errorString);
                invalidinput = true;
            }


            //var test = new StreamReader(queryResult.GetResponseStream()).ReadToEnd();

            if (rating.Rating < 0 || rating.Rating > 5)
            {
                errorString += $"Invalid product rating {rating.Rating}\n";
                log.LogInformation(errorString);
                invalidinput = true;
            }

            if (!invalidinput)
            {
                return new OkObjectResult($"Rating submitted");
            }
            else
            {
                return new BadRequestObjectResult(errorString);
            }


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