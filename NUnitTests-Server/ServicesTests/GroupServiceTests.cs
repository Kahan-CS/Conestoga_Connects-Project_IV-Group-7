using NUnit.Framework;
using BlazorApp.Server.Models;
using BlazorApp.Server.Services;
using MongoDB.Driver;
using Microsoft.Extensions.Configuration;
using System;

namespace NUnitTests_Server.ServicesTests
{
    [TestFixture]
    public class GroupServiceTests
    {
        private IMongoDatabase _database;
        private GroupService _groupService;

        [SetUp]
        public void Setup()
        {
            // Initialize configuration
            var configuration = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json")
                .Build();

            // Read connection string from configuration
            var connectionString = configuration.GetConnectionString("MongoDB");

            // Initialize the test database
            var client = new MongoClient(connectionString);
            _database = client.GetDatabase("unit_test_database");

            // Initialize the GroupService with the test database
            _groupService = new GroupService(_database);

            // Clear the Groups collection before each test
            _database.GetCollection<Group>("Groups").DeleteMany(_ => true);
        }

        [Test]
        public void InsertGroup_AddsGroupToDatabase()
        {
            // Arrange: Create a new group
            var group = new Group { Name = "Test Group" };

            // Act: Create the group
            _groupService.InsertGroup(group);

            // Assert: Check if the group is added to the database
            var retrievedGroup = _database.GetCollection<Group>("Groups").Find(g => g.Name == "Test Group").FirstOrDefault();
            Assert.IsNotNull(retrievedGroup);
            Assert.AreEqual(group.Name, retrievedGroup.Name);
        }

        [Test]
        public void GetGroupById_ReturnsGroup()
        {
            // Arrange: Create a new group
            var group = new Group { Name = "Test Group" };
            _groupService.InsertGroup(group);

            // Act: Get the group by ID
            var retrievedGroup = _groupService.GetGroupById(group.Id);

            // Assert: Check if the retrieved group matches the inserted group
            Assert.IsNotNull(retrievedGroup);
            Assert.AreEqual(group.Name, retrievedGroup.Name);
        }

        // Add more test cases as needed for other methods of the GroupService

        [TearDown]
        public void Teardown()
        {
            // Clean up after each test if necessary
            _database.GetCollection<Group>("Groups").DeleteMany(_ => true);
        }
    }
}
