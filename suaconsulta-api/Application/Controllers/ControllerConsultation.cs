using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using suaconsulta_api.Domain.Services;
using suaconsulta_api.Application.DTO;
using suaconsulta_api.Infrastructure.Repositories;
using suaconsulta_api.Core.Common;
using suaconsulta_api.Domain.Errors;

namespace suaconsulta_api.Application.Controllers
{
    [Route("api/Consultation/")]
    public class ControllerConsultation : ControllerBase
    {
        private readonly ConsultationService _consultationService;
        private readonly InterfaceUserRepository _userRepository;

        public ControllerConsultation(ConsultationService consultationService,InterfaceUserRepository userRepository)
        {
            _consultationService = consultationService ?? throw new ArgumentNullException(nameof(consultationService));
            _userRepository = userRepository ?? throw new ArgumentNullException(nameof(userRepository));
        }

        /// <summary>
        /// Endpoint GET assíncrono para busca de todas as consultas.
        /// </summary>
        /// <returns>Result<DoctorConsultationsDto></returns>
        [HttpGet]
        [Authorize]
        [Route("DoctorConsultations/")]
        public async Task<Result<DoctorConsultationsDto>> GetDoctorConsultation()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (userId == null)
            {
                return Result<DoctorConsultationsDto>.Failure(DomainError.Unauthorized);
            }

            UserExternalInfoDto? UserInfo = await _userRepository.getExternalUserInfo(int.Parse(userId));
            if (UserInfo == null || UserInfo.Doctor == null)
            {
                return Result<DoctorConsultationsDto>.Failure(DoctorDomainError.NotFoundDoctor);
            }
            
            return await _consultationService.GetDoctorConsultation(UserInfo.Doctor);
        }

        /// <summary>
        /// Endpoint GET assíncrono para busca de consulta por paciente.
        /// </summary>
        /// <returns>Result<PatientConsultationsDto></returns>
        [HttpGet]
        [Authorize]
        [Route("PatientConsultations/")]
        public async Task<Result<PatientConsultationsDto>> GetPatientConsultation()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (userId == null)
            {
                return Result<PatientConsultationsDto>.Failure(DomainError.Unauthorized);
            }

            UserExternalInfoDto? UserInfo = await _userRepository.getExternalUserInfo(int.Parse(userId));
            if (UserInfo == null || UserInfo.Patient == null)
            {
                return Result<PatientConsultationsDto>.Failure(PatientDomainError.NotFoundPatient);
            }

            return await _consultationService.GetPatientConsultations(UserInfo.Patient.Id);
        }

        /// <summary>
        /// Endpoint GET assíncrono para busca de consulta por médico e paciente.
        /// </summary>
        /// <param name="DoctorId">Id do médico</param>
        /// <param name="PatientId">Id do paciente</param>
        /// <returns>Result<DoctorPatientConsultations></returns>
        [HttpGet]
        [Authorize]
        [Route("ConsultationByDoctorPatient/")]
        public async Task<Result<DoctorPatientConsultations>> GetConsultationByDoctorPatient(int DoctorId, int PatientId)
        {
            return await _consultationService.GetConsultationByDoctorPatient(DoctorId, PatientId);
        }

        /// <summary>
        /// Endpoint GET assíncrono que retonra o status da consulta por id.
        /// </summary>
        /// <param name="id">ID da consulta</param>
        /// <returns>IActionResult</returns>
        [HttpGet]
        [Authorize]
        [Route("ConsultationStatus/")]
        public async Task<Result<string>> GetConsultationStatusById(int id)
        {
            return await _consultationService.GetConsultationStatusById(id);
        }

        /// <summary>
        /// Endpoint POST assíncrono para criação de consultas.
        /// </summary>
        /// <param name="validator">validações da criação de consulta</param>
        /// <param name="dto">DTO do create consultation</param>
        /// <returns>IActionResult</returns>
        [HttpPost]
        [Authorize]
        [Route("CreateConsultation")]
        public async Task<Result<bool>> PostAsyncConsultation([FromBody] CreateConsultation dto)
        {
            if (!ModelState.IsValid)
                return Result<bool>.Failure(DomainError.GenericBadRequest);

            return await _consultationService.AddConsultation(dto);
        }

        /// <summary>
        /// Endpoint PATCH assíncrono para alteração de consultas.
        /// </summary>
        /// <param name="dto">DTO do update consultation</param>
        /// <returns>Result</returns>
        [HttpPatch]
        [Authorize]
        [Route("UpdateConsultation")]
        public async Task<Result<bool>> UpdateConsultation([FromBody] UpdateConsultation dto)
        {
            if (!ModelState.IsValid)
                return Result<bool>.Failure(DomainError.GenericBadRequest);

            return await _consultationService.UpdateConsultation(dto);
        }
    }
}
