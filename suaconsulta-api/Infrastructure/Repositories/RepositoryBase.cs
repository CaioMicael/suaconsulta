using Microsoft.EntityFrameworkCore;
using suaconsulta_api.Application.DTO;
using suaconsulta_api.Infrastructure.Data;

namespace suaconsulta_api.Infrastructure.Repositories
{
    /// <summary>
    /// Classe repositório base para os demais repositórios.
    /// Contém recursos bases como paginação, ordenação, etc.
    /// </summary>
    public class RepositoryBase
    {
        private readonly AppDbContext _context;

        public RepositoryBase(AppDbContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        /// <summary>
        /// Retorna uma consulta paginada conforme Queryable repassado
        /// </summary>
        /// <typeparam name="T">Model</typeparam>
        /// <param name="query">Queryable carregado</param>
        /// <param name="page">Página desejada</param>
        /// <param name="pageSize">Tamanho da página</param>
        /// <returns>PagedResultDto</returns>
        public async Task<PagedResultDto<T>> GetPagedAsync<T>(IQueryable<T> query, int page, int pageSize)
        {
            var totalCount = await query.CountAsync();
            var items = await query
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            return new PagedResultDto<T>
            {
                Items = items,
                PageNumber = page,
                PageSize = pageSize,
                TotalCount = totalCount
            };
        }
    }
}