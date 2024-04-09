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

        // CRUD operations for contacts

        public void AddContact(string username, Contact contact)
        {
            // Check if the contact username corresponds to an existing user
            if (!IsUsernameTaken(contact.Username))
            {
                // Handle the case where the contact username does not exist
                throw new Exception($"User with username {contact.Username} does not exist.");
            }

            // Add the contact to the user's contacts list
            var filter = Builders<ApplicationUser>.Filter.Eq(u => u.UserName, username);
            var update = Builders<ApplicationUser>.Update.Push(u => u.Contacts, contact);
            _userCollection.UpdateOne(filter, update);
        }


        public Contact GetContactByUsername(string username, string contactUsername)
        {
            var user = _userCollection.Find(u => u.UserName == username).FirstOrDefault();
            return user?.Contacts.FirstOrDefault(c => c.Username == contactUsername);
        }

        public List<Contact> GetAllContacts(string username)
        {
            var user = _userCollection.Find(u => u.UserName == username).FirstOrDefault();
            return user?.Contacts ?? new List<Contact>();
        }

        public void DeleteContact(string username, string contactUsername)
        {
            var filter = Builders<ApplicationUser>.Filter.Eq(u => u.UserName, username);
            var update = Builders<ApplicationUser>.Update.PullFilter(u => u.Contacts, c => c.Username == contactUsername);
            _userCollection.UpdateOne(filter, update);
        }
    }
}

