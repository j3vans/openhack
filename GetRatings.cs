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
    public static class GetRatings
    {
        [FunctionName("GetRatings")]
        public static async Task<IActionResult> Run([HttpTrigger(AuthorizationLevel.Anonymous, "get", "post", Route = null)] HttpRequest req, ILogger log)
        {
            string errorString = "";
            bool invalidInput = false;
            string userID = req.Query["userID"];
            string getUserURL = "https://serverlessohuser.trafficmanager.net/api/GetUser?userId=";
            log.LogInformation("C# HTTP trigger function processed a request.");


            HttpWebResponse queryResult;
            var webReq = HttpWebRequest.CreateHttp(getUserURL + userID);

            try
            {
                queryResult = (HttpWebResponse)webReq.GetResponse();
                if (queryResult.StatusCode != HttpStatusCode.OK)
                {
                    errorString += $"Invalid product {userID}\n";
                    log.LogInformation(errorString);
                }
            }
            catch
            {
                errorString += $"Invalid product {userID}\n";
                log.LogInformation(errorString);
                invalidInput = true;
            }
            // string name = req.Query["ratingId"];
            // name = name ?? data?.name;

            if (!invalidInput)
            {
                return new OkObjectResult($"Ratings retrieved");
            }
            else
            {
                return new BadRequestObjectResult(errorString);
            }





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