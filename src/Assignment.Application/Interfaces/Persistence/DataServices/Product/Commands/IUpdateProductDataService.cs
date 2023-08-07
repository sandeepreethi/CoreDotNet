using Assignment.Domain.Entities;

namespace Assignment.Application.Interfaces.Persistence.DataServices.Product.Commands;

public interface IUpdateProductDataService
{
    Task<Domain.Entities.Product> UpdateProduct(Domain.Entities.Product product, CancellationToken cancellationToken = default);
}

