using Microsoft.EntityFrameworkCore;
using suaconsulta_api.Data;

namespace suaconsulta_api.Repositories
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

        public async Task<PagedResult<T>> GetPagedAsync<T>(IQueryable<T> query, int page, int pageSize)
        {
            var totalCount = await query.CountAsync();
            var items = await query
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            return new PagedResult<T>(items, totalCount, page, pageSize);
        }

    }
}