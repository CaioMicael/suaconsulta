using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using suaconsulta_api.Application.DTO;
using suaconsulta_api.Core.Common;
using suaconsulta_api.Domain.Errors;
using suaconsulta_api.Domain.Model;
using suaconsulta_api.Domain.Services;
using suaconsulta_api.Infrastructure.Repositories;
using System.Security.Claims;

namespace suaconsulta_api.Application.Controllers
{
    [Route("api/Doctor")]
    [ApiController]
    public class ControllerDoctor : ControllerBase
    {
        private readonly DoctorRepository _doctorRepository;
        private readonly DoctorService _doctorService;
        private readonly userRepository _userRepository;

        public ControllerDoctor(DoctorService doctorService, DoctorRepository doctorRepository, userRepository userRepository)
        {
            _doctorService = doctorService ?? throw new ArgumentNullException(nameof(doctorService));
            _doctorRepository = doctorRepository ?? throw new ArgumentNullException(nameof(doctorRepository));
            _userRepository = userRepository ?? throw new ArgumentNullException(nameof(userRepository));
        }

        [HttpGet]
        [Authorize]
        [Route("DoctorPage/")]
        public async Task<Result<PagedResultDto<ModelDoctor>>> GetAsyncDoctorPage([FromQuery] int Page, [FromQuery] int PageSize)
        {
            try
            {
                return await _doctorRepository.GetDoctorPage(Page, PageSize);
            }
            catch (Exception e)
            {
                throw new Exception("Erro ao criar página " + e.Message);
            }
        }

        [HttpGet]
        [Authorize]
        [Route("GetDoctor/")]
        public async Task<Result<ModelDoctor>> GetAsyncDoctorById([FromQuery] int id)
        {
            return await _doctorService.GetDoctorById(id);
        }

        [HttpPut]
        [Authorize]
        [Route("UpdateDoctor/")]
        public async Task<Result<bool>> PutAsyncDoctor([FromBody] UpdateDoctorDto dto)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (userId == null)
            {
                return Result<bool>.Failure(UserDomainError.NotFoundUser);
            }

            UserExternalInfoDto? UserInfo = await _userRepository.getExternalUserInfo(int.Parse(userId));
            if (UserInfo == null || UserInfo.Doctor == null)
            {
                return Result<bool>.Failure(DoctorDomainError.NotFoundDoctor);
            }

            return await _doctorService.UpdateDoctorDto(dto, UserInfo.Doctor.Id);
        }
    }
}
