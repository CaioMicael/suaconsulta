﻿using System.ComponentModel.DataAnnotations;

namespace suaconsulta_api.DTO
{
    public class DeletePatientDto
    {
        [Required(ErrorMessage = "O id é obrigatório.")]
        public int Id { get; set; }
    }
}
