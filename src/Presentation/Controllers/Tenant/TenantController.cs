using Microsoft.AspNetCore.Mvc;

namespace SaaS.src.Presentation.Controllers.Tenant
{

    [ApiController]
    [Route("api/[controller]")]


    // ControllerBase -> Is a base class in ASP.NET Core that provides the fundamental functionality for handling HTTP requests


    public class TenantController : ControllerBase
    
    
    {

        private readonly ICreateTenantUseCase _createTenantUseCase;



    }
}
