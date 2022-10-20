using MongoDB.Driver;
using System.Collections.Generic;
using WebAPIMongo.Models;
using WebAPIMongo.Utils;

namespace WebAPIMongo.Services
{
    public class ClientService
    {
        private readonly IMongoCollection<Client> _clients;

        public ClientService(IDatabaseSettings settings)
        {
            var client = new MongoClient(settings.ConnectionString);
            var database = client.GetDatabase(settings.DatabaseName);
            _clients = database.GetCollection<Client>(settings.ClientCollectionName);
        }

        public Client Create(Client client)
        {
            _clients.InsertOne(client);
            return client;
        }

        public List<Client> Get() => _clients.Find<Client>(client => true).ToList();

        public Client Get(string id) => _clients.Find<Client>(client => client.Id == id).FirstOrDefault();

        public List<Client> GetByName(string name) => _clients.Find<Client>(client => client.Name == name).ToList();

        public Client GetByAddress(string id) => _clients.Find(client => client.Address.Id == id).FirstOrDefault();

        public void Update(string id, Client clientIn)
        {
            _clients.ReplaceOne(client => client.Id == id, clientIn);
        }

        public void Remove(Client clientIn) => _clients.DeleteOne(client => client.Id == clientIn.Id);
    }
}
