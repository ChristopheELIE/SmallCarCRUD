using Entities;
using NUnit.Framework;
using Repositories;
using System;
using UtilsTests;

namespace RepositoriesTests
{
    [TestFixture]
    public class TestCarRepository
    {
        const string dbName = "POC_CSTB";
        const string collectionName = "Cars";

        CarRepository TestedRepository;

        [SetUp]
        public void Initialize()
        {
            TestedRepository = new CarRepository();
        }

        [TearDown]
        public void Cleanup()
        {
            TestedRepository = null;
        }

        [Test]
        public void CarRepository_TestInsert()
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

            long before = Utils.Count(dbName, collectionName);

            // Act
            TestedRepository.Insert(carForInstert);

            // Assert
            long after = Utils.Count(dbName, collectionName);
            Assert.AreEqual(before + 1, after);
        }

        [Test]
        public void CarRepository_TestUpdate()
        {
            // Arrange
            Car inserted = Utils.Insert(dbName, collectionName);
            inserted.Brand = "Updated";
            inserted.Color = "Updated";
            inserted.Automatic = true;
            inserted.MaxSpeed = 222;
            inserted.Passengers = 222;

            // Act
            TestedRepository.Update(inserted);

            // Assert
            Car updated = Utils.Get(dbName, collectionName, inserted.Id);
            Assert.AreEqual(inserted.Id, updated.Id);
            Assert.AreEqual(inserted.Brand, updated.Brand);
            Assert.AreEqual(inserted.Color, updated.Color);
            Assert.AreEqual(inserted.Automatic, updated.Automatic);
            Assert.AreEqual(inserted.MaxSpeed, updated.MaxSpeed);
            Assert.AreEqual(inserted.Passengers, updated.Passengers);
        }

        [Test]
        public void CarRepository_TestFind()
        {
            // Arrange
            Car inserted = Utils.Insert(dbName, collectionName);

            // Act
            Car found = TestedRepository.Find(inserted.Id);

            // Assert
            Car updated = Utils.Get(dbName, collectionName, inserted.Id);
            Assert.AreEqual(inserted.Id, found.Id);
            Assert.AreEqual(inserted.Brand, found.Brand);
            Assert.AreEqual(inserted.Color, found.Color);
            Assert.AreEqual(inserted.Automatic, found.Automatic);
            Assert.AreEqual(inserted.MaxSpeed, found.MaxSpeed);
            Assert.AreEqual(inserted.Passengers, found.Passengers);
        }

        [Test]
        public void CarRepository_TestDelete()
        {
            // Arrange
            Car inserted = Utils.Insert(dbName, collectionName);
            long before = Utils.Count(dbName, collectionName);

            // Act
            TestedRepository.Delete(inserted);

            // Assert
            long after = Utils.Count(dbName, collectionName);
            Assert.AreEqual(before - 1, after);
        }
    }
}
