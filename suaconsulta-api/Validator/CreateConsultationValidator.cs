using FluentValidation;
using FluentValidation.AspNetCore;
using suaconsulta_api.DTO;

namespace suaconsulta_api.Validator
{
    public class CreateConsultationValidator : AbstractValidator<DTO.CreateConsultation>
    {
        public CreateConsultationValidator()
        {
            RuleFor(x => x.Date)
                .NotEmpty()
                    .WithMessage("Data é obrigatória")
                .GreaterThanOrEqualTo(DateTime.Now)
                    .WithMessage("Data informada é menor que a data atual!");
            RuleFor(x => x.PatientId)
                .NotEmpty()
                    .WithMessage("Paciente é obrigatório");
            RuleFor(x => x.DoctorId)
                .NotEmpty()
                    .WithMessage("Médico é obrigatório");
            RuleFor(x => x.Description)
                .NotEmpty()
                    .WithMessage("Descrição é obrigatória");
        }
    }
}
