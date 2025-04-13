using FluentValidation;
using FluentValidation.AspNetCore;
using suaconsulta_api.DTO;

namespace suaconsulta_api.Validator
{
    public class CreateConsultationValidator : AbstractValidator<DTO.CreateConsultation>
    {
        public CreateConsultationValidator()
        {
            RuleFor(Consultation => Consultation.Date)
                .NotEmpty()
                .WithMessage("Data é obrigatória")
                .Must(Date => Date > DateTime.Now)
                .WithMessage("Data inválida");
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
