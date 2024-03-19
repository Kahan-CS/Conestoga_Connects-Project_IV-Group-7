using BlazorApp.Server.Models;
using MongoDB.Driver;
using System.Collections.Generic;
using System.Linq;

namespace BlazorApp.Server.Services
{
    public class UserService
    {
        private readonly IMongoCollection<ApplicationUser> _userCollection;

        public UserService(IMongoDatabase database)
        {
            _userCollection = database.GetCollection<ApplicationUser>("Users");
        }

        public List<ApplicationUser> GetAllUsers()
        {
            return _userCollection.Find(_ => true).ToList();
        }

        public ApplicationUser GetUserById(string id)
        {
            return _userCollection.Find(user => user.Id == id).FirstOrDefault();
        }

        public void InsertUser(ApplicationUser user)
        {
            _userCollection.InsertOne(user);
        }

        public void UpdateUser(ApplicationUser user)
        {
            _userCollection.ReplaceOne(u => u.Id == user.Id, user);
        }

        public void DeleteUser(string id)
        {
            _userCollection.DeleteOne(user => user.Id == id);
        }
    }
}

