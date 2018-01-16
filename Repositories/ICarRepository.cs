using Entities;

namespace Repositories
{
    public interface ICarRepository
    {
        Car Insert(Car car);

        Car Update(Car car);

        Car Find(string carId);

        void Delete(Car car);
    }
}
