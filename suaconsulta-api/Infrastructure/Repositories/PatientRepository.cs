using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using suaconsulta_api.Domain.Model;
using suaconsulta_api.Infrastructure.Data;

namespace suaconsulta_api.Infrastructure.Repositories
{
    /// <summary>
    /// Repository para modelo de Patient
    /// </summary>
    public class PatientRepository
    {
        private readonly AppDbContext _context;

        public PatientRepository(AppDbContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        /// <summary>
        /// Realiza um update no patient no banco de dados
        /// </summary>
        /// <param name="patient">ModelPatient</param>
        /// <returns></returns>
        public async Task<bool> UpdatePatient(ModelPatient patient)
        {
            _context.Patient.Update(patient);
            await _context.SaveChangesAsync();
            return true;
        }

        /// <summary>
        /// Retorna o patient pelo ID
        /// </summary>
        /// <param name="PatientId"></param>
        /// <returns></returns>
        public async Task<ModelPatient?> GetPatientById(int PatientId)
        {
            return await _context.Patient.Where(p => p.Id == PatientId).FirstOrDefaultAsync();
        }
    }
}