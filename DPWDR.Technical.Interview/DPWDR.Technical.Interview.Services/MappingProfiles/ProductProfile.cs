using AutoMapper;
using DPWDR.Technical.Interview.Data.Entities;
using DPWDR.Technical.Interview.Services.DTOS;

namespace DPWDR.Technical.Interview.Services.MappingProfiles
{
    public class ProductProfile : Profile
    {
        public ProductProfile()
        {
            CreateMap<ProductDTO, Product>();
        }
    }
}
