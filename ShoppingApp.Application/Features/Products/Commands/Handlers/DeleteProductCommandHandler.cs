using MediatR;
using ShoppingApp.Domain.Exceptions;

namespace ShoppingApp.Application.Features.Products.Commands.Handlers
{
    public class DeleteProductCommandHandler : IRequestHandler<DeleteProductCommand, bool>
    {
        private readonly IUnitOfWork _unitOfWork;
        public DeleteProductCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<bool> Handle(DeleteProductCommand request, CancellationToken cancellationToken)
        {
            if(request.Id is null)
            {
                throw new BadRequestException("Product ID cannot be null.");
            }
            var product = await _unitOfWork.Products.GetByIdAsync(request.Id.Value);
            if (product is null)
            {
                throw new NotFoundException($"Product with ID {request.Id} not found.");
            }

            _unitOfWork.Products.Remove(product);
            await _unitOfWork.SaveChangesAsync(cancellationToken);
            return true;
        }
    }
}
