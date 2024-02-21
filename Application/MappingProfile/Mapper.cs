using Application.Dto;
using AutoMapper;
using Domain.Entities;
using Domain.Interfaces;
using Domain.ValueObjects;
using Infrastructure.Repository;

namespace Application.MappingProfile
{
    public class Mapper : Profile
    {
        public Mapper(IPasswordHasher passwordHasher)
        {
            CreateMap<Product, ProductDto>();
            CreateMap<ProductDto, Product>();
            CreateMap<UserDto, User>().AfterMap((src, dest) => {
                dest.Password = passwordHasher.HashPassword(src.Password);
            });
            CreateMap<User, UserDto>();
            CreateMap<TokenObject, TokenDto>();
            CreateMap<CartDto, Cart>();
            CreateMap<Cart, CartDto>();
        }
    }
}
