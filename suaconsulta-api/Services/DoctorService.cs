using suaconsulta_api.DTO;
using suaconsulta_api.Model;
using suaconsulta_api.Repositories;

namespace suaconsulta_api.Services
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
        /// <returns></returns>
        public async Task<bool> UpdateDoctorDto(UpdateDoctorDto DoctorDto)
        {
            var doctor = new ModelDoctor
            {
                Id = DoctorDto.Id,
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
            return response;
        }

        /// <summary>
        /// Realiza a criação de um doctor na base de dados
        /// </summary>
        /// <param name="DoctorDto">CreateDoctorDto</param>
        /// <returns></returns>
        public async Task<bool> CreateDoctorDto(CreateDoctorDto DoctorDto)
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

            bool response = await _doctorRepository.CreateDoctor(doctor);
            return response;
        }
    }
}