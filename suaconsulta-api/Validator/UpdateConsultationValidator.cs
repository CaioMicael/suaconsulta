﻿using FluentValidation;
using FluentValidation.AspNetCore;
using suaconsulta_api.Model.Enum;

namespace suaconsulta_api.Validator
{
    public class UpdateConsultationValidator : AbstractValidator<DTO.UpdateConsultation>
    {
        public UpdateConsultationValidator()
        {
            RuleFor(x => x.Date)
                .NotEmpty()
                    .WithMessage("Data é obrigatória")
                .Must(date => date > DateTime.Now)
                    .WithMessage("Data inválida");
            RuleFor(x => x.Status)
                .NotEmpty()
                    .WithMessage("Status é obrigatório")
                .IsInEnum()
                    .WithMessage("Status inválido");
        }
    }
}
