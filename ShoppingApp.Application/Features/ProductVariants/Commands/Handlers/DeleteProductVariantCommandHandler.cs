using AutoMapper;
using MediatR;
using ShoppingApp.Domain.Exceptions;

namespace ShoppingApp.Application.Features.ProductVariants.Commands.Handlers
{
    public class DeleteProductVariantCommandHandler : IRequestHandler<DeleteProductVariantCommand, bool>
    {
        private readonly IUnitOfWork _unitOfWork;

        public DeleteProductVariantCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<bool> Handle(DeleteProductVariantCommand request, CancellationToken cancellationToken)
        {
            if (request.Id is null)
            {
                throw new BadRequestException("ProductVariant Id cannot be null.");
            }

            var productVariant = await _unitOfWork.ProductVariants.GetByIdAsync(request.Id.Value);
            if (productVariant is null)
            {
                throw new NotFoundException($"ProductVariant with ID {request.Id} not found.");
            }
            _unitOfWork.ProductVariants.Remove(productVariant);
            await _unitOfWork.SaveChangesAsync(cancellationToken);
            return true;
        }
    }
}
