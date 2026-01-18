using AutoMapper;
using MediatR;
using ShoppingApp.Domain.Exceptions;

namespace ShoppingApp.Application.Features.Categories.Commands.Handlers
{
    public class DeleteCategoryHandler : IRequestHandler<DeleteCategoryCommand, bool>
    {
        private readonly IUnitOfWork _unitOfWork;

        public DeleteCategoryHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<bool> Handle(DeleteCategoryCommand request, CancellationToken cancellationToken)
        {
            if (request.Id is null)
            {
                throw new BadRequestException("Category ID cannot be null.");
            }
            var category = await _unitOfWork.Categories.GetByIdAsync(request.Id.Value);

            if (category is null)
            {
                throw new NotFoundException($"Category with ID {request.Id} not found.");
            }
            _unitOfWork.Categories.Remove(category);
            await _unitOfWork.SaveChangesAsync(cancellationToken);
            return true;
        }
    }
}
