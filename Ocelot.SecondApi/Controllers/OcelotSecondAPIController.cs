using Microsoft.AspNetCore.Mvc;

namespace Ocelot.SecondApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class OcelotSecondAPIController : ControllerBase
    {
        [HttpGet("Get")]

        public string Get()
        {
            return "Ocelot Second Api Work";
        }
    }
}