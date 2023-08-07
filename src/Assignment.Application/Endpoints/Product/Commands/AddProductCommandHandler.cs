using AutoMapper;
using Assignment.Application.Interfaces;
using Assignment.Application.Interfaces.Persistence.DataServices.Product.Commands;
using Assignment.Application.Models;
using Assignment.Application.Models.Enumerations;
using Assignment.Domain.Entities;
using MediatR;

namespace Assignment.Application.Endpoints.Product.Commands;

public class AddProductCommandHandler : IRequestHandler<AddProductCommand, EndpointResult<ProductViewModel>>
{
    public readonly IRequestValidator<AddProductCommand> _requestValidator;
    public readonly IAddProductDataService _addProductDataService;
    public readonly IMapper _mapper;

    public AddProductCommandHandler(
        IRequestValidator<AddProductCommand> requestValidator,
        IAddProductDataService addProductDataService,
        IMapper mapper
    )
    {
        _requestValidator = requestValidator;
        _addProductDataService = addProductDataService;
        _mapper = mapper;
    }

    public async Task<EndpointResult<ProductViewModel>> Handle(AddProductCommand request, CancellationToken cancellationToken)
    {
        var validationErrors = _requestValidator.ValidateRequest(request);
        if (validationErrors.Count() > 0)
            return new EndpointResult<ProductViewModel>(EndpointResultStatus.Invalid, validationErrors.ToArray());

        var product = await _addProductDataService.AddProduct(_mapper.Map<Domain.Entities.Product>(request));
        return new EndpointResult<ProductViewModel>(_mapper.Map<ProductViewModel>(product));
    }
}
