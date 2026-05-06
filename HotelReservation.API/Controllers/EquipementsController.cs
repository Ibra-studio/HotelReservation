using HotelReservation.Application.Dtos.Equipement;
using HotelReservation.Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace HotelReservation.API.Controllers
{
    [ApiController]
    [Route("api/[Controller]")]
    public class EquipementsController : ControllerBase
    {
        private readonly IEquipementService _equipementsService;

        public EquipementsController(IEquipementService equipementsService)
        {
            _equipementsService = equipementsService;
        }



        //api/Equipements
        [Authorize(Roles = "Administrateur,Receptionniste")]
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var Equipements=await _equipementsService.GetAll();
            return Ok(Equipements);
        }

        //api/Equipements/{id}
        [Authorize(Roles = "Administrateur,Receptionniste")]
        [HttpGet("{equipementId}")]

        public async Task<IActionResult> GetById(Guid equipementId)
        {
            var equipement = await _equipementsService.GetById(equipementId);
            if (equipement == null) return NotFound("Equipement introuvable");
            return Ok(equipement);

        }
        //api/Equipements
        [Authorize(Roles = "Administrateur")]
        [HttpPost]
        public async Task<IActionResult> Add(CreateEquipementDto dto)
        {
            await _equipementsService.Add(dto);
            return Ok("Equipement crée avec succès");
        }

        //api/Equipements/{id}
        [Authorize(Roles = "Administrateur")]
        [HttpPut("{equipementId}")]
        public async Task<IActionResult> Update(Guid equipementId, UpdateEquipementDto dto)
        {
            await _equipementsService.Update(equipementId, dto);
            return Ok("Equipement mis à jouur avec succès");
        }
        //api/Equipements/{id}
        [Authorize(Roles = "Administrateur")]
        [HttpDelete("{equipementId}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            await _equipementsService.Delete(id);
            return Ok("Equipement supprimé avec succès");
        }

    }
}
