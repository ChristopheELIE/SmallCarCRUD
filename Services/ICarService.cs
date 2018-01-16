using Entities;

namespace Services
{
    public interface ICarService
    {
        Car Save(Car car);

        Car Find(string carId);

        void Delete(Car car);
    }
}
