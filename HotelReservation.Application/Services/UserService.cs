using HotelReservation.Application.Dtos.User;
using HotelReservation.Application.Interfaces;
using HotelReservation.Domain.Entities;
using HotelReservation.Domain.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace HotelReservation.Application.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
       
        private  readonly IConfiguration _configuration;

        public UserService(IUserRepository userRepository, IConfiguration configuration)
        {
            _userRepository = userRepository;
            _configuration = configuration;
            
        }

        public async Task<List<UserDto>> GetAll()
        {
            var users = await _userRepository.GetAll();
            return users.Select(u => new UserDto
            (
                u.Id,
                u.Nom,
                u.Courriel,
                u.Role,
                u.EstActif
            )).ToList();
        }
        public async Task<UserDto?> GetById(Guid id)
        {
            var user = await _userRepository.GetById(id);
            if (user == null)
                return null;
            return new UserDto
            (
                user.Id,
                user.Nom,
                user.Courriel,
                user.Role,
                user.EstActif
            );
        }
        public async Task Add(CreateUserDto dto)
        {
            var userExistant = await _userRepository.GetByCourriel(dto.Courriel);
            if (userExistant != null)
                throw new InvalidOperationException("Un utilisateur avec ce courriel existe déjà.");

            var passwordHash = BCrypt.Net.BCrypt.HashPassword(dto.Password); //Pour hasher le mot de passe Avec le nugert BCrypt.Net-Next

            var user = new User
            {
                Id = Guid.NewGuid(),
                Nom = dto.Nom,
                Courriel = dto.Courriel,
                PasswordHash = passwordHash,
                Role = dto.Role,
                EstActif = true
            };
            await _userRepository.Add(user);
        }

        public async Task UpdatePassword(Guid id, UpdatePasswordDto dto)
        {
            var user = await _userRepository.GetById(id);
            if (user == null)
                throw new KeyNotFoundException("Utilisateur non trouvé.");
            if (!BCrypt.Net.BCrypt.Verify(dto.AncienMotDePasse, user.PasswordHash))
                throw new InvalidOperationException("Ancien mot de passe incorrect.");
            if (dto.NouveauMotDePasse != dto.ConfirmationMotDePasse)
                throw new InvalidOperationException("La confirmation du mot de passe ne correspond pas.");
            user.PasswordHash = BCrypt.Net.BCrypt.HashPassword(dto.NouveauMotDePasse);
            await _userRepository.Update(user);
        }
        public async Task UpdateProfil(Guid id, UpdateUserDto dto)
        {
            var user = await _userRepository.GetById(id);
            if (user == null)
                throw new KeyNotFoundException("Utilisateur non trouvé.");

            if (user.Courriel != dto.Courriel)
            {
                var userExistant = await _userRepository.GetByCourriel(dto.Courriel);
                if (userExistant != null)
                    throw new InvalidOperationException("Un utilisateur avec ce courriel existe déjà.");
            }
            user.Nom = dto.Nom;
            user.Courriel = dto.Courriel;
            await _userRepository.Update(user);
        }

        public async Task Desactiver(Guid id)
        {
            var user = await _userRepository.GetById(id);
            if (user == null)
                throw new KeyNotFoundException("Utilisateur non trouvé.");
            user.EstActif = false;
            await _userRepository.Update(user);
        }
        public async Task Reactiver(Guid id)
        {
            var user = await _userRepository.GetById(id);
            if (user == null)
                throw new KeyNotFoundException("Utilisateur non trouvé.");
            user.EstActif = true;
            await _userRepository.Update(user);
        }
        public async Task<string> Login(LoginDto dto)
        {
            var user = await _userRepository.GetByCourriel(dto.Courriel);
            if (user == null || !BCrypt.Net.BCrypt.Verify(dto.Password, user.PasswordHash))
                throw new InvalidOperationException("Courriel ou mot de passe incorrect.");
            if (!user.EstActif)
                throw new InvalidOperationException("Utilisateur désactivé.");

            var token = CreateToken(user);
            // Ici, vous pouvez générer un token JWT ou une session selon votre besoin
            return token; // Placeholder pour le token
        }


        private string CreateToken(User user)
        {
            var claims = new List<Claim>()
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Name, user.Nom),
                new Claim(ClaimTypes.Email, user.Courriel),
                new Claim(ClaimTypes.Role,user.Role.ToString()),
                
            };
            var key = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]!));

            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                 issuer: _configuration["Jwt:Issuer"],
                 audience: _configuration["Jwt:Issuer"],
                 claims: claims,
                 expires: DateTime.Now.AddDays(1),
                 signingCredentials: creds
                );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }


        //private void VerifierAdministrateur()
        //{
        //    var user = _httpContextAccessor.HttpContext?.User;
        //    var isAdmin = user?.FindFirst(System.Security.Claims.ClaimTypes.Role)?.Value == "Admin";

        //    if (!isAdmin)
        //        throw new UnauthorizedAccessException("Seul un administrateur peut effectuer cette action.");
        //}

    }
}
