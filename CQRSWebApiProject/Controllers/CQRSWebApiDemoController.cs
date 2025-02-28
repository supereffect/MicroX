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
        #region fields

        private readonly IMediator _mediator;

        #endregion

        #region ctor

        /// <summary>
        /// ctor
        /// </summary>
        /// <param name="mediator"></param>
        public CQRSWebApiProjectController(IMediator mediator)
        {
            _mediator = mediator;
        }

        #endregion

        #region methods
        /// <response code="400">If the item is null</response> 
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> CreateCustomer([FromBody] CreateCustomerRequest createCustomerRequest)
        {
            var result = await _mediator.Send(new CreateCustomerCommand(createCustomerRequest));
            return Ok(result);
        }


        #endregion
    }
}