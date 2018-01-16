using Entities;
using Services;
using System;

namespace ConsoleUI
{
    static class Program
    {
        static ICarService carService;

        public static void Main()
        {
            carService = new CarService();

            Car car = new Car
            {
                Automatic = true,
                Brand = "Peugeot",
                Color = "Red",
                MaxSpeed = 160,
                Passengers = 5
            };

            car = carService.Save(car);

            car.Brand = "BMW";
            car.Color = "Blue";
            car.MaxSpeed = 250;
            carService.Save(car);

            carService.Delete(car);

            Console.WriteLine("Done");
            Console.ReadLine();
        }
    }
}
