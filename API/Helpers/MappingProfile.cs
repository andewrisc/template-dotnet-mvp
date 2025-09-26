using AutoMapper;
using API.Entities;
using API.Models.Base;
using API.Models.DTOs.Product;
using API.Models.Parameters.Product;

namespace API.Helpers;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        //Product
        CreateMap<Product, ProductDto>().ReverseMap();
        CreateMap<BaseListResult<Product>, BaseListResult<ProductDto>>();
        CreateMap<ProductCreateParameters, Product>();
        CreateMap<ProductUpdateParameters, Product>();
    }
}
