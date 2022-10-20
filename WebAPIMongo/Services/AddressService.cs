using MongoDB.Driver;
using System.Collections.Generic;
using WebAPIMongo.Models;
using WebAPIMongo.Utils;

namespace WebAPIMongo.Services
{
    public class AddressService
    {
        private readonly IMongoCollection<Address> _address;

        public AddressService(IDatabaseSettings settings)
        {
            var address = new MongoClient(settings.ConnectionString);
            var database = address.GetDatabase(settings.DatabaseName);
            _address = database.GetCollection<Address>(settings.AddressCollectionName);
        }

        public Address Create(Address address)
        {
            _address.InsertOne(address);
            return address;
        }

        public List<Address> Get() => _address.Find(address => true).ToList();

        public Address Get(string id) => _address.Find(address => address.Id == id).FirstOrDefault();

        public void Update(string id, Address addressIn) => _address.ReplaceOne(address => address.Id == id, addressIn);

        public void Remove(Address addressIn) => _address.DeleteOne(address => address.Id == addressIn.Id);
    }
}
