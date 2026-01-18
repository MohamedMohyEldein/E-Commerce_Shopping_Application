using AutoMapper;
using MediatR;
using ShoppingApp.Application.Common.Services;
using ShoppingApp.Application.Features.Categories.DTOs;
using ShoppingApp.Domain.Entities;
using ShoppingApp.Domain.Exceptions;

namespace ShoppingApp.Application.Features.Categories.Commands.Handlers
{
    public class CreateCategoryHandler : IRequestHandler<CreateCategoryCommand, CategoryDto>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IFileService _fileService;

        public CreateCategoryHandler(IUnitOfWork unitOfWork, IMapper mapper, IFileService fileService)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _fileService = fileService;
        }

        public async Task<CategoryDto> Handle(CreateCategoryCommand request, CancellationToken cancellationToken)
        {
            if(request.CreateCategoryDto is null)
            {
                throw new BadRequestException("Create category Dto is required.");
            }

            var category = _mapper.Map<Category>(request.CreateCategoryDto);

            category.Id = Ulid.NewUlid();
            
            category.ImageUrl = await _fileService.UploadAsync(request.CreateCategoryDto.Image, "Images/Categories");

            await _unitOfWork.Categories.AddAsync(category);
            await _unitOfWork.SaveChangesAsync(cancellationToken);
            
            return _mapper.Map<CategoryDto>(category);
        }
    }
}
