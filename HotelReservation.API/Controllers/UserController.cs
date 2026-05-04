using HotelReservation.Application.Dtos.User;
using HotelReservation.Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HotelReservation.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController :ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        //api/Users
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var users= await _userService.GetAll();
            return Ok(users);
        }

        //api/Users/{id}
        [HttpGet("{userId}")]
        public async Task<IActionResult> GetById(Guid userId)
        {
            var user = await _userService.GetById(userId);
            if (user == null) return NotFound("user introuvable");
            return Ok(user);
        }
        //api/Users
        //[Authorize(Roles = "Administrateur")]
        [HttpPost]
        public async Task<IActionResult> Add(CreateUserDto dto)
        {
            await _userService.Add(dto);
            return Ok("User crée avec succès");
        }
        //api/Users/updatePassword/{id}
        [HttpPut("updatePassword/{id}")]
        public async Task<IActionResult> UpdatePassword(Guid id, UpdatePasswordDto dto)
        {
            await _userService.UpdatePassword(id, dto);
            return Ok("Password mis à jour avec succès");
        }
        //api/Users/{id}
        [HttpPut("{clientId}")]
        public async Task<IActionResult> UpdateProfil(Guid clientId, UpdateUserDto dto)
        {
            await _userService.UpdateProfil(clientId, dto);
            return Ok("profil utilisateur mis à jour avec succès");
        }
        //api/Users/{id}
        //[Authorize(Roles = "Administrateur")]
        [HttpDelete("{clientId}")]
        public async Task<IActionResult> Desactiver(Guid clientId)
        {
            await _userService.Desactiver(clientId);
            return Ok("Utilisateur desactivé avec succès");
        }

        //// POST api/users/login
        //[HttpPost("login")]
        //public async Task<IActionResult> Login(LoginDto dto)
        //{
        //    var token = await _userService.Login(dto);
        //    return Ok(new { token });
        //}

    }
}
