using CQRSWebApiProject.Business.Commands;
using CQRSWebApiProject.Business.DTO.Request;
using CQRSWebApiProject.Business.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace CQRSWebApiProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CQRSWebApiProjectController : ControllerBase
    {
        private readonly IMediator mediator;
        /// <summary>
        /// ctor
        /// </summary>
        /// <param name="mediator"></param>
        public CQRSWebApiProjectController(IMediator mediator)
        {
            this.mediator = mediator;
        }

        /// <summary>
        /// Create an Customer
        /// </summary>
        /// <param name="createCustomerRequest"></param>
        /// <returns>A newly created customer</returns>
        /// <response code="201">Returns the newly created item</response>
        /// <response code="400">If the item is null</response> 
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> CreateCustomer([FromBody] CreateCustomerRequest createCustomerRequest)
        {
            var result = await mediator.Send(new CreateCustomerCommand(createCustomerRequest));
            return Ok(result);
        }
    }
}