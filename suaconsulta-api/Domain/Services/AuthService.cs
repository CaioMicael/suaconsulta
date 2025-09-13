using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Storage;
using suaconsulta_api.Application.DTO;
using suaconsulta_api.Core.Common;
using suaconsulta_api.Domain.Errors;
using suaconsulta_api.Domain.Model;
using suaconsulta_api.Domain.Model.Enum;
using suaconsulta_api.Infrastructure.Repositories;
using suaconsulta_api.Migrations;

namespace suaconsulta_api.Domain.Services
{
    public class AuthService : InterfaceAuthService
    {
        private readonly AuthRepository authRepository;
        private readonly InterfaceUserRepository userRepository;
        private readonly JwtService jwtService;
        private readonly PatientService _patientService;
        private readonly DoctorService _doctorService;

        public AuthService(
            AuthRepository authRepository,
            InterfaceUserRepository userRepository,
            JwtService jwtService,
            PatientService patientService,
            DoctorService doctorService
        )
        {
            this.authRepository = authRepository ?? throw new ArgumentNullException(nameof(authRepository));
            this.userRepository = userRepository ?? throw new ArgumentNullException(nameof(userRepository));
            this.jwtService = jwtService ?? throw new ArgumentNullException(nameof(jwtService));
            _patientService = patientService ?? throw new ArgumentNullException(nameof(patientService));
            _doctorService = doctorService ?? throw new ArgumentNullException(nameof(doctorService));
        }

        public async Task<bool> isEmailAlreadyRegister(SignUpDto dto)
        {
            var user = await authRepository.getUserByEmail(dto.mail);
            return user != null;
        }

        public bool isPasswordValid(string password)
        {
            return password != null && password.Length >= 6;
        }

        /// <summary>
        /// Realiza a criação de um usuário
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        public async Task<Result<TokenInformationDto>> DoSignUp(SignUpDto dto)
        {
            if (await isEmailAlreadyRegister(dto))
            {
                return Result<TokenInformationDto>.Failure(AuthDomainError.EmailAlreadyRegister);
            }

            if (!isPasswordValid(dto.pass))
            {
                return Result<TokenInformationDto>.Failure(AuthDomainError.InvalidPassword);
            }

            var hasher = new PasswordHasher<ModelUsers>();
            string hash = hasher.HashPassword(null, dto.pass);
            ModelUsers user = new ModelUsers
            {
                ExternalId = 0,
                TypeUser = dto.TypeUser,
                Mail = dto.mail,
                Password = hash
            };

            using (IDbContextTransaction transaction = await userRepository.BeginTransaction())
            {
                try
                {
                    int externalId;
                    await userRepository.InsertUser(user);

                    switch (dto.TypeUser)
                    {
                        case EnumTypeUsers.Doctor:
                            if (dto.DoctorDto == null)
                                return Result<TokenInformationDto>.Failure(DoctorDomainError.NotFoundDoctor);
                            ModelDoctor doctor = await _doctorService.CreateDoctorDto(dto.DoctorDto);
                            externalId = doctor.Id;
                            break;

                        case EnumTypeUsers.Patient:
                            if (dto.PatientDto == null)
                                return Result<TokenInformationDto>.Failure(PatientDomainError.PatientNullBadRequest);
                            ModelPatient patient = await _patientService.CreatePatient(dto.PatientDto);
                            externalId = patient.Id;
                            break;

                        default:
                            return Result<TokenInformationDto>.Failure(AuthDomainError.InvalidTypeUser);
                    }

                    await userRepository.setExternalId(user.Id, externalId);
                    await transaction.CommitAsync();
                }
                catch (Exception)
                {
                    await transaction.RollbackAsync();
                    throw;
                }
            }

            // Gera o JWT
            var token = jwtService.GenerateToken(user);
            TokenInformationDto resultObject = new TokenInformationDto { token = token, role = user.TypeUser };
            return Result<TokenInformationDto>.Success(resultObject);
        }

        public async Task<IActionResult> DoLogin([FromBody] LoginRequest request)
        {
            ModelUsers? user = await userRepository.GetUserByEmail(request.Email);
            if (user == null)
            {
                return new NotFoundObjectResult("Usuário não encontrado");
            }

            var hasher = new PasswordHasher<ModelUsers>();
            var result = hasher.VerifyHashedPassword(user, user.Password, request.Password);

            if (result == PasswordVerificationResult.Failed)
            {
                return new BadRequestObjectResult("Senha incorreta");
            }

            var token = jwtService.GenerateToken(user);
            TokenInformationDto resultObject = new TokenInformationDto { token = token, role = user.TypeUser };
            return new OkObjectResult(resultObject);
        }
    }
}