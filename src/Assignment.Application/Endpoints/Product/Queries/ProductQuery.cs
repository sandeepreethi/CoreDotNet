using Assignment.Application.Models;
using MediatR;

namespace Assignment.Application.Endpoints.Product.Queries;

public class ProductQuery : IRequest<EndpointResult<IEnumerable<ProductViewModel>>>
{
    public bool IncludeInactive { get; init; } = false;
    public int? Id { get; init; }
}

