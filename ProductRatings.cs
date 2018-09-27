using System;
using System.Collections.Generic;
using Microsoft.Azure.Documents;
using Microsoft.Azure.Documents.Client;

namespace OpenHackTeam16
{
    public class ProductRatings
    {
        public static readonly string Endpoint = "https://team16.documents.azure.com:443/";
        private static readonly string Key = "3vb5HwBkNzdmwibw7M3BVLoewdT0JZvJVNrzE4fUzxESbr9KhoUzmXnoIQZxAh7UJ3UAfLqGUS8kTrBGSNIqpA==";
        private static readonly string DatabaseId = "ToDoList";
        private static readonly string CollectionId = "Items";

        private DocumentClient client;

        public void Init()
        {

        }

        public Document Get(string id)
        {
            using (client = new DocumentClient(new Uri(Endpoint), Key))
            {
                var collection = UriFactory.CreateDocumentUri(DatabaseId, CollectionId, id);
                Document rating = client.ReadDocumentAsync(collection).Result;
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

        public IEnumerable<ProductRating> All()
        {
            return null;
        }

        public void Delete(Guid id)
        {

        }
    }
}

