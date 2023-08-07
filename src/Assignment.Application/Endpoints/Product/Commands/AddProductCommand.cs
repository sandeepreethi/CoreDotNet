using Assignment.Application.Models;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace Assignment.Application.Endpoints.Product.Commands;

public class AddProductCommand : IRequest<EndpointResult<ProductViewModel>>
{
    public string Name { get; init; } = "";
    public string Description { get; init; } = "";
    public int? Price { get; set; }
    
    

}
