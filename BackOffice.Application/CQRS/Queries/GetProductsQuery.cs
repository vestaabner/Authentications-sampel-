using BackOffice.Application.Dtos;
using MediatR;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BackOffice.Application.CQRS.Queries
{
    public class GetProductsQuery : IRequest<GetProductsQueryResponseDto>
    {
        public GetProductsQuery()
        {
                
        }
    }
    public class GetProductsQueryHandler : IRequestHandler<GetProductsQuery, GetProductsQueryResponseDto>
    {
        private readonly IMediator mediator;

        public GetProductsQueryHandler( IMediator mediator)
        {
            this.mediator = mediator;
        }
        public async Task<GetProductsQueryResponseDto> Handle(GetProductsQuery request, CancellationToken cancellationToken)
        {
            var response = new GetProductsQueryResponseDto { };
            return response;
        }
    }

}
