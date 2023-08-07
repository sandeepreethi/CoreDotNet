using AutoMapper;
using Assignment.Application.Interfaces;
using Assignment.Application.Interfaces.Persistence.DataServices.Product.Commands;
using Assignment.Application.Models;
using Assignment.Application.Models.Enumerations;
using Assignment.Domain.Entities;
using MediatR;

namespace Assignment.Application.Endpoints.Product.Commands;

public class UpdateProductCommandHandler : IRequestHandler<UpdateProductCommand, EndpointResult<ProductViewModel>>
{
    public readonly IRequestValidator<UpdateProductCommand> _requestValidator;
    public readonly IUpdateProductDataService _updateProductDataService;
    public readonly IMapper _mapper;

    public UpdateProductCommandHandler(
        IRequestValidator<UpdateProductCommand> requestValidator,
        IUpdateProductDataService updateProductDataService,
        IMapper mapper
    )
    {
        _requestValidator = requestValidator;
        _updateProductDataService = updateProductDataService;
        _mapper = mapper;
    }

    public async Task<EndpointResult<ProductViewModel>> Handle(UpdateProductCommand request, CancellationToken cancellationToken)
    {
        var validationErrors = _requestValidator.ValidateRequest(request);
        if (validationErrors.Count() > 0)
            return new EndpointResult<ProductViewModel>(EndpointResultStatus.Invalid, validationErrors.ToArray());

        var product = await _updateProductDataService.UpdateProduct(_mapper.Map<Domain.Entities.Product>(request));
        return new EndpointResult<ProductViewModel>(_mapper.Map<ProductViewModel>(product));
    }
}
