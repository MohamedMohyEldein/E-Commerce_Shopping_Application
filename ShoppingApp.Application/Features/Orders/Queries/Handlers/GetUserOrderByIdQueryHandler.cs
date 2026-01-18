using AutoMapper;
using MediatR;
using ShoppingApp.Application.Features.Orders.DTOs;
using ShoppingApp.Domain.Exceptions;

namespace ShoppingApp.Application.Features.Orders.Queries.Handlers
{
    public class GetUserOrderByIdQueryHandler : IRequestHandler<GetUserOrderByIdQuery, OrderDto?>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetUserOrderByIdQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<OrderDto?> Handle(GetUserOrderByIdQuery request, CancellationToken cancellationToken)
        {
            if(request is null)
            {
                throw new BadRequestException ("Request object cannot be null.");
            }
            var order = await _unitOfWork.OrderRepository.GetUserOrderByIdAsync(request.OrderId, request.UserId);
            return _mapper.Map<OrderDto?>(order);
        }
    }
}
