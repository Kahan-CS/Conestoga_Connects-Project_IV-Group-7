using NUnit.Framework;
using BlazorApp.Server.Models;
using BlazorApp.Server.Services;
using MongoDB.Driver;
using Microsoft.Extensions.Configuration;
using System;

namespace NUnitTests_Server.ServicesTests
{
    [TestFixture]
    public class MessageServiceTests
    {
        private IMongoDatabase _database;
        private MessageService _messageService;

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

            // Initialize the MessageService with the test database
            _messageService = new MessageService(_database);

            // Clear the Messages collection before each test
            _database.GetCollection<Message>("Messages").DeleteMany(_ => true);
        }

        [Test]
        public void InsertMessage_AddsMessageToDatabase()
        {
            // Arrange: Create a new message
            var message = new Message { Content = "New Message" };

            // Get the current local time before inserting the message
            var currentTime = DateTimeOffset.Now;

            // Act: Insert the message
            _messageService.InsertMessage(message);

            // Assert: Check if the message is added to the database
            var retrievedMessage = _database.GetCollection<Message>("Messages").Find(m => m.Content == "New Message").FirstOrDefault();
            Assert.IsNotNull(retrievedMessage);
            Assert.AreEqual(message.Content, retrievedMessage.Content);

            // Compare the timestamps with a tolerance of 1 second
            Assert.That(retrievedMessage.Timestamp, Is.EqualTo(currentTime).Within(TimeSpan.FromSeconds(1)));
        }



        [Test]
        public void UpdateMessage_UpdatesMessageInDatabase()
        {
            // Arrange: Insert a dummy message
            var message = new Message { Content = "Original Message" };
            _messageService.InsertMessage(message);

            // Get the current local time before updating the message
            var currentTime = DateTimeOffset.Now;

            // Modify the message
            message.Content = "Updated Message";

            // Act: Update the message
            _messageService.UpdateMessage(message);

            // Assert: Check if the message is updated in the database
            var updatedMessage = _database.GetCollection<Message>("Messages").Find(m => m.Content == "Updated Message").FirstOrDefault();
            Assert.IsNotNull(updatedMessage);
            Assert.AreEqual(message.Content, updatedMessage.Content);

            // Compare the timestamps with a tolerance of 1 second
            Assert.That(updatedMessage.Timestamp, Is.EqualTo(currentTime).Within(TimeSpan.FromSeconds(1)));
        }


        [Test]
        public void GetAllMessages_ReturnsAllMessages()
        {
            // Arrange: Insert dummy messages
            
            // Note: Hardcoded _id values are used for testing purposes. Do not replicate for functionalities.
            var message1 = new Message { Id = 1, Content = "Message 1", Timestamp = DateTimeOffset.Now };
            var message2 = new Message { Id = 2, Content = "Message 2", Timestamp = DateTimeOffset.Now.AddSeconds(1) }; // Ensure unique timestamps

            // Manually insert messages with specific _id values
            _messageService.InsertMessage(message1);
            _messageService.InsertMessage(message2);

            // Act: Get all messages
            var messages = _messageService.GetAllMessages();

            // Assert: Check if all messages are returned
            Assert.AreEqual(2, messages.Count);
            Assert.IsTrue(messages.Exists(m => m.Content == "Message 1"));
            Assert.IsTrue(messages.Exists(m => m.Content == "Message 2"));
        }



        [Test]
        public void GetMessageById_ReturnsMessage()
        {
            // Arrange: Insert a dummy message
            var currentTime = DateTimeOffset.Now;
            var message = new Message { Content = "Test Message", Timestamp = currentTime };
            _messageService.InsertMessage(message);

            // Act: Get the message by ID
            var retrievedMessage = _messageService.GetMessageById(message.Id);

            // Assert: Check if the retrieved message matches the inserted message
            Assert.IsNotNull(retrievedMessage);
            Assert.AreEqual(message.Content, retrievedMessage.Content);

            // Compare the timestamps with a tolerance of 1 second
            Assert.That(retrievedMessage.Timestamp, Is.EqualTo(currentTime).Within(TimeSpan.FromSeconds(1)));
        }

        [Test]
        public void DeleteMessage_RemovesMessageFromDatabase()
        {
            // Arrange: Insert a dummy message
            var message = new Message { Content = "Test Message", Timestamp = DateTime.Now };
            _messageService.InsertMessage(message);

            // Act: Delete the message
            _messageService.DeleteMessage(message.Id);

            // Assert: Check if the message is removed from the database
            var deletedMessage = _database.GetCollection<Message>("Messages").Find(m => m.Content == "Test Message").FirstOrDefault();
            Assert.IsNull(deletedMessage);
        }
    }
}
