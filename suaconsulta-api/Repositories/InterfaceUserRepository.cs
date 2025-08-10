namespace suaconsulta_api.Repositories
{
    using suaconsulta_api.Model;

    /// <summary>
    /// Interface para o repositório de usuários.
    /// </summary>
    public interface InterfaceUserRepository
    {
        /// <summary>
        /// Método para buscar um usuário pelo ID.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<ModelUsers?> getUserById(int id);

        /// <summary>
        /// Retorna o usuário com o email fornecido
        /// </summary>
        /// <param name="email">string</param>
        /// <returns></returns>
        Task<ModelUsers?> GetUserByEmail(string email);

        /// <summary>
        /// Método para relacionar um external Id ao usuário.
        /// Por exemplo, na criação de um usuário paciente, este método relaciona o Id do paciente com o usuário.
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="externalId"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        void setExternalId(int userId, int externalId);

        /// <summary>
        /// Insere um novo usuário no repositório.
        /// </summary>
        /// <param name="user"></param>
        Task InsertUser(ModelUsers user);
    }
}