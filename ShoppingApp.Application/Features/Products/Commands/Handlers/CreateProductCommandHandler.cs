using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Diagnostics;
using ShoppingApp.Application.Common.Services;
using ShoppingApp.Application.Features.Products.DTOs;
using ShoppingApp.Domain.Entities;
using ShoppingApp.Domain.Exceptions;

namespace ShoppingApp.Application.Features.Products.Commands.Handlers
{
    public class CreateProductCommandHandler : IRequestHandler<CreateProductCommand, ProductDto>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IFileService _fileService;

        public CreateProductCommandHandler(IUnitOfWork unitOfWork, IMapper mapper, IFileService fileService)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _fileService = fileService;
        }

        public async Task<ProductDto> Handle(CreateProductCommand request, CancellationToken cancellationToken)
        {

            var category = await _unitOfWork.Categories.GetByIdAsync(request.CreateProductDto.CategoryId);

            if (category is null)
            {
                throw new NotFoundException($"Category with ID {request.CreateProductDto.CategoryId} does not exist.");
            }

            var product = _mapper.Map<Product>(request.CreateProductDto);

            product.Id = Ulid.NewUlid();
            product.ImageUrl = await _fileService.UploadAsync(request.CreateProductDto.Image, "Images/Products");
            product.CreatedAt = DateTime.UtcNow;
            product.UpdatedAt = DateTime.UtcNow;

            await _unitOfWork.Products.AddAsync(product);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return _mapper.Map<ProductDto>(product);
        }
    }
}
