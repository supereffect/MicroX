using Common.Messaging.Abstract;
using CQRSWebApiProject.Business.Commands;
using CQRSWebApiProject.Business.DTO.Request;
using CQRSWebApiProject.Business.DTO.Response;
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
        private readonly IMessageQueueProvider messageQueueProvider;
        /// <summary>
        /// ctor
        /// </summary>
        /// <param name="mediator"></param>
        public CQRSWebApiProjectController(IMediator mediator, IMessageQueueProvider messageQueueProvider)
        {
            this.mediator = mediator;
            this.messageQueueProvider = messageQueueProvider;
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
            await messageQueueProvider.PublishAsync("my-queue", "message");
            var result = await mediator.Send(new CreateCustomerCommand(createCustomerRequest));
            return Ok(result);
        }


        /// <summary>
        /// Get Customer Given By Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Returns the Given By Id Customer</returns>
        /// <response code="200">Returns the Given By Id Customer</response>
        /// <response code="404">If the item is null</response> 
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetCustomerById(int id)
        {
            var result = await mediator.Send(new GetCustomerByIDQuery(id));
            return Ok(result);
        }
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetAllCustomers()
        {
            var result = await mediator.Send(new GetAllCustomersQuery());
            return Ok(result);
        }


    }
}