using FluentValidation;
using Microsoft.EntityFrameworkCore;
using suaconsulta_api.Data;
using suaconsulta_api.DTO;

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
                .MustAsync((dto, data, cancellationToken) => DataConsultaDisponivel(dto, data, cancellationToken))
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

        private async Task<bool> DataConsultaDisponivel(CreateConsultation dto, DateTime data, CancellationToken cancellationToken)
        {
            return await context.DoctorSchedule
                .AnyAsync(x => x.StartTime.Date.Year == data.Year && 
                          x.StartTime.Date.Month == data.Month && 
                          x.StartTime.Date.Day == data.Day && 
                          x.StartTime.Hour == data.Hour &&
                          x.StartTime.Date.Minute == data.Minute && 
                          x.DoctorId == dto.DoctorId, cancellationToken); 
        }

        private async Task<bool> ExistsPatientAsync(int PatientId, CancellationToken cancellationToken)
        {
            if (await context.Patient.FirstOrDefaultAsync(P => P.Id == PatientId) == null)
            {
                return false;
            }
            return true;
        }

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

