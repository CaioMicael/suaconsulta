using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using suaconsulta_api.DTO;
using suaconsulta_api.Model;
using suaconsulta_api.Repositories;

namespace suaconsulta_api.Services
{
    public class AuthService : InterfaceAuthService
    {
        private readonly AuthRepository authRepository;
        private readonly InterfaceUserRepository userRepository;
        private readonly JwtService jwtService;

        public AuthService(AuthRepository authRepository, InterfaceUserRepository userRepository, JwtService jwtService)
        {
            this.authRepository = authRepository ?? throw new ArgumentNullException(nameof(authRepository));
            this.userRepository = userRepository ?? throw new ArgumentNullException(nameof(userRepository));
            this.jwtService = jwtService ?? throw new ArgumentNullException(nameof(jwtService));
        }

        public async Task<bool> isEmailAlreadyRegister(SignUpDto dto)
        {
            var user = await authRepository.getUserByEmail(dto.mail);
            if (user != null)
            {
                return true;
            }

            return false;
        }

        public Task<bool> isPasswordValid(string password)
        {
            if (password == null || password.Length < 6)
            {
                return Task.FromResult(false);
            }
            return Task.FromResult(true);
        }

        public IActionResult DoSignUp(SignUpDto dto)
        {
            if (isEmailAlreadyRegister(dto).Result)
            {
                return new ConflictObjectResult("Email já cadastrado");
            }

            if (!isPasswordValid(dto.pass).Result)
            {
                return new BadRequestObjectResult("Senha deve ter pelo menos 6 caracteres");
            }

            var hasher = new PasswordHasher<ModelUsers>();
            string hash = hasher.HashPassword(null, dto.pass);
            var user = new ModelUsers
            {
                ExternalId = 0,
                TypeUser = dto.TypeUser,
                Mail = dto.mail,
                Password = hash
            };

            userRepository.InsertUser(user);

            // Gera o JWT
            var token = jwtService.GenerateToken(user);
            return new OkObjectResult(new { token = token, role = user.TypeUser });
        }
    }
}