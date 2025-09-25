using AutoMapper;
using API.Entities;
using API.Models.Base;
using API.Models.DTOs.Product;

namespace API.Helpers;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<Product, ProductDto>().ReverseMap();
        CreateMap<BaseListResult<Product>, BaseListResult<ProductDto>>();
    }
}
