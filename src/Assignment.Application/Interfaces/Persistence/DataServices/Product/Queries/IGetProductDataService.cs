using Assignment.Domain.Entities;

namespace Assignment.Application.Interfaces.Persistence.DataServices.Product.Queries;

public interface IGetProductDataService
{
     Task<IEnumerable<Domain.Entities.Product>> GetAllProducts(bool includeInactive, CancellationToken cancellationToken = default);
    Task<IEnumerable<Domain.Entities.Product>> GetProductsById(int? id, bool includeInactive, CancellationToken cancellationToken = default);
}
