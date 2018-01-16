using Entities;
using MongoDB.Driver;
using System;

namespace Repositories
{
    public class CarRepository : ICarRepository
    {
        public Car Insert(Car car)
        {
            MongoClient client = new MongoClient();
            IMongoDatabase db = client.GetDatabase("POC_CSTB");
            IMongoCollection<Car> collection = db.GetCollection<Car>("Cars");
            car.Id = Guid.NewGuid().ToString();
            collection.InsertOne(car);
            return car;
        }

        public Car Update(Car car)
        {
            MongoClient client = new MongoClient();
            IMongoDatabase db = client.GetDatabase("POC_CSTB");
            IMongoCollection<Car> collection = db.GetCollection<Car>("Cars");
            var filter = Builders<Car>.Filter.Eq(s => s.Id, car.Id);
            var result = collection.ReplaceOne(filter, car);

            return car;
        }

        public Car Find(string carId)
        {
            MongoClient client = new MongoClient();
            IMongoDatabase db = client.GetDatabase("POC_CSTB");
            IMongoCollection<Car> collection = db.GetCollection<Car>("Cars");

            return collection.Find(x => x.Id == carId).Limit(1).FirstOrDefault();
        }

        public void Delete(Car car)
        {
            MongoClient client = new MongoClient();
            IMongoDatabase db = client.GetDatabase("POC_CSTB");
            IMongoCollection<Car> collection = db.GetCollection<Car>("Cars");

            collection.FindOneAndDelete(x => x.Id == car.Id);
        }
    }
}