using Microsoft.EntityFrameworkCore;
using suaconsulta_api.Data;
using suaconsulta_api.Model;

namespace suaconsulta_api.Repositories
{
    /// <summary>
    /// Contém métodos para interagir com o repositório de autenticação.
    /// </summary>
    public class AuthRepository : InterfaceAuthRepository
    {
        private readonly AppDbContext _context;

        public AuthRepository(AppDbContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public async Task<ModelUsers?> getUserByEmail(string email)
        {
            var existingUser = await _context.Users.FirstOrDefaultAsync(u => u.Mail == email);
            if (existingUser != null)
            {
                return existingUser;
            }

            return null;
        }

        /// <summary>
        /// Método para buscar um usuário pelo ID.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<ModelUsers?> getUserById(int id)
        {
            var existingUser = await _context.Users.FindAsync(id);
            if (existingUser != null)
            {
                return existingUser;
            }

            return null;
        }
    }
}