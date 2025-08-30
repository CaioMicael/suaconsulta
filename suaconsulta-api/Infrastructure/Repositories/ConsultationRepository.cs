using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using suaconsulta_api.Application.DTO;
using suaconsulta_api.Domain.Model;
using suaconsulta_api.Domain.Model.Enum;
using suaconsulta_api.Infrastructure.Data;
using suaconsulta_api.Migrations;

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
        public async Task<PatientConsultationsDto> GetPatientConsultations(ModelPatient Patient)
        {
            var consultations = await _context.Consultation
                .Where(c => c.Patient.Id == Patient.Id)
                .AsNoTracking()
                .ToListAsync();

            return new PatientConsultationsDto
            {
                Consultations = consultations,
                Patient = Patient
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

            DoctorConsultationsDto response = new DoctorConsultationsDto
            {
                Doctor = Doctor,
                Consultation = DoctorConsultations
            };

            return response;
        }

        /// <summary>
        /// Retorna as consultas onde o médico e paciente passado estão relacionados
        /// </summary>
        /// <param name="DoctorId"></param>
        /// <param name="PatientId"></param>
        /// <returns></returns>
        public async Task<ModelConsultation[]?> GetConsultationByDoctorPatient(int DoctorId, int PatientId)
        {
            ModelConsultation[]? consultation = await _context.Consultation
                .Where(C => C.Doctor.Id == DoctorId && C.Patient.Id == PatientId)
                .AsNoTracking()
                .ToArrayAsync();

            return consultation;
        }

        /// <summary>
        /// Retorna a primeira consulta com o ID repassado
        /// </summary>
        /// <param name="id">int</param>
        /// <returns>ModelConsultation|null</returns>
        public async Task<ModelConsultation?> GetConsultationById(int id)
        {
            return await _context.Consultation.AsNoTracking().FirstOrDefaultAsync(C => C.Id == id);
        }

        public async Task<EnumStatusConsultation> GetConsultationStatusByDateDoctor(DateTime consultationDate, int doctorId)
        {
            return await _context.Consultation
            .AsNoTracking()
            .Where(
                C => C.Doctor.Id == doctorId &&
                C.Date.Year == consultationDate.Year &&
                C.Date.Month == consultationDate.Month &&
                C.Date.Day == consultationDate.Day &&
                C.Date.Hour == consultationDate.Hour &&
                C.Date.Minute == consultationDate.Minute
            )
            .Select(C => C.Status)
            .FirstOrDefaultAsync();
        }

        public async Task<bool> CreateConsultation(CreateConsultation consultation)
        {
            await _context.AddAsync(consultation);
            return true;
        }
    }
}