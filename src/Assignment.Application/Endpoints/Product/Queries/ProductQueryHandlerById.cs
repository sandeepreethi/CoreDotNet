using AutoMapper;
using Assignment.Application.Interfaces.Persistence.DataServices.Product.Queries;
using Assignment.Application.Models;
using MediatR;

namespace Assignment.Application.Endpoints.Product.Queries;

public class ProductQueryHandlerById : IRequestHandler<ProductQuery, EndpointResult<IEnumerable<ProductViewModel>>>
{
    public readonly IGetProductDataService _getProductDataService;
    public readonly IMapper _mapper;

    public ProductQueryHandlerById(IGetProductDataService getProductDataService, IMapper mapper)
    {
        _getProductDataService = getProductDataService;
        _mapper = mapper;
    }

    public async Task<EndpointResult<IEnumerable<ProductViewModel>>> Handle(ProductQuery request, CancellationToken cancellationToken = default)
    {
        var product = await _getProductDataService.GetProductsById(request.Id, request.IncludeInactive, cancellationToken);

        return new EndpointResult<IEnumerable<ProductViewModel>>(_mapper.Map<ProductViewModel[]>(product));
    }

}
