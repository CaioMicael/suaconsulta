using Microsoft.EntityFrameworkCore;
using suaconsulta_api.Application.DTO;
using suaconsulta_api.Domain.Model;
using suaconsulta_api.Infrastructure.Data;

namespace suaconsulta_api.Infrastructure.Repositories
{
    /// <summary>
    /// Repository para o modelo de Doctor
    /// </summary>
    public class DoctorRepository : RepositoryBase
    {
        private readonly AppDbContext _context;

        public DoctorRepository(AppDbContext context) : base(context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        /// <summary>
        /// Realiza a atualização de um cadastro de médico no banco de dados
        /// </summary>
        /// <param name="Doctor">ModelDoctor</param>
        /// <returns>boolean</returns>
        /// <exception cref="ArgumentNullException"></exception>
        public async Task<bool> UpdateDoctor(ModelDoctor Doctor)
        {
            if (Doctor == null) throw new ArgumentNullException(nameof(Doctor));
            var existing = await _context.Doctor.FindAsync(Doctor.Id);
            if (existing == null) return false;

            _context.Entry(existing).State = EntityState.Detached;
            _context.Doctor.Update(Doctor);
            await _context.SaveChangesAsync();
            return true;
        }

        /// <summary>
        /// Realiza a inserção de um novo doctor no banco de dados
        /// </summary>
        /// <param name="Doctor">ModelDoctor</param>
        /// <returns>boolean</returns>
        public async Task<bool> CreateDoctor(ModelDoctor Doctor)
        {
            ArgumentNullException.ThrowIfNull(Doctor);

            await _context.AddAsync(Doctor);
            return true;
        }

        /// <summary>
        /// Retorna a página de doctors
        /// </summary>
        /// <returns>PagedResultDto com entidade Doctor</returns>
        public async Task<PagedResultDto<ModelDoctor>> GetDoctorPage()
        {
            IQueryable<ModelDoctor> queryDoctor = _context.Doctor.AsNoTracking();
            return await GetPagedAsync(queryDoctor, 1, 20);
        }

        /// <summary>
        /// Retorna o doctor pelo ID do mesmo
        /// </summary>
        /// <param name="idDoctor">int</param>
        /// <returns>ModelDoctor|null</returns>
        public async Task<ModelDoctor?> GetDoctorById(int idDoctor)
        {
            ModelDoctor? doctor = await _context.Doctor.AsNoTracking().FirstOrDefaultAsync(i => i.Id == idDoctor);
            return doctor;
        }
    }
}