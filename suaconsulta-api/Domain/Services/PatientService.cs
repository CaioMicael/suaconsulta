using suaconsulta_api.Application.DTO;
using suaconsulta_api.Core.Common;
using suaconsulta_api.Domain.Errors;
using suaconsulta_api.Domain.Model;
using suaconsulta_api.Infrastructure.Repositories;

namespace suaconsulta_api.Domain.Services
{
    /// <summary>
    /// Service para o modelo de Patient
    /// </summary>
    public class PatientService
    {
        private readonly PatientRepository _patientRepository;
        private readonly userRepository _userRepository;

        public PatientService(PatientRepository patientRepository, userRepository userRepository)
        {
            _patientRepository = patientRepository ?? throw new ArgumentNullException(nameof(patientRepository));
            _userRepository = userRepository ?? throw new ArgumentNullException(nameof(userRepository));
        }

        /// <summary>
        /// Realiza a criação de um paciente
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        public async Task<ModelPatient> CreatePatient(CreatePatientDto dto)
        {
            ModelPatient patient = new ModelPatient
            {
                Name = dto.Name,
                Email = dto.Email,
                Birthday = dto.Birthday,
                Phone = dto.Phone,
                City = dto.City,
                State = dto.State,
                Country = dto.Country
            };

            return await _patientRepository.InsertPatient(patient);
        }

        /// <summary>
        /// Altera os dados do paciente
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        public async Task<Result<string>> AlterPatient(UpdatePatientDto dto, int userId)
        {
            UserExternalInfoDto? user = await _userRepository.getExternalUserInfo(userId);
            if (user == null || user.Patient == null)
            {
                return Result<string>.Failure(PatientDomainError.PatientNotFoundForUser);
            }

            ModelPatient patientToUpdate = user.Patient;
            patientToUpdate.Name = dto.Name;
            patientToUpdate.Email = dto.Email;
            patientToUpdate.Birthday = dto.Birthday;
            patientToUpdate.Phone = dto.Phone;
            patientToUpdate.City = dto.City;
            patientToUpdate.State = dto.State;
            patientToUpdate.Country = dto.Country;

            await _patientRepository.UpdatePatient(patientToUpdate);
            return Result<string>.Success("Dados de paciente alterado com sucesso!");
        }

    }
}