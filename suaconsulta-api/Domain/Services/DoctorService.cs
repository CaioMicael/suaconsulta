using suaconsulta_api.Application.DTO;
using suaconsulta_api.Core.Common;
using suaconsulta_api.Domain.Errors;
using suaconsulta_api.Domain.Model;
using suaconsulta_api.Infrastructure.Repositories;

namespace suaconsulta_api.Domain.Services
{
    /// <summary>
    /// Serviço para o modelo de Doctor
    /// </summary>
    public class DoctorService
    {
        private readonly DoctorRepository _doctorRepository;

        public DoctorService(DoctorRepository doctorRepository)
        {
            _doctorRepository = doctorRepository ?? throw new ArgumentNullException(nameof(_doctorRepository));
        }

        /// <summary>
        /// Atualiza o doctor conforme DTO repassado
        /// </summary>
        /// <param name="DoctorDto">UpdateDoctorDto</param>
        /// <param name="doctorId">Id do doctor deve ser extráido do userInfo</param>
        /// <returns></returns>
        public async Task<Result<bool>> UpdateDoctorDto(UpdateDoctorDto DoctorDto, int doctorId)
        {
            var doctor = new ModelDoctor
            {
                Id = doctorId,
                Name = DoctorDto.Name,
                Specialty = DoctorDto.Specialty,
                CRM = DoctorDto.CRM,
                Phone = DoctorDto.Phone,
                Email = DoctorDto.Email,
                City = DoctorDto.City,
                State = DoctorDto.State,
                Country = DoctorDto.Country
            };

            bool response = await _doctorRepository.UpdateDoctor(doctor);
            if (response)
                return Result<bool>.Success(response);

            return Result<bool>.Failure(DoctorDomainError.NotFoundDoctor);
        }

        /// <summary>
        /// Realiza a criação de um doctor na base de dados
        /// </summary>
        /// <param name="DoctorDto">CreateDoctorDto</param>
        /// <returns></returns>
        public async Task<ModelDoctor> CreateDoctorDto(CreateDoctorDto DoctorDto)
        {
            var doctor = new ModelDoctor
            {
                Name = DoctorDto.Name,
                Specialty = DoctorDto.Specialty,
                CRM = DoctorDto.CRM,
                Phone = DoctorDto.Phone,
                Email = DoctorDto.Email,
                City = DoctorDto.City,
                State = DoctorDto.State,
                Country = DoctorDto.Country
            };

            return await _doctorRepository.CreateDoctor(doctor);
        }

        /// <summary>
        /// Retorna o médico pelo ID informado
        /// </summary>
        /// <param name="idDoctor">ModelDoctor|null</param>
        /// <returns>result</returns>
        public async Task<Result<ModelDoctor>> GetDoctorById(int idDoctor)
        {
            if (idDoctor == 0)
                return Result<ModelDoctor>.Failure(DomainError.InvalidId);

            ModelDoctor? Doctor = await _doctorRepository.GetDoctorById(idDoctor);
            if (Doctor == null)
                return Result<ModelDoctor>.Failure(DomainError.GenericNotFound);

            return Result<ModelDoctor>.Success(Doctor);
        }
    }
}