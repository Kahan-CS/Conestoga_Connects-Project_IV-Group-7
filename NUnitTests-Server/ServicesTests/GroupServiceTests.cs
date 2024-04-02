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

            // Act: Insert the group
            _groupService.InsertGroup(group);

            // Assert: Check if the group is added to the database
            var retrievedGroup = _database.GetCollection<Group>("Groups").Find(g => g.Name == "Test Group").FirstOrDefault();
            Assert.IsNotNull(retrievedGroup);
            Assert.AreEqual(group.Name, retrievedGroup.Name);
        }

        [Test]
        public void UpdateGroup_UpdatesGroupInDatabase()
        {
            // Arrange: Insert a dummy group
            var group = new Group { Name = "Original Group" };
            _groupService.InsertGroup(group);

            // Modify the group
            group.Name = "Updated Group";

            // Act: Update the group
            _groupService.UpdateGroup(group);

            // Assert: Check if the group is updated in the database
            var updatedGroup = _database.GetCollection<Group>("Groups").Find(g => g.Name == "Updated Group").FirstOrDefault();
            Assert.IsNotNull(updatedGroup);
            Assert.AreEqual(group.Name, updatedGroup.Name);
        }

        [Test]
        public void GetAllGroups_ReturnsAllGroups()
        {
            // Arrange: Insert dummy groups
            var group1 = new Group { Id = 1, Name = "Group 1" };
            var group2 = new Group { Id =2, Name = "Group 2" };
            _groupService.InsertGroup(group1);
            _groupService.InsertGroup(group2);

            // Act: Get all groups
            var groups = _groupService.GetAllGroups();

            // Assert: Check if all groups are returned
            Assert.AreEqual(2, groups.Count);
            Assert.IsTrue(groups.Exists(g => g.Name == "Group 1"));
            Assert.IsTrue(groups.Exists(g => g.Name == "Group 2"));
        }

        [Test]
        public void GetGroupById_ReturnsGroup()
        {
            // Arrange: Insert a dummy group
            var group = new Group { Name = "Test Group" };
            _groupService.InsertGroup(group);

            // Act: Get the group by ID
            var retrievedGroup = _groupService.GetGroupById(group.Id);

            // Assert: Check if the retrieved group matches the inserted group
            Assert.IsNotNull(retrievedGroup);
            Assert.AreEqual(group.Name, retrievedGroup.Name);
        }

        [Test]
        public void DeleteGroup_RemovesGroupFromDatabase()
        {
            // Arrange: Insert a dummy group
            var group = new Group { Name = "Test Group" };
            _groupService.InsertGroup(group);

            // Act: Delete the group
            _groupService.DeleteGroup(group.Id);

            // Assert: Check if the group is removed from the database
            var deletedGroup = _database.GetCollection<Group>("Groups").Find(g => g.Name == "Test Group").FirstOrDefault();
            Assert.IsNull(deletedGroup);
        }

        [TearDown]
        public void Teardown()
        {
            // Clean up the database after each test
            _database.DropCollection("Groups");
        }
    }
}
