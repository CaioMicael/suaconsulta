namespace suaconsulta_api.Repositories
{
    using Microsoft.EntityFrameworkCore;
    using suaconsulta_api.Data;
    using suaconsulta_api.Model;

    public class userRepository : InterfaceUserRepository
    {
        private readonly AppDbContext _context;

        public userRepository(AppDbContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public async Task<ModelUsers?> getUserById(int id)
        {
            var existingUser = await _context.Users.FirstOrDefaultAsync(u => u.Id == id);
            if (existingUser != null)
            {
                return existingUser;
            }

            return null;
        }

        public async void setExternalId(int userId, int externalId)
        {
            var user = await getUserById(userId);
            if (user != null)
            {
                if (user.ExternalId == null || user.ExternalId == 0)
                {
                    user.ExternalId = externalId;
                    try
                    {
                      await _context.SaveChangesAsync();                    
                    } catch (Exception ex)
                    {
                        throw new Exception("Erro ao salvar o usuário: " + ex.Message);
                    }
                }
                else
                {
                    throw new Exception("Usuário já possui um cadastro vinculado");
                }
            }
            else
            {
                throw new Exception("Usuário não encontrado");
            }
        }
    }
}