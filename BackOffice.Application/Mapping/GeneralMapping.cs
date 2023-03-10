using System;
using AutoMapper;
using BackOffice.Application.Dtos;
using BackOffice.Domain.Entities;

namespace BackOffice.Application.Mapping
{
    public class GeneralMapping : Profile
    {

        public GeneralMapping()
        {
            CreateMap<ProductDto, Product>().ReverseMap();
        }

    }
}

