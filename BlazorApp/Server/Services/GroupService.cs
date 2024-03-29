using BlazorApp.Server.Models;
using MongoDB.Driver;
using System.Collections.Generic;
using System.Linq;

namespace BlazorApp.Server.Services
{
    public class GroupService
    {
        private readonly IMongoCollection<Group> _groupCollection;

        public GroupService(IMongoDatabase database)
        {
            _groupCollection = database.GetCollection<Group>("Groups");
        }

        public List<Group> GetAllGroups()
        {
            return _groupCollection.Find(_ => true).ToList();
        }

        public Group GetGroupById(int id)
        {
            return _groupCollection.Find(group => group.Id == id).FirstOrDefault();
        }

        public void InsertGroup(Group group)
        {
            _groupCollection.InsertOne(group);
        }

        public void UpdateGroup(Group group)
        {
            _groupCollection.ReplaceOne(g => g.Id == group.Id, group);
        }

        public void DeleteGroup(int id)
        {
            _groupCollection.DeleteOne(group => group.Id == id);
        }
    }
}
