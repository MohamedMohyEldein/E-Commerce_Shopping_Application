using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using ShoppingApp.Application.Features.Categories.Commands;
using ShoppingApp.Application.Features.Categories.DTOs;
using ShoppingApp.Application.Features.Categories.Queries;

namespace ShoppingApp.Presentation.Controllers
{
    public class CategoriesController(IMediator mediator) : BaseController(mediator)
    {
        [HttpGet("{id:ulid}")]
        public async Task<ActionResult<CategoryDto>> GetCategoryById(Ulid id, CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(new GetCategoryByIdQuery(id), cancellationToken);
            return Ok(result);
        }

        [HttpGet]
        public async Task<ActionResult<List<CategoryDto>>> GetAllCategories(CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(new GetAllCategoriesQuery(), cancellationToken);
            return Ok(result);
        }

        [HttpPost]
        public async Task<ActionResult<CategoryDto>> CreateCategory([FromForm] CreateCategoryDto createCategoryDto, IValidator<CreateCategoryDto> validator, CancellationToken cancellationToken)
        {
            try
            {
                validator.ValidateAndThrow(createCategoryDto);
            }
            catch (ValidationException ex)
            {
                return BadRequest(ex.Errors);
            }
            var result = await _mediator.Send(new CreateCategoryCommand(createCategoryDto));
            return CreatedAtAction(nameof(GetCategoryById), new { id = result.Id }, result);
        }

        [HttpPut("{id:ulid}")]
        public async Task<ActionResult<CategoryDto>> UpdateCategory(Ulid id, [FromForm] UpdateCategoryDto updateCategoryDto, IValidator<UpdateCategoryDto> validator, CancellationToken cancellationToken)
        {
            try
            {
                validator.ValidateAndThrow(updateCategoryDto);
            }
            catch (ValidationException ex)
            {
                return BadRequest(ex.Errors);
            }
            var result = await _mediator.Send(new UpdateCategoryCommand(id, updateCategoryDto), cancellationToken);
            return Ok(result);
        }

        [HttpDelete("{id:ulid}")]
        public async Task<IActionResult> DeleteCategoryAsync(Ulid id, CancellationToken cancellationToken)
        {
            await _mediator.Send(new DeleteCategoryCommand(id), cancellationToken);
            return NoContent();
        }
    }
}
