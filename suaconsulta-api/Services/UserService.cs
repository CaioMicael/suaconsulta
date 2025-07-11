using Microsoft.AspNetCore.Mvc;
using suaconsulta_api.Data;

namespace suaconsulta_api.Services
{
    public class UserService
    {
        private readonly AppDbContext _context;

        public UserService(AppDbContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        /// <summary>
        /// Método estático serve para relacionar um external Id ao usuário logado.
        /// Por exemplo na criação de um usuário paciente, este método relaciona o Id do paciente com o usuário.
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="externalId"></param>
        public void RelateExternalId(string userId, int externalId)
        {
            var user = _context.Users.FirstOrDefault(u => u.Id.ToString() == userId);
            if (user != null)
            {
                if (user.ExternalId == null || user.ExternalId == 0)
                {
                    user.ExternalId = externalId;
                    _context.SaveChanges();
                }
                else
                {
                    throw new Exception("Usuário já possui um cadastro vinculado");
                }
            }
            else
            {
                throw new Exception("Usuário não encontrado");
            }
        }
    }
}