using HotelReservation.Application.Dtos.Client;
using HotelReservation.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace HotelReservation.API.Controllers
{
  

    namespace HotelReservation.API.Controllers
    {
        [ApiController]
        [Route("api/[controller]")]
        public class ClientsController : ControllerBase
        {
            private readonly IClientService _clientService;

            public ClientsController(IClientService clientService)
            {
                _clientService = clientService;
            }

            // GET api/clients
            [HttpGet]
            public async Task<IActionResult> GetAll()
            {
                var clients = await _clientService.GetAll();
                return Ok(clients);
            }

            // GET api/clients/{id}
            [HttpGet("{id}")]
            public async Task<IActionResult> GetById(Guid id)
            {
                var client = await _clientService.GetById(id);
                if (client == null) return NotFound("Client introuvable");
                return Ok(client);
            }

            // GET api/clients/search/nom/{nom}
            [HttpGet("search/nom/{nom}")]
            public async Task<IActionResult> GetByNom(string nom)
            {
                var client = await _clientService.GetByNom(nom);
                if (client == null) return NotFound("Client introuvable");
                return Ok(client);
            }

            // GET api/clients/search/identite/{num}
            [HttpGet("search/identite/{num}")]
            public async Task<IActionResult> GetByNumIdentite(string num)
            {
                var client = await _clientService.GetByNumIdentite(num);
                if (client == null) return NotFound("Client introuvable");
                return Ok(client);
            }

            // GET api/clients/{id}/historiqueReservation
            [HttpGet("{id}/historiqueReservation")]
            public async Task<IActionResult> GetHistorique(Guid id)
            {
                var historique = await _clientService.GetHistorique(id);
                return Ok(historique);
            }

            // POST api/clients
            [HttpPost]
            public async Task<IActionResult> Create(CreateClientDto dto)
            {
                await _clientService.Add(dto);
                return Ok("Client créé avec succès");
            }

            // PUT api/clients/{id}
            [HttpPut("{id}")]
            public async Task<IActionResult> Update(Guid id, UpdateClientDto dto)
            {
                
                    await _clientService.Update(id, dto);
                    return Ok("Client mis à jour avec succès");
                
            }

            // DELETE api/clients/{id}
            [HttpDelete("{id}")]
            public async Task<IActionResult> Desactiver(Guid id)
            {
                
                    await _clientService.Desactiver(id);
                    return Ok("Client désactivé avec succès");
               
            }
        }
    }

}
