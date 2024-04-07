using AutoMapper;
using StationeryStore.Dto;
using StationeryStore.Models;

namespace StationeryStore.Utils
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            CreateMap<User, UserDto>().ReverseMap();
            CreateMap<User, SignUpDto>().ReverseMap();
            CreateMap<User, RegisterUserDto>().ReverseMap();
            CreateMap<SignUpDto, RegisterUserDto>().ReverseMap();
            CreateMap<User, LoginDto>().ReverseMap();
            CreateMap<Address, AddressDto>().ReverseMap();
            CreateMap<ResCategoryDto, Category>().ReverseMap();
            CreateMap<ResSubCategoryDto, SubCategory>().ReverseMap();
            CreateMap<ReqCategoryDto, Category>().ReverseMap();
            CreateMap<SubCategory, ReqSubCategoryDto>().ReverseMap();
            CreateMap<Product, ResProductDto>().ReverseMap();
            CreateMap<Product, ReqProductDto>().ReverseMap();
            CreateMap<Review, ResReviewDto>().ReverseMap();
            CreateMap<Review, ReqReviewDto>().ReverseMap();
            CreateMap<Rate, ReqRateDto>().ReverseMap();
            CreateMap<Rate, ResRateDto>().ReverseMap();
            CreateMap<ProductAttributeQuantity, ReqProductAttributeQuantityDto>().ReverseMap();

        }
    }
}
