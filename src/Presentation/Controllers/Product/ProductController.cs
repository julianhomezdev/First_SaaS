using MediatR;
using Microsoft.AspNetCore.Mvc;
using SaaS.src.Application.UseCases.Product.Commands.Create;
using SaaS.src.Application.DTOs.Product;
using SaaS.src.Application.UseCases.Product.Queries;
using SaaS.src.Application.UseCases.Product.Queries.Size;


namespace SaaS.src.Presentation.Controllers.Product
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductController : ControllerBase
    {


        private readonly IMediator _mediator;

        public ProductController(IMediator mediator)
        {

            _mediator = mediator;


        }


        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]

        public async Task<IActionResult> Create([FromBody] CreateProductRequest request)
        {

            // Request -> command
            var command = new CreateProductCommand { 
            
                ProductName = request.ProductName,
                ProductReference = request.ProductReference,
                ProductPrice = request.ProductPrice,
                ProductTypeId = request.ProductTypeId,
                Sizes = request.Sizes
            
            
            
            };


            // Send to MediatR
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
            catch (Exception ex) {

                return BadRequest(new { error = ex.Message });



            }

        }


        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetAllProducts(

            [FromQuery] int? pageSize,
            [FromQuery] int? pageNumber,
            [FromQuery] List<int>? sizeIds

            )
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

                // Log the exception
                return BadRequest(new { error = ex.Message });


            }


        }
    }
}
