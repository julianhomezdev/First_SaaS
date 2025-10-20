using MediatR;
using Microsoft.AspNetCore.Mvc;
using SaaS.src.Application.UseCases.Product.Commands.Create;
using SaaS.src.Application.DTOs.Product;


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
            
            
            
            };


            // Send to MediatR
            var result = await _mediator.Send(command);

            if (!result.IsSuccess)
                return BadRequest(result);


            return Ok(result);
        }






    }
}
