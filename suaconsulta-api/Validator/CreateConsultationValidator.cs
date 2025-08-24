using FluentValidation;
using Microsoft.EntityFrameworkCore;
using suaconsulta_api.Application.DTO;
using suaconsulta_api.Infrastructure.Data;

namespace suaconsulta_api.Validator
{
    public class CreateConsultationValidator : AbstractValidator<CreateConsultation>
    {
        private readonly AppDbContext context;
        public CreateConsultationValidator(AppDbContext _context)
        {
            context = _context ?? throw new ArgumentNullException(nameof(_context));
            RuleFor(x => x.Date)
                .NotEmpty()
                    .WithMessage("Data é obrigatória")
                .GreaterThanOrEqualTo(DateTime.Now)
                    .WithMessage("Data informada é menor que a data atual!")
                .MustAsync((dto, data, cancellationToken) => DataConsultaDisponivelAgendaMedico(dto, data, cancellationToken))
                    .WithMessage("Data informada não está disponível na agenda do médico!")
                .MustAsync((dto, data, cancellationToken) => IsDataOcupada(dto, data, cancellationToken))
                    .WithMessage("Data informada não está disponível na agenda do médico!");
            RuleFor(x => x.PatientId)
                .NotEmpty()
                    .WithMessage("Paciente é obrigatório")
                .MustAsync((patientId, CancellationToken) => ExistsPatientAsync(patientId, CancellationToken))
                    .WithMessage("Paciente não cadastrado!");
            RuleFor(x => x.DoctorId)
                .NotEmpty()
                    .WithMessage("Médico é obrigatório")
                .MustAsync((doctorId, CancellationToken) => ExistsDoctorAsync(doctorId, CancellationToken))
                    .WithMessage("Médico não cadastrado!");
            RuleFor(x => x.Description)
                .NotEmpty()
                    .WithMessage("Descrição é obrigatória");
        }

        /// <summary>
        /// Verifica se a data informada está disponível na agenda do médico.
        /// </summary>
        /// <param name="dto">DTO do update consultation</param>
        /// <param name="data">data informada na requisição</param>
        /// <param name="cancellationToken">cancellation Token</param>
        /// <returns>bool</returns>
        private async Task<bool> DataConsultaDisponivelAgendaMedico(CreateConsultation dto, DateTime data, CancellationToken cancellationToken)
        {
            bool result = await context.DoctorSchedule
                .AnyAsync(x => x.StartTime.Date.Year == data.Year && 
                          x.StartTime.Date.Month == data.Month && 
                          x.StartTime.Date.Day == data.Day && 
                          x.StartTime.Hour == data.Hour &&
                          x.StartTime.Date.Minute == data.Minute && 
                          x.DoctorId == dto.DoctorId, cancellationToken); 

            return result;
        }

        /// <summary>
        /// Verifica se a data informada tem uma consulta já marcada.
        /// </summary>
        /// <param name="dto">DTO do update consultation</param>
        /// <param name="data">data informada na requisição</param>
        /// <param name="cancellationToken">cancellation Token</param>
        /// <returns>bool</returns>
        private async Task<bool> IsDataOcupada(CreateConsultation dto, DateTime data, CancellationToken cancellationToken)
        {
            bool result = await context.Consultation
                .AnyAsync(x => x.Date.Date.Year == data.Year &&
                  x.Date.Date.Month == data.Month &&
                  x.Date.Date.Day == data.Day &&
                  x.Date.Hour == data.Hour &&
                  x.Date.Minute == data.Minute &&
                  x.DoctorId == dto.DoctorId, cancellationToken);

            return !result;
        }

        /// <summary>
        /// Verifica se o paciente existe no banco de dados.
        /// </summary>
        /// <param name="PatientId">Id do paciente</param>
        /// <param name="cancellationToken">cancellation Token</param>
        /// <returns>bool</returns>
        private async Task<bool> ExistsPatientAsync(int PatientId, CancellationToken cancellationToken)
        {
            if (await context.Patient.FirstOrDefaultAsync(P => P.Id == PatientId, cancellationToken) == null)
            {
                return false;
            }
            return true;
        }

        /// <summary>
        /// Verifica se o médico existe no banco de dados.
        /// </summary>
        /// <param name="PatientId">Id do médico</param>
        /// <param name="cancellationToken">cancellation Token</param>
        /// <returns>bool</returns>
        private async Task<bool> ExistsDoctorAsync(int DoctorId, CancellationToken cancellationToken)
        {
            if (await context.Doctor.FirstOrDefaultAsync(D => D.Id == DoctorId) == null)
            {
                return false;
            }
            return true;
        }
    }
}

