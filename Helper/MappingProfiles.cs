﻿using AutoMapper;
using StationeryStore.Dto;
using StationeryStore.Models;

namespace StationeryStore.Utils
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            CreateMap<Product, ProductDto>();
            CreateMap<User, UserDto>();
            CreateMap<UserDto, User>();
            CreateMap<User, LoginDto>();
            CreateMap<LoginDto, User>();
            CreateMap<Address, AddressDto>();
            CreateMap<AddressDto, Address>();
            CreateMap<Category, CategoryDto>();
            CreateMap<CategoryDto, Category>();
            CreateMap<SubCategory, SubCategoryDto>();
            CreateMap<SubCategoryDto, SubCategory>();
        }
    }
}
