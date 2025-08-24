namespace suaconsulta_api.Repositories
{
    using Microsoft.EntityFrameworkCore;
    using suaconsulta_api.Data;
    using suaconsulta_api.Domain.Model;
    using suaconsulta_api.Domain.Model.Enum;
    using suaconsulta_api.DTO;

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

        public async Task<ModelUsers?> GetUserByEmail(string email)
        {
            var existingUser = await _context.Users.FirstOrDefaultAsync(u => u.Mail == email);
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
                    }
                    catch (Exception ex)
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

        /// <summary>
        /// Retorna informações externas do usuário, como paciente ou médico, dependendo do tipo de usuário.
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public async Task<UserExternalInfoDto?> getExternalUserInfo(int userId)
        {
            var user = await getUserById(userId);
            if (user == null)
                return null;

            if (user.TypeUser == EnumTypeUsers.Patient)
            {
                var response = _context.Users.
                    Join(_context.Patient, u => u.ExternalId, patient => patient.Id, (u, patient) => new
                    {
                        user = u,
                        patient
                    }).Select(result => new UserExternalInfoDto
                    {
                        User = result.user,
                        Patient = result.patient
                    }).FirstOrDefault(result => result.User.Id == user.Id);
                return response;
            }
            else if (user.TypeUser == EnumTypeUsers.Doctor)
            {
                var response = _context.Users.
                    Join(_context.Doctor, u => u.ExternalId, doctor => doctor.Id, (u, doctor) => new
                    {
                        user = u,
                        doctor
                    }).Select(result => new UserExternalInfoDto
                    {
                        User = result.user,
                        Doctor = result.doctor
                    }).FirstOrDefault(result => result.User.Id == user.Id);
                return response;
            }

            return null;
        }

        public async Task InsertUser(ModelUsers user)
        {
            ArgumentNullException.ThrowIfNull(nameof(user));

            try
            {
                await _context.Users.AddAsync(user);
                await _context.SaveChangesAsync();
            } catch (Exception ex)
            {
                throw new Exception("Erro ao inserir usuário: " + ex.Message);
            }
        }
    }
}