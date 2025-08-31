using Microsoft.AspNetCore.Mvc;
using suaconsulta_api.Application.DTO;
using suaconsulta_api.Core.Common;
using suaconsulta_api.Domain.Errors;
using suaconsulta_api.Domain.Model;
using suaconsulta_api.Domain.Model.Enum;
using suaconsulta_api.Infrastructure.Repositories;

namespace suaconsulta_api.Domain.Services
{
    public class ConsultationService
    {
        private readonly ConsultationRepository _consultationRepository;
        private readonly PatientRepository _patientRepository;
        private readonly DoctorRepository _doctorRepository;
        private readonly DoctorScheduleRepository _doctorScheduleRepository;

        public ConsultationService(
            ConsultationRepository consultationRepository,
            PatientRepository patientRepository,
            DoctorRepository doctorRepository,
            DoctorScheduleRepository doctorScheduleRepository
            )
        {
            _consultationRepository = consultationRepository ?? throw new ArgumentNullException(nameof(_consultationRepository));
            _patientRepository = patientRepository ?? throw new ArgumentNullException(nameof(_patientRepository));
            _doctorRepository = doctorRepository ?? throw new ArgumentNullException(nameof(_doctorRepository));
            _doctorScheduleRepository = doctorScheduleRepository ?? throw new ArgumentNullException(nameof(_doctorScheduleRepository));
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

        /// <summary>
        /// Retorna o status da consulta pelo ID
        /// </summary>
        /// <param name="id">int</param>
        /// <returns>Result<string></returns>
        public async Task<Result<string>> GetConsultationStatusById(int id)
        {
            ModelConsultation? Consultation = await _consultationRepository.GetConsultationById(id);
            if (Consultation == null)
                return Result<string>.Failure(ConsultationDomainError.NotFoundConsultation);

            string? statusDescription = Enum.GetName(typeof(EnumStatusConsultation), Consultation.Status);
            if (statusDescription == null)
                return Result<string>.Failure(ConsultationDomainError.NotFoundStatusConsultation);

            return Result<string>.Success(statusDescription);
        }

        /// <summary>
        /// Validações de regra de negócio antes de adicionar a consulta
        /// </summary>
        /// <param name="CreateDto"></param>
        /// <returns></returns>
        public async Task<Result<bool>> AddConsultation(CreateConsultation CreateDto)
        {
            if (CreateDto.Date < DateTime.Now)
                return Result<bool>.Failure(ConsultationDomainError.BadRequestConsultationDateLessNow);

            if (! await IsConsultationDateAvailableInDoctorSchedule(CreateDto.Date, CreateDto.DoctorId))
                return Result<bool>.Failure(ConsultationDomainError.BadRequestDateForConsultationNotAvailable);

            if (await _consultationRepository.CreateConsultation(CreateDto))
                return Result<bool>.Success(true);

            return Result<bool>.Failure(DomainError.GenericBadRequest);
        }

        /// <summary>
        /// Atualiza a consulta conforme DTO repassado
        /// </summary>
        /// <param name="consultation">DTO de update consultation</param>
        /// <returns>Result</returns>
        public async Task<Result<bool>> UpdateConsultation(UpdateConsultation consultation)
        {
            ModelConsultation? consultationDb = await _consultationRepository.GetConsultationByIdWithTracking(consultation.Id);
            if (consultationDb == null)
                return Result<bool>.Failure(ConsultationDomainError.NotFoundConsultation);

            if (consultation.Date != consultationDb.Date)
                if (! await IsConsultationDateAvailableInDoctorSchedule(consultation.Date, consultationDb.DoctorId))
                    return Result<bool>.Failure(ConsultationDomainError.BadRequestDateForConsultationNotAvailable);

            consultationDb.Date = consultation.Date;
            consultationDb.Status = consultation.Status;

            if (await _consultationRepository.UpdateConsultation(consultationDb))
                return Result<bool>.Success(true);

            return Result<bool>.Failure(DomainError.GenericBadRequest);
        }

        /// <summary>
        /// Retorna se a data desejada para consulta está disponível na agenda do médico
        /// </summary>
        /// <param name="dateConsultation"></param>
        /// <param name="doctorId"></param>
        /// <param name="_doctorScheduleRepository"></param>
        /// <returns>bool</returns>
        protected async Task<bool> IsConsultationDateAvailableInDoctorSchedule(DateTime dateConsultation, int doctorId)
        {
            return await _doctorScheduleRepository.IsDateScheduleAvailable(dateConsultation, doctorId);
        }
    }
}