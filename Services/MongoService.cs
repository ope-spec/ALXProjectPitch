using Microsoft.Extensions.Options;
using MongoDB.Driver;
using ProjectPitch4.Models;
using System;

namespace ProjectPitch4.Services
{
    public class MongoService
    {
        public readonly IMongoCollection<Region> _regions;
        public readonly IMongoCollection<ProductDescription> _productDescriptions;
        
        public MongoService(IOptions<AppConfig> config)
        {
            var client = new MongoClient(config.Value.MongoConnectionString);
            var database = client.GetDatabase(config.Value.MongoDbCollectionName);
            _regions = database.GetCollection<Region>(nameof(Region));
            _productDescriptions = database.GetCollection<ProductDescription>("productDescription");
     
        }
    }
}
