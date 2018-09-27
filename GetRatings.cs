using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace OpenHackTeam16
{
    public static class GetRatings
    {
        [FunctionName("GetRatings")]
        public static async Task<IActionResult> Run([HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = null)] HttpRequest req, ILogger log)
        {
            log.LogInformation("GetRatings function triggered...");

            var userId = req.Query["userId"];
            Guid parsedUserId;

            if (string.IsNullOrWhiteSpace(userId) || Guid.TryParse(userId, out parsedUserId) == false)
            {
                log.LogInformation($"Invalid user id {userId}");
                return new BadRequestObjectResult($"Invalid user id {userId}");
            }

            // TODO: get the ratings for the user from your database
            List<ProductRating> productRatings = new List<ProductRating>();


            return new OkObjectResult(JsonConvert.SerializeObject(productRatings));
        }
    }
}