using BlazorApp.Server.Models;
using BlazorApp.Shared;
using MongoDB.Driver;
using System.Collections.Generic;
using System.Linq;

namespace BlazorApp.Server.Services
{
    public class IndividualChatService
    {
        private readonly IMongoCollection<IndividualChat> _individualChatCollection;
        private readonly UserService _userService; // Add UserService


        public IndividualChatService(IMongoDatabase database, UserService userService)
        {
            _individualChatCollection = database.GetCollection<IndividualChat>("IndividualChats");
            _userService = userService; // Initialize UserService

        }

        // Method to start a new individual chat between two users
        public int StartIndividualChat(string user1Id, string user2Id)
        {
            // Create a new individual chat object
            var chat = new IndividualChat
            {
                User1Id = user1Id,
                User2Id = user2Id,
                Messages = new List<Message>() // Initialize messages list
            };

            // Insert the new individual chat into the database
            _individualChatCollection.InsertOne(chat);

            // Return the ID of the newly created chat
            return chat.Id;
        }

        public List<IndividualChat> GetAllIndividualChats()
        {
            return _individualChatCollection.Find(_ => true).ToList();
        }

        public IndividualChat GetIndividualChatById(int id)
        {
            return _individualChatCollection.Find(chat => chat.Id == id).FirstOrDefault();
        }

        public IndividualChat GetIndividualChatByUsernames(string user1Username, string user2Username)
        {
            return _individualChatCollection.Find(chat =>
                ((_userService.GetUserByUsername(chat.User1Id)).UserName == user1Username && 
                (_userService.GetUserByUsername(chat.User2Id)).UserName == user2Username) ||
                ((_userService.GetUserByUsername(chat.User1Id)).UserName == user2Username &&
               (_userService.GetUserByUsername(chat.User2Id)).UserName == user1Username))
                .FirstOrDefault();
        }

        public List<Message> GetMessagesForIndividualChat(int chatId)
        {
            var chat = _individualChatCollection.Find(c => c.Id == chatId).FirstOrDefault();
            return (List<Message>)(chat?.Messages ?? new List<Message>());
        }

        public void SendMessageToIndividualChat(int chatId, MessageModel message)
        {
            // Find the individual chat by ID
            var chat = _individualChatCollection.Find(c => c.Id == chatId).FirstOrDefault();

            if (chat == null)
            {
                throw new ArgumentException($"Individual chat with ID {chatId} not found.");
            }

            // Create a new message
            var newMessage = new Message
            {
                Content = message.Content,
                Timestamp = DateTimeOffset.UtcNow, // Use UTC time
                SenderId = message.SenderId, // Use sender's ID instead of name
                /*SenderProfileImageUrl = message.SenderProfileImageUrl,
                ImageUrl = message.ImageUrl*/
            };

            // Add the message to the chat's message list
            chat.Messages ??= new List<Message>();
            chat.Messages.Add(newMessage);

            // Update the individual chat with the new message
            var filter = Builders<IndividualChat>.Filter.Eq(c => c.Id, chatId);
            var update = Builders<IndividualChat>.Update.Set(c => c.Messages, chat.Messages);
            _individualChatCollection.UpdateOne(filter, update);
        }

        public void InsertIndividualChat(IndividualChat chat)
        {
            _individualChatCollection.InsertOne(chat);
        }

        public void UpdateIndividualChat(IndividualChat chat)
        {
            _individualChatCollection.ReplaceOne(c => c.Id == chat.Id, chat);
        }

        public void DeleteIndividualChat(int id)
        {
            _individualChatCollection.DeleteOne(chat => chat.Id == id);
        }
    }
}
