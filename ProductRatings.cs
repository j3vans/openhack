using System;
using System.Collections.Generic;
using Microsoft.Azure.Documents;
using Microsoft.Azure.Documents.Client;
using System.Linq;

namespace OpenHackTeam16
{
    public class ProductRatings
    {
        public static readonly string Endpoint = "https://team16.documents.azure.com:443/";
        private static readonly string Key = "3vb5HwBkNzdmwibw7M3BVLoewdT0JZvJVNrzE4fUzxESbr9KhoUzmXnoIQZxAh7UJ3UAfLqGUS8kTrBGSNIqpA==";
        private static readonly string DatabaseId = "Ratings";
        private static readonly string CollectionId = "RatingsCollection";

        private DocumentClient client;

        public void Init()
        {

        }

        public ProductRating Get(string id)
        {
            using (client = new DocumentClient(new Uri(Endpoint), Key))
            {
                var collection = UriFactory.CreateDocumentUri(DatabaseId, CollectionId, id);
                var rating = client.ReadDocumentAsync<ProductRating>(collection).Result;
                return rating;
            }

        }

        public string Add(ProductRating rating)
        {
            using (client = new DocumentClient(new Uri(Endpoint), Key))
            {
                var collection = UriFactory.CreateDocumentCollectionUri(DatabaseId, CollectionId);
                Document doc = client.CreateDocumentAsync(collection, rating).Result;
                return doc.Id;
            }

        }

        public IEnumerable<ProductRating> All(string userid)
        {
            using (client = new DocumentClient(new Uri(Endpoint), Key))
            {
                var query = client.CreateDocumentQuery<ProductRating>(CollectionId).Where(t => t.UserId == userid);
                return query.ToList();
            }
        }

        ///comment
        public void Delete(Guid id)
        {

        }
    }
}

