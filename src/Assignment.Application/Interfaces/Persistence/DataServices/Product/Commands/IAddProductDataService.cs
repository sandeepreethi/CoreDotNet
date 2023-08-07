using Assignment.Domain.Entities;

namespace Assignment.Application.Interfaces.Persistence.DataServices.Product.Commands;

public interface IAddProductDataService
{
    Task<Domain.Entities.Product> AddProduct(Domain.Entities.Product product, CancellationToken cancellationToken = default);
}

