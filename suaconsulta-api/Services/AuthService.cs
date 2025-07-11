using suaconsulta_api.DTO;
using suaconsulta_api.Repositories;

namespace suaconsulta_api.Services
{
    public class AuthService : InterfaceAuthService
    {
        private readonly AuthRepository authRepository;

        public AuthService(AuthRepository authRepository)
        {
            this.authRepository = authRepository ?? throw new ArgumentNullException(nameof(authRepository));
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
    }
}