using Entities;
using Moq;
using NUnit.Framework;
using Repositories;
using Services;
using System;

namespace ServicesTests
{
    [TestFixture]
    public class TestCarService
    {
        const string dbName = "POC_CSTB";
        const string collectionName = "Cars";

        CarService TestedService;
        Mock<ICarRepository> mockRepository;

        [SetUp]
        public void Initialize()
        {
            mockRepository = new Mock<ICarRepository>();
            TestedService = new CarService();
            TestedService.Repository = mockRepository.Object;

        }

        [TearDown]
        public void Cleanup()
        {
            mockRepository = null;
            TestedService = null;
        }

        [Test]
        public void CarService_TestSave_InsertMode()
        {
            // Arrange
            Car carForInstert = new Car
            {
                Automatic = true,
                Brand = "Peugeot",
                Color = "Red",
                Id = Guid.NewGuid().ToString(),
                MaxSpeed = 160,
                Passengers = 5
            };

            // Act
            TestedService.Save(carForInstert);

            // Assert
            mockRepository.Verify(repo => repo.Find(carForInstert.Id));
            mockRepository.Verify(repo => repo.Insert(carForInstert));
            mockRepository.Verify(repo => repo.Update(carForInstert), Times.Never);
        }

        [Test]
        public void CarService_TestSave_UpdateMode()
        {
            // Arrange
            Car carForUpdate = new Car
            {
                Automatic = true,
                Brand = "Peugeot",
                Color = "Red",
                Id = Guid.NewGuid().ToString(),
                MaxSpeed = 160,
                Passengers = 5
            };

            mockRepository.Setup(repo => repo.Find(carForUpdate.Id)).Returns(carForUpdate);


            // Act
            TestedService.Save(carForUpdate);

            // Assert
            mockRepository.Verify(repo => repo.Find(carForUpdate.Id));
            mockRepository.Verify(repo => repo.Insert(carForUpdate), Times.Never);
            mockRepository.Verify(repo => repo.Update(carForUpdate));
        }

        [Test]
        public void CarService_TestFind()
        {
            // Arrange
            Car carToFind = new Car
            {
                Automatic = true,
                Brand = "Peugeot",
                Color = "Red",
                Id = Guid.NewGuid().ToString(),
                MaxSpeed = 160,
                Passengers = 5
            };

            mockRepository.Setup(repo => repo.Find(carToFind.Id)).Returns(carToFind);

            // Act
            Car result = TestedService.Find(carToFind.Id);

            // Assert
            mockRepository.Verify(repo => repo.Find(carToFind.Id));
            Assert.AreEqual(carToFind, result);
        }

        [Test]
        public void CarService_TestDelete()
        {
            // Arrange
            Car carToDelete = new Car
            {
                Automatic = true,
                Brand = "Peugeot",
                Color = "Red",
                Id = Guid.NewGuid().ToString(),
                MaxSpeed = 160,
                Passengers = 5
            };

            // Act
            TestedService.Delete(carToDelete);

            // Assert
            mockRepository.Verify(repo => repo.Delete(carToDelete));
        }
    }
}
