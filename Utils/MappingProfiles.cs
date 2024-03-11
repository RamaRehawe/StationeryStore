using AutoMapper;
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
        }
    }
}
