using HotelReservation.Application.Dtos.Chambre;
using HotelReservation.Application.Services;
using Microsoft.AspNetCore.Mvc;

namespace HotelReservation.API.Controllers
{
    [ApiController]
    [Route("/api/[controller]")]
    public class ChambreController : ControllerBase
    {
        private readonly ChambreService _chambreService;


        public ChambreController(ChambreService chambreService)
        {
            _chambreService = chambreService;
        }

        //api/Chambres
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var reservations = await _chambreService.GetAll();
            return Ok(reservations);
        }

        //api/Chambres/{id}
        [HttpGet("{chambreId}")]
        public async Task<IActionResult> GetById(Guid chambreId)
        {
            var chambre = await _chambreService.GetById(chambreId);
            if (chambre == null) return NotFound("chambre introuvable");

            return Ok(chambre);
        }
        //api/Chambres/disponibles?dateArrivee="2025-12-03"&dateDepart="2025-12-12"
        [HttpGet("disponibles")]
        public async Task<IActionResult> GetDisponibles([FromQuery] DateOnly dateArrivee, [FromQuery] DateOnly dateDepart)
        {
            var chambres = await _chambreService.GetDisponibles(dateArrivee, dateDepart);
            return Ok(chambres);
        }

        //api/chambres
        [HttpPost]
        public async Task<IActionResult> Add(CreateChambreDto dto)
        {

            await _chambreService.Add(dto);
            return Ok("Chambre Crée avec succès");
        }

        //api/chambres/{id}
        [HttpPut("{chambreId}")]
        public async Task<IActionResult> Update(Guid chambreId, UpdateChambreDto dto)
        {
            await _chambreService.Update(chambreId, dto);
            return Ok("Chambre mise à jour avec succès");

        }
        //api/chambres/{id}

        [HttpDelete("{chambreId}")]
        public async Task<IActionResult> Desactiver(Guid chambreId)
        {
            await _chambreService.Desactiver(chambreId);
            return Ok("Chambre desactivée avec succès");
        }
        //api/chambres/{id}/equipements/{equipementId}
        [HttpPost("{chambreId}/ajouterEquipements/{equipementID}")]
        public async Task<IActionResult> AjouterEquiments(Guid chambreId, Guid equipementId)
        {
            await _chambreService.AjouterEquiments(chambreId, equipementId);
            return Ok("Equipement ajouté avec succès");
        }

        //api/chambres/{id}/equipements/{equipementId}
        [HttpDelete("{chambreId}/retirerEquipements/{equipementID}")]
        public async Task<IActionResult> RetirerEquiments(Guid chambreId, Guid equipementId)
        {
            await _chambreService.RetirerEquipements(chambreId, equipementId);
            return Ok("Equipement retiré avec succès");
        }

    }
}

