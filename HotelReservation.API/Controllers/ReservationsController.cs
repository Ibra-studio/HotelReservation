using HotelReservation.Application.Dtos.Facture;
using HotelReservation.Application.Dtos.Reservation;
using HotelReservation.Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HotelReservation.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ReservationsController : ControllerBase
    {
        private readonly IReservationService _reservationService;

        public ReservationsController(IReservationService reservationService)
        {
            _reservationService = reservationService;
        }

        //api/Reservations/{id}
        [Authorize(Roles = "Administrateur,Receptionniste")]
        [HttpGet("{reservationId}")]
        public  async Task<IActionResult> GetById(Guid reservationId)
        {
            var reservation = await _reservationService.GetById(reservationId);
            if (reservation == null) return NotFound("reservation introuvable");
            return Ok(reservation);
        }
        //api/Reservations/client/{clientId}
        [Authorize(Roles = "Administrateur,Receptionniste")]
        [HttpGet("client/{clientId}")]

        public  async Task<IActionResult> GetByClientId(Guid clientId)
        {
            var reservation = await _reservationService.GetByClientId(clientId);
            //pas de verification ==null car une liste n'est jamais ==null soit vide soit pleine
            return Ok(reservation);
        }

        //api/Reservations
        [Authorize(Roles = "Administrateur,Receptionniste")]
        [HttpPost]

        public async  Task<IActionResult> Add(CreateReservationDto dto)
        {
           
                await _reservationService.Add(dto);
                return Ok("reservation Crée avec succès");


        }
        //api/Reservations/{id}
        [Authorize(Roles = "Administrateur,Receptionniste")]
        [HttpPut("{reservationId}")]

        public async Task<IActionResult> Update(Guid reservationId, UpdateReservationDto dto)
        {
           
                await _reservationService.Update(reservationId, dto);
                return Ok("Reservation mis à jour avec succès");
           
        }
        //api/Reservations/{id}
        [Authorize(Roles = "Administrateur,Receptionniste")]
        [HttpDelete("{reservationId}")]

        public async Task<IActionResult> Annuler(Guid reservationId)
        {
           
                var facture= await _reservationService.Annuler(reservationId);
                return Ok(facture);
           
        }

        //api/Reservations/{id}/checkin
        [Authorize(Roles = "Administrateur,Receptionniste")]
        [HttpPut("{reservationId}/checkin")]
        public async Task<IActionResult> CheckIn(Guid reservationId)
        {
            
                await _reservationService.CheckIn(reservationId);
                return Ok("Check In effectué avec succès");
            
        }

        //api/Reservations/{id}/checkout
        [Authorize(Roles = "Administrateur,Receptionniste")]
        [HttpPut("{reservationId}/checkout")]

        public async Task<IActionResult> CheckOut(Guid reservationId)
        {

         
                var facture = await _reservationService.CheckOut(reservationId);
                return Ok(facture);
            
        }
        [Authorize(Roles = "Administrateur,Receptionniste")]
        [HttpGet("test")]
        public async Task<IActionResult> Test()
        {
            throw new KeyNotFoundException("test KeyNotFound");
        }



    }
}
