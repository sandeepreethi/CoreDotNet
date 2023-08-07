using Assignment.Application.Models;
using MediatR;

namespace Assignment.Application.Endpoints.Product.Commands;

public class DeleteProductCommand : IRequest<EndpointResult<ProductViewModel>>
{
    public int Id { get; set; } 
}
