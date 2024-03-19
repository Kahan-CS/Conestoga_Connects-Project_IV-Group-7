using BlazorApp.Server.Models;
using MongoDB.Driver;
using System.Collections.Generic;
using System.Linq;

namespace BlazorApp.Server.Services
{
    public class IndividualChatService
    {
        private readonly IMongoCollection<IndividualChat> _individualChatCollection;

        public IndividualChatService(IMongoDatabase database)
        {
            _individualChatCollection = database.GetCollection<IndividualChat>("IndividualChats");
        }

        public List<IndividualChat> GetAllIndividualChats()
        {
            return _individualChatCollection.Find(_ => true).ToList();
        }

        public IndividualChat GetIndividualChatById(int id)
        {
            return _individualChatCollection.Find(chat => chat.Id == id).FirstOrDefault();
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
