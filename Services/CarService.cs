using Entities;
using Repositories;

namespace Services
{
    public class CarService : ICarService
    {
        public ICarRepository Repository { get; set; }

        public CarService()
        {
            Repository = new CarRepository();
        }

        public Car Save(Car car)
        {
            Car carFromdb = Find(car.Id);
            if (carFromdb is null)
                return Repository.Insert(car);
            else
                return Repository.Update(car);
        }

        public Car Find(string carId)
        {
            return Repository.Find(carId);
        }

        public void Delete(Car car)
        {
            Repository.Delete(car);
        }
    }
}