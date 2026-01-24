using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ShoppingApp.Application.Features.ProductVariants.Commands;
using ShoppingApp.Application.Features.ProductVariants.DTOs;
using ShoppingApp.Application.Features.ProductVariants.Queries;

namespace ShoppingApp.Presentation.Controllers
{
    [Route("api/Products/{productId:ulid}/Variants")]
    public class ProductVariantsController(IMediator mediator) : BaseController(mediator)
    {
        [HttpGet("{variantId:ulid}")]
        public async Task<ActionResult<ProductVariantDto>> GetById(Ulid productId, Ulid variantId)
        {
            var result = await _mediator.Send(new GetProductVariantByIdQuery(variantId, productId));
            if (result is null)
            {
                return NotFound();
            }
            return Ok(result);
        }

        [HttpGet]
        public async Task<ActionResult<List<ProductVariantDto>>> GetAll(Ulid productId)
        {
            var result = await _mediator.Send(new GetAllProductVariantsQuery(productId));
            return Ok(result);
        }

        [HttpPost]
        [Authorize(Policy = "SellerOnly")]
        public async Task<ActionResult<ProductVariantDto>> Create(Ulid productId, [FromForm] CreateProductVariantDto createProductVariantDto, IValidator<CreateProductVariantDto> validator)
        {
            createProductVariantDto.ProductId = productId;
            try
            {
                validator.ValidateAndThrow(createProductVariantDto);
            }
            catch (ValidationException ex)
            {
                return BadRequest(ex.Errors);
            }
            var result = await _mediator.Send(new CreateProductVariantCommand(createProductVariantDto));
            return CreatedAtAction(nameof(GetById), new { productId = result.ProductId, variantId = result.Id }, result);
        }

        [HttpPut("{variantId:ulid}")]
        [Authorize(Policy = "SellerOnly")]
        public async Task<ActionResult<ProductVariantDto>> Update(Ulid productId, Ulid variantId, [FromForm] UpdateProductVariantDto updateProductVariantDto, IValidator<UpdateProductVariantDto> validator)
        {
            updateProductVariantDto.Id = variantId;
            updateProductVariantDto.ProductId = productId;
            try
            {
                validator.ValidateAndThrow(updateProductVariantDto);
            }
            catch (ValidationException ex)
            {
                return BadRequest(ex.Errors);
            }

            var result = await _mediator.Send(new UpdateProductVariantCommand(updateProductVariantDto));
            return Ok(result);
        }

        [HttpDelete("{variantId:ulid}")]
        [Authorize(Policy = "SellerOnly")]
        public async Task<IActionResult> Delete(Ulid productId, Ulid variantId)
        {
            var result = await _mediator.Send(new DeleteProductVariantCommand(variantId));
            if (!result)
            {
                return NotFound();
            }
            return NoContent();
        }
    }
}
