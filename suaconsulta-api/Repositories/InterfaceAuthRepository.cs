namespace suaconsulta_api.Repositories
{
    using suaconsulta_api.Domain.Model;

    public interface InterfaceAuthRepository
    {
        /// <summary>
        /// Método para buscar um usuário pelo email.
        /// </summary>
        /// <param name="email"></param>
        /// <returns></returns>
        Task<ModelUsers?> getUserByEmail(string email);

        /// <summary>
        /// Método para buscar um usuário pelo ID.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<ModelUsers?> getUserById(int id);
    }
}