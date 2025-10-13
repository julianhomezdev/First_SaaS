using Microsoft.AspNetCore.Mvc;
using SaaS.src.Application.DTOs.Requests.Tenant;
using SaaS.src.Application.Interfaces.TenantUseCases;
using System.Linq.Expressions;

namespace SaaS.src.Presentation.Controllers.Tenant
{

    [ApiController]
    [Route("api/[controller]")]


    // ControllerBase -> Is a base class in ASP.NET Core that provides the fundamental functionality for handling HTTP requests


    public class TenantController : ControllerBase


    {

        private readonly ICreateTenantUseCase _createTenantUseCase;
        private readonly ILogger<TenantController> _logger;


        public TenantController(ICreateTenantUseCase createTenantUseCase, ILogger<TenantController> logger)
        {

            _logger = logger;
            _createTenantUseCase = createTenantUseCase;


        }


        [HttpPost]
        public async Task<IActionResult> CreateTenant([FromBody] CreateTenantRequest request)
        {

            try
            {

                var response = await _createTenantUseCase.CreateAsync(request);


                _logger.LogInformation("Tenant created succesfully");


                return Ok(new
                {

                    message = "Tenant created succesfully",
                    tenant = response

                });

            }


            catch (Exception ex)
            {

                _logger.LogError(ex, "Error creating tenant");
                return BadRequest(new { message = ex.Message });



            }




        }

    }

}

