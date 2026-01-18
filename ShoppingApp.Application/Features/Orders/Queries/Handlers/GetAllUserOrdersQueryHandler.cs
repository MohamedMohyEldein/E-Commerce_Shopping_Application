using AutoMapper;
using MediatR;
using ShoppingApp.Application.Features.Orders.DTOs;
using ShoppingApp.Domain.Exceptions;

namespace ShoppingApp.Application.Features.Orders.Queries.Handlers
{
    public class GetAllUserOrdersQueryHandler : IRequestHandler<GetAllUserOrdersQuery, List<OrderDto>?>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetAllUserOrdersQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<List<OrderDto>?> Handle(GetAllUserOrdersQuery request, CancellationToken cancellationToken)
        {
            if(request is null || request.UserId is null)
            {
                throw new BadRequestException ("Invalid request: UserId is required.");
            }
            var orders = await _unitOfWork.OrderRepository.GetAllUserOrdersAsync(request.UserId);
            return _mapper.Map<List<OrderDto>?>(orders);
        }
    }
}
