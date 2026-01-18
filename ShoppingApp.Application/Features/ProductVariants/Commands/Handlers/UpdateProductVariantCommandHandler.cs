using AutoMapper;
using MediatR;
using ShoppingApp.Application.Common.Services;
using ShoppingApp.Application.Features.ProductVariants.DTOs;
using ShoppingApp.Domain.Entities;
using ShoppingApp.Domain.Exceptions;

namespace ShoppingApp.Application.Features.ProductVariants.Commands.Handlers
{
    public class UpdateProductVariantCommandHandler : IRequestHandler<UpdateProductVariantCommand, ProductVariantDto>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IFileService _fileService;

        public UpdateProductVariantCommandHandler(IUnitOfWork unitOfWork, IMapper mapper, IFileService fileService)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _fileService = fileService;
        }

        public async Task<ProductVariantDto> Handle(UpdateProductVariantCommand request, CancellationToken cancellationToken)
        {
            if(request.updateProductVariant is null) 
            { 
                throw new BadRequestException("ProductVariant data must be provided.");
            }
           
            var product = await _unitOfWork.Products.GetByIdAsync(request.updateProductVariant.ProductId);
            if(product is null)
            {
                throw new NotFoundException($"Product with ID {request.updateProductVariant.ProductId} not found.");
            }

            var productVariant = await _unitOfWork.ProductVariants.GetByIdAsync(request.updateProductVariant.Id);

            if(productVariant is null)
            {
                throw new NotFoundException($"ProductVariant with ID {request.updateProductVariant.Id} not found.");
            }

            productVariant.Color = request.updateProductVariant.Color;
            productVariant.Size = request.updateProductVariant.Size;
            productVariant.StockQuantity = request.updateProductVariant.StockQuantity;
            productVariant.ImageUrl = await _fileService.UploadAsync(request.updateProductVariant!.Image!, "Images/ProductVariants");

            _unitOfWork.ProductVariants.Update(productVariant);
            await _unitOfWork.SaveChangesAsync(cancellationToken);
            return _mapper.Map<ProductVariantDto>(productVariant);
        }
    }
}
