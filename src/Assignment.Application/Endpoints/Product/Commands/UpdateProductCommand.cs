using Assignment.Application.Models;
using MediatR;

namespace Assignment.Application.Endpoints.Product.Commands;

public class UpdateProductCommand : IRequest<EndpointResult<ProductViewModel>>
{
    public int Id { get; init; } 
    public string Name { get; init; } = "";
    public string Description { get; init; } = "";
    public int? Price { get; set; } 
}
