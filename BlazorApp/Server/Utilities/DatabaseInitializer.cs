using MongoDB.Driver;
using System;

namespace BlazorApp.Server.Utilities
{
    public class DatabaseInitializer
    {
        private readonly IMongoDatabase _database;

        public DatabaseInitializer(IMongoDatabase database)
        {
            _database = database;
        }

        public void InitializeCollections()
        {
            DefineCollection("Users");
            DefineCollection("Groups");
            DefineCollection("IndividualChats");
            DefineCollection("Messages");
        }

        private void DefineCollection(string collectionName)
        {
            try
            {
                _database.CreateCollection(collectionName);
                Console.WriteLine($"{collectionName} collection created successfully.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Failed to create {collectionName} collection: {ex.Message}");
                throw;
            }
        }
    }
}
