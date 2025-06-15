using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using suaconsulta_api.Data;
using suaconsulta_api.Model;
using suaconsulta_api.Services;
using suaconsulta_api.Model.Enum;

namespace suaconsulta_api.Controllers
{
    [Route("api/Auth/")]
    public class ControllerAuth : ControllerBase
    {
        [HttpPost]
        [Route("SignUp")]
        public IActionResult SignUp([FromServices] AppDbContext context, [FromServices] JwtService jwtService, string Mail, string Pass, EnumTypeUsers TypeUser)
        {
            var hasher = new PasswordHasher<ModelUsers>();
            string hash = hasher.HashPassword(null, Pass);
            var user = new ModelUsers
            {
                ExternalId = 0, // Defina o valor adequado para ExternalId
                TypeUser = Model.Enum.EnumTypeUsers.Patient, // Defina o tipo de usuário conforme necessário
                Mail = Mail,
                Password = hash
            };
            context.Users.Add(user);
            context.SaveChanges();
            return CreatedAtAction(nameof(SignUp), new { id = user.Id }, user);
        }

        [HttpPost]
        [Route("Login/")]
        public IActionResult Login([FromServices] AppDbContext context, [FromServices] JwtService jwtService,[FromBody] LoginRequest request)
        {
            var user = context.Users.FirstOrDefault(u => u.Mail == request.Email);
            if (user == null)
            {
                return Unauthorized("Usuário não encontrado");
            }

            var hasher = new PasswordHasher<ModelUsers>();
            var result = hasher.VerifyHashedPassword(user, user.Password, request.Password);

            if (result != PasswordVerificationResult.Success)
                return Unauthorized("Senha incorreta");

            // Gera o JWT
            var token = jwtService.GenerateToken(user);

            return Ok(new { token });
        }
    }
}
