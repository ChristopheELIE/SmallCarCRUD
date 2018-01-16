using Entities;
using MongoDB.Bson;
using MongoDB.Driver;
using System;

namespace UtilsTests
{
    public static class Utils
    {
        public static long Count(string dbName, string collectionName)
        {
            MongoClient client = new MongoClient();
            IMongoDatabase db = client.GetDatabase(dbName);
            IMongoCollection<Car> collection = db.GetCollection<Car>(collectionName);

            return collection.Count(new BsonDocument());
        }

        public static Car Insert(string dbName, string collectionName)
        {
            Car car = new Car
            {
                Automatic = false,
                Brand = "UtilsInsertBrand",
                Color = "UtilsInsertColor",
                Id = Guid.NewGuid().ToString(),
                MaxSpeed = 111,
                Passengers = 111
            };

            MongoClient client = new MongoClient();
            IMongoDatabase db = client.GetDatabase(dbName);
            IMongoCollection<Car> collection = db.GetCollection<Car>(collectionName);
            collection.InsertOne(car);
            return car;
        }

        public static Car Get(string dbName, string collectionName, string carId)
        {
            MongoClient client = new MongoClient();
            IMongoDatabase db = client.GetDatabase("POC_CSTB");
            IMongoCollection<Car> collection = db.GetCollection<Car>("Cars");

            return collection.Find(x => x.Id == carId).Limit(1).First();
        }
    }
}
