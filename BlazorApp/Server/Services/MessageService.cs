using BlazorApp.Server.Models;
using MongoDB.Driver;
using System.Collections.Generic;
using System.Linq;

namespace BlazorApp.Server.Services
{
    public class MessageService
    {
        private readonly IMongoCollection<Message> _messageCollection;

        public MessageService(IMongoDatabase database)
        {
            _messageCollection = database.GetCollection<Message>("Messages");
        }

        public List<Message> GetAllMessages()
        {
            return _messageCollection.Find(_ => true).ToList();
        }

        public Message GetMessageById(int id)
        {
            return _messageCollection.Find(message => message.Id == id).FirstOrDefault();
        }

        public void InsertMessage(Message message)
        {
            _messageCollection.InsertOne(message);
        }

        public void UpdateMessage(Message message)
        {
            _messageCollection.ReplaceOne(m => m.Id == message.Id, message);
        }

        public void DeleteMessage(int id)
        {
            _messageCollection.DeleteOne(message => message.Id == id);
        }
    }
}
