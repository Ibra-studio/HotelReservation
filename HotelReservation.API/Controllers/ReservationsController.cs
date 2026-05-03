using HotelReservation.Application.Dtos.Facture;
using HotelReservation.Application.Dtos.Reservation;
using HotelReservation.Application.Interfaces;
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
        [HttpGet("{id}")]
        public  async Task<IActionResult> GetById(Guid id)
        {
            var reservation = await _reservationService.GetById(id);
            if (reservation == null) return NotFound("reservation introuvable");
            return Ok(reservation);
        }
        //api/Reservations/client/{clientId}
        [HttpGet("client/{clientId}")]

        public  async Task<IActionResult> GetByClientId(Guid clientId)
        {
            var reservation = await _reservationService.GetByClientId(clientId);
            //pas de verification ==null car une liste n'est jamais ==null soit vide soit pleine
            return Ok(reservation);
        }

        //api/Reservations
        [HttpPost]

        public async  Task<IActionResult> Add(CreateReservationDto dto)
        {
            try
            {
                await _reservationService.Add(dto);
                return Ok("reservation Crée avec succès");

            } catch(InvalidOperationException ex)
            {
                return BadRequest(ex.Message);
            
            }catch(Exception ex) {

                return StatusCode(500, ex.Message);
            }

        }
        //api/Reservations/{id}
        [HttpPut("{id}")]

        public async Task<IActionResult> Update(Guid id, UpdateReservationDto dto)
        {
            try
            {
                await _reservationService.Update(id, dto);
                return Ok("Reservation mis à jour avec succès");
            }
            catch(KeyNotFoundException ex) {
                return NotFound(ex.Message);
            }
            catch(InvalidOperationException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
        //api/Reservations/{id}

        [HttpDelete("{id}")]

        public async Task<IActionResult> Annuler(Guid id)
        {
            try
            {
                await _reservationService.Annuler(id);
                return Ok("Reservation annulée avec succès");
            }catch(KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }catch(InvalidOperationException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        //api/Reservations/{id}/checkin
        [HttpPut("{id}/checkin")]
        public async Task<IActionResult> CheckIn(Guid id)
        {
            try
            {
                await _reservationService.CheckIn(id);
                return Ok("Check In effectué avec succès");
            }catch(KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                return NotFound( ex.Message);
            }
        }

        //api/Reservations/{id}/checkout
        [HttpPut("{id}/checkout")]

        public async Task<IActionResult> CheckOut(Guid reservationId)
        {

            try
            {
                var facture = await _reservationService.CheckOut(reservationId);
                return Ok(facture);
            }catch(KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }catch(InvalidOperationException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500,ex.Message);
            }
        }



    }
}
