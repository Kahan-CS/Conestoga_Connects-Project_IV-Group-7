using BlazorApp.Server.Models;
using MongoDB.Driver;
using System.Collections.Generic;
using System.Linq;

namespace BlazorApp.Server.Services
{
    public class UserService
    {
        private readonly IMongoCollection<ApplicationUser> _userCollection;

        public void DeleteUserByUsername(string username)
        {
            _userCollection.DeleteOne(user => user.UserName == username);
        }
        public UserService(IMongoDatabase database)
        {
            _userCollection = database.GetCollection<ApplicationUser>("Users");
        }

        public ApplicationUser getUserByUsernameAndPassword(string username, string password)
        {
            return _userCollection.Find(user => user.UserName == username && user.password == password).FirstOrDefault();
        }

        public List<ApplicationUser> GetAllUsers()
        {
            return _userCollection.Find(_ => true).ToList();
        }

        public bool IsUsernameTaken(string username)
        {
            return _userCollection.Find(user => user.UserName == username).Any();
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

