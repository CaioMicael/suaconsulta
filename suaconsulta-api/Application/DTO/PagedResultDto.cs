namespace suaconsulta_api.Application.DTO
{
    /// <summary>
    /// Dto base para paginação de consultas. Toda consulta paginada contém estes atributos.
    /// </summary>
    /// <typeparam name="T">Entidade</typeparam>
    public class PagedResultDto<T>
    {
        public IEnumerable<T> Items { get; set; } = new List<T>();
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public int TotalCount { get; set; }
        public int TotalPages { get; set; }
    }
}
