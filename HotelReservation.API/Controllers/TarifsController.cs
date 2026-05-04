using HotelReservation.Application.Dtos.Tarif;
using HotelReservation.Application.Interfaces;
using HotelReservation.Domain.Enums;
using Microsoft.AspNetCore.Mvc;

namespace HotelReservation.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TarifsController : ControllerBase
    {
        private readonly ITarifService _tarifService;

        public TarifsController(ITarifService tarifService)
        {
            _tarifService = tarifService;
        }

        // GET api/tarifs
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var tarifs = await _tarifService.GetAll();
            return Ok(tarifs);
        }

        // GET api/tarifs/{id}
        [HttpGet("{tarifId}")]
        public async Task<IActionResult> GetById(Guid tarifId)
        {
            var tarif = await _tarifService.GetById(tarifId);
            if (tarif == null) return NotFound("Tarif introuvable");
            return Ok(tarif);
        }

        // GET api/tarifs/typeAndsaison?type=0&saison=0
        [HttpGet("typeAndSaison")]
        public async Task<IActionResult> GetByTypeAndSaison([FromQuery] TypeChambre type, [FromQuery] Season saison)
        {
            var tarif = await _tarifService.GetByTypeAndSaison(type, saison);
            if (tarif == null) return NotFound("Tarif introuvable pour ce type de chambre et cette saison");
            return Ok(tarif);
        }

        // POST api/tarifs
        [HttpPost]
        public async Task<IActionResult> Create(CreateTarifDto dto)
        {
            await _tarifService.Add(dto);
            return Ok("Tarif créé avec succès");
        }

        // PUT api/tarifs/{id}
        [HttpPut("{tarifId}")]
        public async Task<IActionResult> Update(Guid tarifId, UpdateTarifDto dto)
        {
            await _tarifService.Update(tarifId, dto);
            return Ok("Tarif mis à jour avec succès");
        }
    }
}
