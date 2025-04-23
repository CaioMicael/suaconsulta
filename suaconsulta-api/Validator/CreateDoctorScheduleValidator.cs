using FluentValidation;
using suaconsulta_api.DTO;

namespace suaconsulta_api.Validator
{
    public class CreateDoctorScheduleValidator : AbstractValidator<CreateDoctorScheduleDto>
    {
        public CreateDoctorScheduleValidator()
        {
            RuleFor(x => x.StartTime)
                .NotEmpty()
                    .WithMessage("Data é obrigatória")
                .GreaterThanOrEqualTo(DateTime.Now)
                    .WithMessage("Data informada é menor que a data atual!")
                .LessThan(x => x.EndTime)
                    .WithMessage("Data de inicio deve ser menor que a data de fim!");
        }
    }
}
