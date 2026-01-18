using AutoMapper;
using MediatR;
using ShoppingApp.Application.Common.Services;
using ShoppingApp.Application.Features.Categories.DTOs;
using ShoppingApp.Domain.Exceptions;

namespace ShoppingApp.Application.Features.Categories.Commands.Handlers
{
    public class UpdateCategoryHandler : IRequestHandler<UpdateCategoryCommand, CategoryDto?>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IFileService _fileService;

        public UpdateCategoryHandler(IUnitOfWork unitOfWork, IMapper mapper, IFileService fileService)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _fileService = fileService;
        }

        public async Task<CategoryDto?> Handle(UpdateCategoryCommand request, CancellationToken cancellationToken)
        {
            if(request.Id is null || request.UpdateCategoryDto is null)
            {
                throw new BadRequestException("Category Id or UpdateCategoryDto cannot be null");
            }
            var category = await _unitOfWork.Categories.GetByIdAsync(request.Id.Value);

            if(category is null)
            {
                throw new NotFoundException($"Category with Id {request.Id} not found");
            }

            category.Description = request.UpdateCategoryDto.Description;
            category.Name = request.UpdateCategoryDto.Name;
            category.ImageUrl = await _fileService.UploadAsync(request.UpdateCategoryDto!.Image!, "Images/Categories");

            _unitOfWork.Categories.Update(category);
            await _unitOfWork.SaveChangesAsync();
            return _mapper.Map<CategoryDto>(category);
        }
    }
}
