using NUnit.Framework;
using BlazorApp.Server.Models;
using BlazorApp.Server.Services;
using MongoDB.Driver;
using Microsoft.Extensions.Configuration;

using System;
using System.Collections.Generic;
using System.Linq;

namespace NUnitTests_Server.ServicesTests
{
    [TestFixture]
    public class UserServiceTests
    {
        private IMongoDatabase _database;
        private UserService _userService;

        [SetUp]
        public void Setup()
        {
            // Initialize configuration
            var configuration = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json")
                .Build();

            // Read connection string from configuration
            var connectionString = configuration.GetConnectionString("MongoDB");

            // Initialize database connection
            var client = new MongoClient(connectionString);
            _database = client.GetDatabase("unit_test_database");

            // Initialize UserService with the test database
            _userService = new UserService(_database);

            // Clear the Users collection before each test
            _database.GetCollection<ApplicationUser>("Users").DeleteMany(_ => true);
        }

        [Test]
        public void GetAllUsers_ReturnsAllUsers()
        {
            // Arrange: Insert dummy users
            var user1 = new ApplicationUser { UserName = "user1", Email = "user1@example.com" };
            var user2 = new ApplicationUser { UserName = "user2", Email = "user2@example.com" };
            _userService.InsertUser(user1);
            _userService.InsertUser(user2);

            // Act: Get all users
            var users = _userService.GetAllUsers();

            // Assert: Check if all users are returned
            Assert.AreEqual(2, users.Count);
            Assert.IsTrue(users.Any(u => u.UserName == "user1"));
            Assert.IsTrue(users.Any(u => u.UserName == "user2"));
        }

        [Test]
        public void GetUserById_ReturnsUser()
        {
            // Arrange: Insert a dummy user
            var user = new ApplicationUser { UserName = "user1", Email = "user1@example.com" };
            _userService.InsertUser(user);

            // Act: Get the user by ID
            var retrievedUser = _userService.GetUserById(user.Id);

            // Assert: Check if the retrieved user matches the inserted user
            Assert.IsNotNull(retrievedUser);
            Assert.AreEqual(user.UserName, retrievedUser.UserName);
            Assert.AreEqual(user.Email, retrievedUser.Email);
        }

        [Test]
        public void InsertUser_AddsUserToDatabase()
        {
            // Arrange: Create a new user
            var user = new ApplicationUser { UserName = "user1", Email = "user1@example.com" };

            // Act: Insert the user
            _userService.InsertUser(user);

            // Assert: Check if the user is added to the database
            var retrievedUser = _database.GetCollection<ApplicationUser>("Users").Find(u => u.UserName == "user1").FirstOrDefault();
            Assert.IsNotNull(retrievedUser);
            Assert.AreEqual(user.Email, retrievedUser.Email);
        }

        [Test]
        public void UpdateUser_UpdatesUserInDatabase()
        {
            // Arrange: Insert a dummy user
            var user = new ApplicationUser { UserName = "user1", Email = "user1@example.com" };
            _userService.InsertUser(user);

            // Modify the user
            user.Email = "updated@example.com";

            // Act: Update the user
            _userService.UpdateUser(user);

            // Assert: Check if the user is updated in the database
            var updatedUser = _database.GetCollection<ApplicationUser>("Users").Find(u => u.UserName == "user1").FirstOrDefault();
            Assert.IsNotNull(updatedUser);
            Assert.AreEqual("updated@example.com", updatedUser.Email);
        }

        [Test]
        public void DeleteUser_RemovesUserFromDatabase()
        {
            // Arrange: Insert a dummy user
            var user = new ApplicationUser { UserName = "user1", Email = "user1@example.com" };
            _userService.InsertUser(user);

            // Act: Delete the user
            _userService.DeleteUser(user.Id);

            // Assert: Check if the user is removed from the database
            var deletedUser = _database.GetCollection<ApplicationUser>("Users").Find(u => u.UserName == "user1").FirstOrDefault();
            Assert.IsNull(deletedUser);
        }
    }
}
