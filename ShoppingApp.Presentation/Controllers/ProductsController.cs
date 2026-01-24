using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ShoppingApp.Application.Features.Products.Commands;
using ShoppingApp.Application.Features.Products.DTOs;
using ShoppingApp.Application.Features.Products.Queries;

namespace ShoppingApp.Presentation.Controllers
{
    public class ProductsController(IMediator mediator) : BaseController(mediator)
    {
        [HttpGet("{id:ulid}")]
        public async Task<ActionResult<ProductDto>> GetProductById(Ulid id)
        {
            var result = await _mediator.Send(new GetProductByIdQuery(id));
            return Ok(result);
        }
        [HttpGet]
        public async Task<ActionResult<List<ProductDto>>> GetAllProducts()
        {
            var result = await _mediator.Send(new GetAllProductsQuery());
            return Ok(result);
        }
        [HttpPost]
        [Authorize(Policy = "SellerOnly")]
        public async Task<ActionResult<ProductDto>> CreateProduct([FromForm] CreateProductDto createProductDto, IValidator<CreateProductDto> validator)
        {
            try
            {
                validator.ValidateAndThrow(createProductDto);
            }
            catch (ValidationException ex)
            {
                return BadRequest(ex.Errors);
            }
            var result = await _mediator.Send(new CreateProductCommand(createProductDto));
            return CreatedAtAction(nameof(GetProductById), new { id = result.Id }, result);
        }
        [HttpPut("{id:ulid}")]
        [Authorize(Policy = "SellerOnly")]
        public async Task<ActionResult<ProductDto>> UpdateProduct(Ulid id, [FromForm] UpdateProductDto updateProductDto, IValidator<UpdateProductDto> validator)
        {
            try
            {
                validator.ValidateAndThrow(updateProductDto);
            }
            catch (ValidationException ex)
            {
                return BadRequest(ex.Errors);
            }
            validator.ValidateAndThrow(updateProductDto);
            var result = await _mediator.Send(new UpdateProductCommand(id, updateProductDto));
            return Ok(result);
        }
        [HttpDelete("{id:ulid}")]
        [Authorize(Policy = "SellerOnly")]
        public async Task<IActionResult> DeleteProduct(Ulid id)
        {
            var result = await _mediator.Send(new DeleteProductCommand(id));
            if (!result)
            {
                return NotFound();
            }
            return NoContent();
        }
    }
}
