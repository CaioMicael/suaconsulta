using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using suaconsulta_api.Application.DTO;
using suaconsulta_api.Domain.Model;
using suaconsulta_api.Infrastructure.Data;

namespace suaconsulta_api.Infrastructure.Repositories
{
    public class ConsultationRepository
    {
        private readonly AppDbContext _context;

        public ConsultationRepository(AppDbContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        /// <summary>
        /// Retorna as consultas do paciente
        /// </summary>
        /// <param name="patientId">Id do paciente</param>
        /// <returns>PatientConsultationsDto</returns>
        public async Task<PatientConsultationsDto?> GetPatientConsultations(int patientId)
        {
            var patient = await _context.Patient.FirstOrDefaultAsync(p => p.Id == patientId);
            if (patient == null)
                return null;

            var consultations = await _context.Consultation
                .Where(c => c.Patient.Id == patientId)
                .AsNoTracking()
                .ToListAsync();

            return new PatientConsultationsDto
            {
                Consultations = consultations,
                Patient = patient
            };
        }

        /// <summary>
        /// Retorna as consultas do médico repassado
        /// </summary>
        /// <param name="Doctor">ModelDoctor</param>
        /// <returns>DoctorConsultationDto</returns>
        public async Task<DoctorConsultationsDto> GetDoctorConsultations(ModelDoctor Doctor)
        {
            var DoctorConsultations = await _context.Consultation
                .Where(c => c.Doctor.Id == Doctor.Id)
                .OrderBy(C => C.Id)
                .AsNoTracking()
                .ToListAsync();

            return new DoctorConsultationsDto
            {
                Doctor = Doctor,
                Consultation = DoctorConsultations
            };
        }
    }
}