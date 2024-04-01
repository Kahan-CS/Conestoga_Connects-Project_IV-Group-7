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
    public class IndividualChatServiceTests
    {
        private IMongoDatabase _database;
        private IndividualChatService _individualChatService;

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

            // Initialize IndividualChatService with the test database
            _individualChatService = new IndividualChatService(_database);

            // Clear the IndividualChats collection before each test
            _database.GetCollection<IndividualChat>("IndividualChats").DeleteMany(_ => true);
        }

        [Test]
        public void GetAllIndividualChats_ReturnsAllChats()
        {
            // Arrange: Insert dummy chats
            var chat1 = new IndividualChat { Id = 1, User1Id = "user1", User2Id = "user2", Messages = new List<Message>() };
            var chat2 = new IndividualChat { Id = 2, User1Id = "user3", User2Id = "user4", Messages = new List<Message>() };
            _individualChatService.InsertIndividualChat(chat1);
            _individualChatService.InsertIndividualChat(chat2);

            // Act: Get all individual chats
            var chats = _individualChatService.GetAllIndividualChats();

            // Assert: Check if all chats are returned
            Assert.AreEqual(2, chats.Count);
            Assert.IsTrue(chats.Any(c => c.Id == 1));
            Assert.IsTrue(chats.Any(c => c.Id == 2));
        }

        [Test]
        public void GetIndividualChatById_ReturnsChat()
        {
            // Arrange: Insert a dummy chat
            var chat = new IndividualChat { Id = 1, User1Id = "user1", User2Id = "user2", Messages = new List<Message>() };
            _individualChatService.InsertIndividualChat(chat);

            // Act: Get the chat by ID
            var retrievedChat = _individualChatService.GetIndividualChatById(1);

            // Assert: Check if the retrieved chat matches the inserted chat
            Assert.IsNotNull(retrievedChat);
            Assert.AreEqual(chat.User1Id, retrievedChat.User1Id);
            Assert.AreEqual(chat.User2Id, retrievedChat.User2Id);
        }

        [Test]
        public void InsertIndividualChat_AddsChatToDatabase()
        {
            // Arrange: Create a new chat
            var chat = new IndividualChat { Id = 1, User1Id = "user1", User2Id = "user2", Messages = new List<Message>() };

            // Act: Insert the chat
            _individualChatService.InsertIndividualChat(chat);

            // Assert: Check if the chat is added to the database
            var retrievedChat = _database.GetCollection<IndividualChat>("IndividualChats").Find(c => c.Id == 1).FirstOrDefault();
            Assert.IsNotNull(retrievedChat);
            Assert.AreEqual(chat.User1Id, retrievedChat.User1Id);
            Assert.AreEqual(chat.User2Id, retrievedChat.User2Id);
        }

        [Test]
        public void UpdateIndividualChat_UpdatesChatInDatabase()
        {
            // Arrange: Insert a dummy chat
            var chat = new IndividualChat { Id = 1, User1Id = "user1", User2Id = "user2", Messages = new List<Message>() };
            _individualChatService.InsertIndividualChat(chat);

            // Modify the chat
            chat.User1Id = "updatedUser1";
            chat.User2Id = "updatedUser2";

            // Act: Update the chat
            _individualChatService.UpdateIndividualChat(chat);

            // Assert: Check if the chat is updated in the database
            var updatedChat = _database.GetCollection<IndividualChat>("IndividualChats").Find(c => c.Id == 1).FirstOrDefault();
            Assert.IsNotNull(updatedChat);
            Assert.AreEqual("updatedUser1", updatedChat.User1Id);
            Assert.AreEqual("updatedUser2", updatedChat.User2Id);
        }

        [Test]
        public void DeleteIndividualChat_RemovesChatFromDatabase()
        {
            // Arrange: Insert a dummy chat
            var chat = new IndividualChat { Id = 1, User1Id = "user1", User2Id = "user2", Messages = new List<Message>() };
            _individualChatService.InsertIndividualChat(chat);

            // Act: Delete the chat
            _individualChatService.DeleteIndividualChat(1);

            // Assert: Check if the chat is removed from the database
            var deletedChat = _database.GetCollection<IndividualChat>("IndividualChats").Find(c => c.Id == 1).FirstOrDefault();
            Assert.IsNull(deletedChat);
        }
    }
}
