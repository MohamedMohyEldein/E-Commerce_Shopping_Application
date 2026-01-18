using AutoMapper;
using MediatR;
using ShoppingApp.Application.Common.Services;
using ShoppingApp.Application.Features.Products.DTOs;
using ShoppingApp.Domain.Exceptions;

namespace ShoppingApp.Application.Features.Products.Commands.Handlers
{
    public class UpdateProductCommandHandler : IRequestHandler<UpdateProductCommand, ProductDto>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IFileService _fileService;

        public UpdateProductCommandHandler(IUnitOfWork unitOfWork, IMapper mapper, IFileService fileService)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _fileService = fileService;
        }

        public async Task<ProductDto> Handle(UpdateProductCommand request, CancellationToken cancellationToken)
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

            var category = await _unitOfWork.Categories.GetByIdAsync(request.UpdateProductDto!.CategoryId.Value);

            if(category is null)
            {
                throw new NotFoundException($"Category with ID {request.UpdateProductDto.CategoryId} not found.");
            }

            product.Name = request.UpdateProductDto.Name ?? product.Name;
            product.Description = request.UpdateProductDto.Description ?? product.Description;
            product.Price = request.UpdateProductDto.Price ?? product.Price;
            product.StockQuantity = request.UpdateProductDto.StockQuantity ?? product.StockQuantity;
            product.UpdatedAt = DateTime.UtcNow;
            product.ImageUrl = await _fileService.UploadAsync(request.UpdateProductDto!.Image!,"Images/Products");

            _unitOfWork.Products.Update(product);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return _mapper.Map<ProductDto>(product);
        }
    }
}
