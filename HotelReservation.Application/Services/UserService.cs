using HotelReservation.Application.Dtos.User;
using HotelReservation.Application.Interfaces;
using HotelReservation.Domain.Entities;
using HotelReservation.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace HotelReservation.Application.Services
{
    public class UserService: IUserService
    {
        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
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
                   throw new Exception("Un utilisateur avec ce courriel existe déjà.");

               var passwordHash= BCrypt.Net.BCrypt.HashPassword(dto.Password); //Pour hasher le mot de passe Avec le nugert BCrypt.Net-Next

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
                throw new Exception("Utilisateur non trouvé.");
            if (!BCrypt.Net.BCrypt.Verify(dto.AncienMotDePasse, user.PasswordHash))
                throw new Exception("Ancien mot de passe incorrect.");
            if (dto.NouveauMotDePasse != dto.ConfirmationMotDePasse)
                throw new Exception("La confirmation du mot de passe ne correspond pas.");
            user.PasswordHash = BCrypt.Net.BCrypt.HashPassword(dto.NouveauMotDePasse);
            await _userRepository.Update(user);
        }
        public async Task UpdateProfil(Guid id, UpdateUserDto dto)
        {
            var user = await _userRepository.GetById(id);
            if (user == null)
                throw new Exception("Utilisateur non trouvé.");

            if(user.Courriel != dto.Courriel)
            {
                var userExistant = await _userRepository.GetByCourriel(dto.Courriel);
                if (userExistant != null)
                    throw new Exception("Un utilisateur avec ce courriel existe déjà.");
            }
            user.Nom = dto.Nom;
            user.Courriel = dto.Courriel;
            user.Role = dto.Role;
           
            await _userRepository.Update(user);
        }

        public async Task Desactiver(Guid id)
            {
                var user = await _userRepository.GetById(id);
                if (user == null)
                    throw new Exception("Utilisateur non trouvé.");
                user.EstActif = false;
                await _userRepository.Update(user);
        }
        public async Task<string> Login(LoginDto dto)
        {
            var user = await _userRepository.GetByCourriel(dto.Courriel);
            if (user == null || !BCrypt.Net.BCrypt.Verify(dto.Password, user.PasswordHash))
                throw new Exception("Courriel ou mot de passe incorrect.");
            if (!user.EstActif)
                throw new Exception("Utilisateur désactivé.");
            // Ici, vous pouvez générer un token JWT ou une session selon votre besoin
            return "TokenJWT_Généré"; // Placeholder pour le token
        }

    }
}
