using AutoMapper;
using MediatR;
using ShoppingApp.Application.Common.Services;
using ShoppingApp.Application.Features.ProductVariants.DTOs;
using ShoppingApp.Domain.Entities;
using ShoppingApp.Domain.Exceptions;

namespace ShoppingApp.Application.Features.ProductVariants.Commands.Handlers
{
    public class CreateProductVariantCommandHandler : IRequestHandler<CreateProductVariantCommand, ProductVariantDto>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IFileService _fileService;

        public CreateProductVariantCommandHandler(IUnitOfWork unitOfWork, IMapper mapper, IFileService fileService)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _fileService = fileService;
        }

        public async Task<ProductVariantDto> Handle(CreateProductVariantCommand request, CancellationToken cancellationToken)
        {
            if(request.CreateProductVariantDto is null)
            {
                throw new BadRequestException("CreateProductVariantDto cannot be null.");
            }

            var product = await _unitOfWork.Products.GetByIdAsync(request.CreateProductVariantDto.ProductId);
            if (product is null)
            {
                throw new NotFoundException($"Product with ID {request.CreateProductVariantDto.ProductId} not found.");
            }

            var productVariant = _mapper.Map<ProductVariant>(request.CreateProductVariantDto);
            productVariant.Id = Ulid.NewUlid();
            productVariant.ImageUrl = await _fileService.UploadAsync(request.CreateProductVariantDto!.Image!, "Images/ProductVariants");

            await _unitOfWork.ProductVariants.AddAsync(productVariant);
            await _unitOfWork.SaveChangesAsync(cancellationToken);
            return _mapper.Map<ProductVariantDto>(productVariant);
        }
    }
}
