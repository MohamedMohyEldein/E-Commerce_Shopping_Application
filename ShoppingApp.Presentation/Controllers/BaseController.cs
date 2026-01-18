using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace ShoppingApp.Presentation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BaseController : ControllerBase
    {
        protected readonly IMediator _mediator;
        public BaseController(IMediator mediator)
        {
            _mediator = mediator;
        }
    }
}
