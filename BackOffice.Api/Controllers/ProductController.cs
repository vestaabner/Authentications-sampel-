using System;
using AutoMapper;
using BackOffice.Application.CQRS.Queries;
using BackOffice.Application.Dtos;
using BackOffice.Infrastructure.UnitOfWork;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BackOffice.Api.Controllers
{
    [ApiController]
    [Route("/api/[Controller]")]
    //[Authorize(Roles = "Admin")]
    public class ProductController : ControllerBase
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly ILogger<ProductController> _logger;
        public IMapper mapper;
       private readonly IMediator mediator;
        public ProductController(IUnitOfWork unitOfWork, ILogger<ProductController> logger, IMapper mapper, IMediator mediator)
        {
            this.unitOfWork = unitOfWork;
            _logger = logger;
            this.mapper = mapper;
            this.mediator = mediator;
        }

        [HttpGet()]
        public async Task<GetProductsQueryResponseDto> GetProducts()
        {
            var query = new GetProductsQuery();
             var result = await mediator.Send(query);
            return result;
        }

        [HttpPost("/add")]
        public async Task<IActionResult> AddProduct(ProductDto dto)
        {
            return Ok(await unitOfWork.ProductRepository.AddAsync(dto));
        }
    }
}

