using CSharp.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace CSharp.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class OrdersController: ControllerBase
    {
        private readonly IOrdersService _ordersService;

        public OrdersController(IOrdersService orderService)
        {
            _ordersService = orderService;
        }

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
