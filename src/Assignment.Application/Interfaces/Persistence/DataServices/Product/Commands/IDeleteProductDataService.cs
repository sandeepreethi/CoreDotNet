using Assignment.Domain.Entities;

namespace Assignment.Application.Interfaces.Persistence.DataServices.Product.Commands;

public interface IDeleteProductDataService
{
    Task<Domain.Entities.Product> DeleteProduct(Domain.Entities.Product product, CancellationToken cancellationToken = default);
}

