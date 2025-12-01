// SaaS.src.Presentation.Controllers.Size/SizeController.cs
using MediatR;
using Microsoft.AspNetCore.Mvc;
using SaaS.src.Application.Interfaces.Repositories;
using SaaS.src.Application.UseCases.Size.Commands.Create;

namespace SaaS.src.Presentation.Controllers.Size
{
    [ApiController]
    [Route("api/[controller]")]
    public class SizeController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly ISizeRepository _sizeRepository;

        public SizeController(
            IMediator mediator,
            ISizeRepository sizeRepository)
        {
            _mediator = mediator;
            _sizeRepository = sizeRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllSizes()
        {
            try
            {
                var sizes = await _sizeRepository.GetAllSizesAsync();
                return Ok(sizes);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Error al obtener las tallas", error = ex.Message });
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetSizeById(int id)
        {
            try
            {
                var size = await _sizeRepository.GetSizeByIdAsync(id);

                if (size == null)
                {
                    return NotFound(new { message = "Talla no encontrada" });
                }

                return Ok(size);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Error al obtener la talla", error = ex.Message });
            }
        }

        [HttpPost]
        public async Task<IActionResult> CreateSize([FromBody] CreateSizeCommand command)
        {
            try
            {
                var sizeId = await _mediator.Send(command);

                return CreatedAtAction(
                    nameof(GetSizeById),
                    new { id = sizeId },
                    new { id = sizeId, sizeName = command.SizeName }
                );
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Error al crear la talla", error = ex.Message });
            }
        }
    }
}