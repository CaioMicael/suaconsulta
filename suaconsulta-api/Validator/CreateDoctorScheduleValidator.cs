using FluentValidation;
using Microsoft.EntityFrameworkCore;
using suaconsulta_api.Data;
using suaconsulta_api.DTO;

namespace suaconsulta_api.Validator
{
    public class CreateDoctorScheduleValidator : AbstractValidator<CreateDoctorScheduleDto>
    {
        private readonly AppDbContext context;

        public CreateDoctorScheduleValidator(AppDbContext _context)
        {
            context = _context ?? throw new ArgumentNullException(nameof(_context));
            RuleFor(x => x.StartTime)
                .NotEmpty()
                    .WithMessage("Data é obrigatória")
                .GreaterThanOrEqualTo(DateTime.Now)
                    .WithMessage("Data informada é menor que a data atual!")
                .LessThan(x => x.EndTime)
                    .WithMessage("Data de inicio deve ser menor que a data de fim!");
            RuleFor(x => x.DoctorId)
                .MustAsync((DoctorId, CancellationToken) => ExistsDoctorAsync(DoctorId, CancellationToken))
                     .WithMessage("Médico não cadastrado!");
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
