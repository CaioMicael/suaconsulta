using suaconsulta_api.Application.DTO;
using suaconsulta_api.Core.Common;
using suaconsulta_api.Domain.Errors;
using suaconsulta_api.Domain.Model;
using suaconsulta_api.Infrastructure.Repositories;

namespace suaconsulta_api.Domain.Services
{
    public class ConsultationService
    {
        private readonly ConsultationRepository _consultationRepository;
        private readonly PatientRepository _patientRepository;
        private readonly DoctorRepository _doctorRepository;

        public ConsultationService(
            ConsultationRepository consultationRepository,
            PatientRepository patientRepository,
            DoctorRepository doctorRepository
            )
        {
            _consultationRepository = consultationRepository ?? throw new ArgumentNullException(nameof(_consultationRepository));
            _patientRepository = patientRepository ?? throw new ArgumentNullException(nameof(_patientRepository));
            _doctorRepository = doctorRepository ?? throw new ArgumentNullException(nameof(_doctorRepository));
        }

        /// <summary>
        /// Retorna as consultas do médico repassado
        /// </summary>
        /// <param name="Doctor">ModelDoctor</param>
        /// <returns>DoctorConsultationsDto</returns>
        public async Task<Result<DoctorConsultationsDto>> GetDoctorConsultation(ModelDoctor Doctor)
        {
            return Result<DoctorConsultationsDto>.Success(await _consultationRepository.GetDoctorConsultations(Doctor));
        }

        /// <summary>
        /// Retorna as consultas do paciente repassado
        /// </summary>
        /// <param name="patientId"></param>
        /// <returns></returns>
        public async Task<Result<PatientConsultationsDto>> GetPatientConsultations(int patientId)
        {
            ModelPatient? Patient = await _patientRepository.GetPatientById(patientId);
            if (Patient == null)
                return Result<PatientConsultationsDto>.Failure(PatientDomainError.NotFoundPatient);

            PatientConsultationsDto PatientConsultations = await _consultationRepository.GetPatientConsultations(Patient);
            return Result<PatientConsultationsDto>.Success(PatientConsultations);
        }

        public async Task<Result<DoctorPatientConsultations>> GetConsultationByDoctorPatient(int DoctorId, int PatientId)
        {
            ModelDoctor? Doctor = await _doctorRepository.GetDoctorById(DoctorId);
            if (Doctor == null)
                return Result<DoctorPatientConsultations>.Failure(DoctorDomainError.NotFoundDoctor);

            ModelPatient? Patient = await _patientRepository.GetPatientById(PatientId);
            if (Patient == null)
                return Result<DoctorPatientConsultations>.Failure(PatientDomainError.NotFoundPatient);

            ModelConsultation[]? consultations = await _consultationRepository.GetConsultationByDoctorPatient(DoctorId, PatientId);

            DoctorPatientConsultations responseDto = new DoctorPatientConsultations();
            responseDto.Consultations = consultations;
            responseDto.Doctor = Doctor;
            responseDto.Patient = Patient;

            return Result<DoctorPatientConsultations>.Success(responseDto);
        }
    }
}