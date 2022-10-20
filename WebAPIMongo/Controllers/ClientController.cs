using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using WebAPIMongo.Models;
using WebAPIMongo.Services;

namespace WebAPIMongo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClientController : ControllerBase
    {
        private readonly ClientService _clientService;
        private readonly AddressService _addressService;

        public ClientController(ClientService clientService, AddressService addressService)
        {
            _clientService = clientService;
            _addressService = addressService;
        }

        [HttpGet]
        public ActionResult<List<Client>> Get() => _clientService.Get();

        [HttpGet("{id:length(24)}", Name = "GetClient")]
        public ActionResult<Client> Get(string id)
        {
            var client = _clientService.Get(id);
            if (client == null)
                return NotFound();

            return Ok(client);
        }

        [HttpGet("{name}", Name = "GetName")]
        public ActionResult<Client> GetByName(string name)
        {
            var client = _clientService.GetByName(name);
            if (client == null)
                return NotFound();

            return Ok(client);
        }

        [HttpGet("address/{id:length(24)}", Name = "GetClientByAddress")]
        public ActionResult<Client> GetByAddress(string id)
        {
            var client = _clientService.GetByAddress(id);
            if (client == null)
                return NotFound();

            return Ok(client);
        }

        [HttpPost] //insert or create
        public ActionResult<Client> Create(Client client)
        {
            Address address = _addressService.Create(client.Address);
            client.Address = address;
            _clientService.Create(client);
            return CreatedAtRoute("GetClient", new { id = client.Id.ToString() }, client);
        }

        [HttpPut] //update
        public ActionResult<Client> Put(Client clientIn, string id)
        {
            var client = _clientService.Get(id);
            if (client == null)
                return NotFound();

            clientIn.Id = id;
            _clientService.Update(clientIn.Id, clientIn);
            return NoContent();
        }

        [HttpDelete]
        public ActionResult<Client> Delete(string id)
        {
            Client client = _clientService.Get(id);
            if (client == null)
                return NotFound();

            _clientService.Remove(client);
            return NoContent();
        }
    }
}

