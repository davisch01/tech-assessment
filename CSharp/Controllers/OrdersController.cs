using Microsoft.AspNetCore.Mvc;

namespace CSharp.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class OrdersController : ControllerBase
    {
        [HttpGet]
        [Route("{customerId}")]
        public string Get()
        {
            return default;
        }

        [HttpPost]
        public string Post()
        {
            return default;
        }

        [HttpPut]
        public string Update()
        {
            return default;
        }

        [HttpDelete]
        public string Delete()
        {
            return default;
        }
    }
}
