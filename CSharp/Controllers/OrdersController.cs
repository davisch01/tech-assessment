using CSharp.Models;
using CSharp.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;

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
        public List<OrderModel> GetByCustomerId(Guid customerId)
        {
            var orders =  _ordersService.GetOrdersByCustomerId(customerId);

            return orders;
        }

        [HttpPost]
        public OrderModel Post(OrderModel orderModel)
        {
            var createdOrder = _ordersService.CreateOrder(orderModel);

            return createdOrder;
        }

        [HttpPut]
        public OrderModel Update(OrderModel orderModel)
        {
            var updatedOrder = _ordersService.UpdateOrder(orderModel);

            return updatedOrder;
        }

        [HttpDelete]
        public OrderModel Delete(Guid orderId)
        {
            var canceledOrder = _ordersService.CancelOrder(orderId);

            return canceledOrder;
        }
    }
}
