using Microsoft.EntityFrameworkCore;
using suaconsulta_api.Data;
using suaconsulta_api.Model;

namespace suaconsulta_api.Repositories
{
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
    }
}