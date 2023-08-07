using AutoMapper;
using Assignment.Application.Endpoints.Product;
using Assignment.Application.Endpoints.Product.Commands;
using Assignment.Domain.Entities;
using Assignment.Application.Endpoints.Product.Queries;

namespace Assignment.Application.Mapping;

public class ProductProfile : Profile
{
    public ProductProfile()
    {
        CreateMap<Product, ProductViewModel>().ReverseMap();


        CreateMap<AddProductCommand, Product>()
            .ForMember(dest => dest.Active, opt => opt.MapFrom(src => true))
            .ForMember(dest => dest.CreatedBy, opt => opt.Ignore())
            .ForMember(dest => dest.CreatedOn, opt => opt.Ignore())
            .ForMember(dest => dest.ModifiedBy, opt => opt.Ignore())
            .ForMember(dest => dest.ModifiedOn, opt => opt.Ignore());
        CreateMap<UpdateProductCommand, Product>()
            .ForMember(dest => dest.Active, opt => opt.MapFrom(src => true))
            .ForMember(dest => dest.CreatedBy, opt => opt.Ignore())
            .ForMember(dest => dest.CreatedOn, opt => opt.Ignore())
            .ForMember(dest => dest.ModifiedBy, opt => opt.Ignore())
            .ForMember(dest => dest.ModifiedOn, opt => opt.Ignore());
        CreateMap<DeleteProductCommand, Product>()
           .ForMember(dest => dest.Active, opt => opt.MapFrom(src => true))
           .ForMember(dest => dest.CreatedBy, opt => opt.Ignore())
           .ForMember(dest => dest.CreatedOn, opt => opt.Ignore())
           .ForMember(dest => dest.ModifiedBy, opt => opt.Ignore())
           .ForMember(dest => dest.ModifiedOn, opt => opt.Ignore());
    }
}
