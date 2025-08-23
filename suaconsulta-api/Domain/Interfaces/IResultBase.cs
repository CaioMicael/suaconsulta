using suaconsulta_api.Domain.Errors;

namespace suaconsulta_api.Domain.Interfaces
{
    public interface IResultBase
    {
        bool IsSuccess { get; }
        DomainError Error { get; }
        object GetValue();
    }
}