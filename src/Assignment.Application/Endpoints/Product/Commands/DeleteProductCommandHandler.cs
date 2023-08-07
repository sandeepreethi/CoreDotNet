using AutoMapper;
using Assignment.Application.Interfaces;
using Assignment.Application.Interfaces.Persistence.DataServices.Product.Commands;
using Assignment.Application.Models;
using Assignment.Application.Models.Enumerations;
using Assignment.Domain.Entities;
using MediatR;

namespace Assignment.Application.Endpoints.Product.Commands;

public class DeleteProductCommandHandler : IRequestHandler<DeleteProductCommand, EndpointResult<ProductViewModel>>
{
    public readonly IRequestValidator<DeleteProductCommand> _requestValidator;
    public readonly IDeleteProductDataService _deleteProductDataService;
    public readonly IMapper _mapper;

    public DeleteProductCommandHandler(
        IRequestValidator<DeleteProductCommand> requestValidator,
        IDeleteProductDataService deleteProductDataService,
        IMapper mapper
    )
    {
        _requestValidator = requestValidator;
        _deleteProductDataService = deleteProductDataService;
        _mapper = mapper;
    }

    public async Task<EndpointResult<ProductViewModel>> Handle(DeleteProductCommand request, CancellationToken cancellationToken)
    {
        var validationErrors = _requestValidator.ValidateRequest(request);
        if (validationErrors.Count() > 0)
            return new EndpointResult<ProductViewModel>(EndpointResultStatus.Invalid, validationErrors.ToArray());

        var product = await _deleteProductDataService.DeleteProduct(_mapper.Map<Domain.Entities.Product>(request));
        return new EndpointResult<ProductViewModel>(_mapper.Map<ProductViewModel>(product));
    }
}
