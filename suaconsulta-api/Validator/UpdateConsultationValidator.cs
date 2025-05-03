using FluentValidation;
using Microsoft.EntityFrameworkCore;
using suaconsulta_api.Data;
using suaconsulta_api.DTO;

namespace suaconsulta_api.Validator
{
    public class UpdateConsultationValidator : AbstractValidator<UpdateConsultation>
    {
        private readonly AppDbContext context;

        public UpdateConsultationValidator(AppDbContext _context)
        {
            context = _context ?? throw new ArgumentNullException(nameof(_context));

            RuleFor(x => x.Date)
                .NotEmpty()
                    .WithMessage("Data é obrigatória")
                .Must(date => date > DateTime.Now)
                    .WithMessage("Data inválida")
                .MustAsync((dto, data, cancellationToken) => DataConsultaDisponivelAgendaMedico(dto, data, cancellationToken))
                    .WithMessage("Data informada não está disponível na agenda do médico!")
                .MustAsync((dto, data, cancellationToken) => isDataOcupada(dto, data, cancellationToken))
                    .WithMessage("Data informada não está disponível na agenda do médico!");
            RuleFor(x => x.Status)
                .NotEmpty()
                    .WithMessage("Status é obrigatório")
                .IsInEnum()
                    .WithMessage("Status inválido");
        }

        /// <summary>
        /// Verifica se a data informada está disponível na agenda do médico.
        /// </summary>
        /// <param name="dto">DTO do update consultation</param>
        /// <param name="data">data informada na requisição</param>
        /// <param name="cancellationToken">cancellation Token</param>
        /// <returns>bool</returns>
        private async Task<bool> DataConsultaDisponivelAgendaMedico(UpdateConsultation dto, DateTime data, CancellationToken cancellationToken)
        {
            int DoctorId = await context.Consultation
                .Where(x => x.Id == dto.Id)
                .Select(x => x.DoctorId)
                .FirstOrDefaultAsync(cancellationToken);

            bool result = await context.DoctorSchedule
                .AnyAsync(x => x.StartTime.Date.Year == data.Year && 
                          x.StartTime.Date.Month == data.Month && 
                          x.StartTime.Date.Day == data.Day && 
                          x.StartTime.Hour == data.Hour &&
                          x.StartTime.Date.Minute == data.Minute && 
                          x.DoctorId == DoctorId, cancellationToken); 

            return result;
        }

        /// <summary>
        /// Verifica se a data informada tem uma consulta já marcada.
        /// </summary>
        /// <param name="dto">DTO do update consultation</param>
        /// <param name="data">data informada na requisição</param>
        /// <param name="cancellationToken">cancellation Token</param>
        /// <returns>bool</returns>
        private async Task<bool> isDataOcupada(UpdateConsultation dto, DateTime data, CancellationToken cancellationToken)
        {
            int DoctorId = await context.Consultation
                .Where(x => x.Id == dto.Id)
                .Select(x => x.DoctorId)
                .FirstOrDefaultAsync(cancellationToken);

            bool result = await context.Consultation
                .AnyAsync(x => x.Date.Date.Year == data.Year &&
                  x.Date.Date.Month == data.Month &&
                  x.Date.Date.Day == data.Day &&
                  x.Date.Hour == data.Hour &&
                  x.Date.Minute == data.Minute &&
                  x.DoctorId == DoctorId, cancellationToken);

            return !result;
        }
    }
}
