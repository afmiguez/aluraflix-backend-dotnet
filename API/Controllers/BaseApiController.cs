using Application.Core;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BaseApiController : ControllerBase
    {
        private IMediator _mediator;
        protected IMediator Mediator => _mediator ??=HttpContext.RequestServices.GetService<IMediator>();

        protected ActionResult HandleResult<T>(Result<T> result,string path="")
        {
            if (result == null)
            {
                return NotFound();
            }
            if (result.IsSuccess && result.Value != null)
            {
                if (result.Status == Status.OK)
                {
                    return Ok(result.Value);    
                }

                if (result.Status == Status.CREATED)
                {
                    return Created($"{path}",result.Value);
                }
                
                if (result.Status == Status.NO_CONTENT)
                {
                    return NoContent();
                }
            }

            if (result.IsSuccess && result.Value == null || result.Status==Status.NOT_FOUND)
            {
                return NotFound();
            }

            

            return BadRequest(result.Error);
        }
    }
}