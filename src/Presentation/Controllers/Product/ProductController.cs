using MediatR;
using Microsoft.AspNetCore.Mvc;
using SaaS.src.Application.DTOs.Product;
using SaaS.src.Application.UseCases.Invoice.Commands.Create;
using SaaS.src.Application.UseCases.Product.Commands.Create;
using SaaS.src.Application.UseCases.Product.Commands.Delete;
using SaaS.src.Application.UseCases.Product.Commands.Update;
using SaaS.src.Application.UseCases.Product.Queries;
using SaaS.src.Application.UseCases.Product.Queries.Size;

namespace SaaS.src.Presentation.Controllers.Product
{
    [ApiController]
    [Route("api/[controller]")]
    [DisableRequestSizeLimit]
    public class ProductController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ProductController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        [Consumes("multipart/form-data")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Create([FromForm] CreateProductRequest request)
        {
            var options = new System.Text.Json.JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };

            var sizes = string.IsNullOrEmpty(request.Sizes)
                ? new List<ProductSizeRequest>()
                : System.Text.Json.JsonSerializer.Deserialize<List<ProductSizeRequest>>(request.Sizes, options);

            var command = new CreateProductCommand
            {
                ProductName = request.ProductName,
                ProductReference = request.ProductReference,
                ProductPrice = request.ProductPrice,
                ProductTypeId = request.ProductTypeId,
                Sizes = sizes,
                ImageFile = request.ImageFile
            };

            var result = await _mediator.Send(command);

            if (!result.IsSuccess)
                return BadRequest(result);

            return Ok(result);
        }

        [HttpGet("sizes")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetAllSizes()
        {
            try
            {
                var query = new GetAllSizesQuery();
                var result = await _mediator.Send(query);

                if (!result.IsSuccess)
                    return BadRequest(result);

                return Ok(result.Data);
            }
            catch (Exception ex)
            {
                return BadRequest(new { error = ex.Message });
            }
        }

        [HttpDelete("delete/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var command = new DeleteProductCommand { ProductId = id };
                var result = await _mediator.Send(command);

                if (!result.IsSuccess)
                    return BadRequest(result);

                return Ok(new { message = "Product deleted successfully" });
            }
            catch (Exception ex)
            {
                return BadRequest(new { error = ex.Message });
            }
        }

        

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetAllProducts(
            [FromQuery] int? pageSize,
            [FromQuery] int? pageNumber,
            [FromQuery] List<int>? sizeIds)
        {
            try
            {
                var query = new GetAllProductsQuery
                {
                    PageSize = pageSize,
                    PageNumber = pageNumber,
                    SizeIds = sizeIds
                };

                var products = await _mediator.Send(query);
                return Ok(products);
            }
            catch (Exception ex)
            {
                return BadRequest(new { error = ex.Message });
            }
        }

        [HttpPost("invoices")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> CreateInvoice([FromBody] CreateInvoiceCommand command)
        {
            try
            {
                var result = await _mediator.Send(command);

                if (!result.IsSuccess)
                    return BadRequest(result);
                    
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(new { error = ex.Message });
            }
        }

        [HttpPut("update/{id}")]
        [Consumes("multipart/form-data")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Update(int id, [FromForm] UpdateProductRequest request)
        {
            try
            {
                var options = new System.Text.Json.JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                };

                var sizes = string.IsNullOrEmpty(request.Sizes)
                    ? new List<ProductSizeRequest>()
                    : System.Text.Json.JsonSerializer.Deserialize<List<ProductSizeRequest>>(request.Sizes, options);

                var command = new UpdateProductCommand
                {
                    ProductId = id,
                    ProductName = request.ProductName,
                    ProductReference = request.ProductReference,
                    ProductPrice = request.ProductPrice,
                    ProductTypeId = request.ProductTypeId,
                    Sizes = sizes,
                    ImageFile = request.ImageFile 
                };

                var result = await _mediator.Send(command);

                if (!result.IsSuccess)
                    return BadRequest(result);

                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(new { error = ex.Message });
            }
        }
    }
}