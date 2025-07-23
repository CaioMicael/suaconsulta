using Microsoft.AspNetCore.Mvc;
using suaconsulta_api.Data;
using suaconsulta_api.Model;

namespace suaconsulta_api.Repositories
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
        public async Task<Boolean> UpdatePatient(ModelPatient patient)
        {
            _context.Patient.Update(patient);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}