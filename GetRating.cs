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
    public static class GetRating
    {
        [FunctionName("GetRating")]
        public static async Task<IActionResult> Run([HttpTrigger(AuthorizationLevel.Anonymous, "get", "post", Route = null)] HttpRequest req, ILogger log)
        {
            string ratingID = req.Query["ratingId"];
            log.LogInformation("C# HTTP trigger function processed a request.");

            // string name = req.Query["ratingId"];
            // name = name ?? data?.name;

            return ratingID != null ?
                (ActionResult)new OkObjectResult($"Hello, {name}") :
                new BadRequestObjectResult("Please pass a name on the query string or in the request body");
        }
    }
    // public class ProductRating
    // {
    //     public string UserId { get; set; }
    //     public string ProductId { get; set; }
    //     public string LocationName { get; set; }
    //     public int Rating { get; set; }
    //     public string UserNotes { get; set; }
    //     public Guid Id { get; set; }
    //     public DateTime TimeStamp { get; set; }
    // }
}