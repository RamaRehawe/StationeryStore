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
            CreateMap<Cart, ResCartDto>().ReverseMap();
            CreateMap<Cart, ReqCartDto>().ReverseMap();
            CreateMap<CartItem, CartItemDto>().ReverseMap();
            CreateMap<ProductAttributeQuantity, ReqProductAttributeQuantityDto>().ReverseMap();
            CreateMap<ProductAttributeQuantity, ResProductAttributeQuantityDto>().ReverseMap();
            CreateMap<Order, PlaceOrderDto>().ReverseMap();
            CreateMap<Order, ResOrderDto>().ReverseMap();
            CreateMap<OrderItem, ResOrderItemDto>().ReverseMap();
            CreateMap<Driver, ReqDriverDto>().ReverseMap();
            CreateMap<Order, PendingOrderDto>().ReverseMap();
            //CreateMap<User, PendingOrderDto>().ReverseMap();
            //CreateMap<Address, PendingOrderDto>().ReverseMap();
            CreateMap<Atribute, ReqAttributeDto>().ReverseMap();
            CreateMap<Atribute, ResAttributeDto>().ReverseMap();
            CreateMap<ProductAttribute, ReqProductAttributeDto>().ReverseMap();
            CreateMap<ProductAttribute, ResProductAttributeDto>().ReverseMap();
            CreateMap<ContactInformation, ReqContactInformationDto>().ReverseMap();
            CreateMap<ContactInformation, ResContactInformationDto>().ReverseMap();
            CreateMap<ImageAttribute, ReqImageAttributeDto>().ReverseMap();

        }
    }
}
