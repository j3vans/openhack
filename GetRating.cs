using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using System;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;


namespace OpenHackTeam16
{
    public static class GetRating
    {
        [FunctionName("GetRating")]
        public static async Task<IActionResult> Run([HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = null)] HttpRequest req, ILogger log)
        {
            log.LogInformation("GetRating function triggered...");

            string ratingId = req.Query["ratingId"];
            Guid parsedRatingId;

            if (string.IsNullOrWhiteSpace(ratingId) || Guid.TryParse(ratingId, out parsedRatingId) == false)
            {
                log.LogInformation($"Invalid rating id {ratingId}");
                return new BadRequestObjectResult($"Invalid rating id {ratingId}");
            }

            // get data from cosmos db
            ProductRating productRating = null;
            try
            {
                productRating = new ProductRatings().Get(ratingId);
                return new OkObjectResult(JsonConvert.SerializeObject(productRating));
            }
            catch
            {
                return new NotFoundObjectResult($"ProductRating not found for rating id {ratingId}");
            }
        }
    }
}