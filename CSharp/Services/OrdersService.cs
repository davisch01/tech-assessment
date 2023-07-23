using CSharp.Models;
using CSharp.Services.Interfaces;
using System;
using System.Collections.Generic;

namespace CSharp.Services
{
    public class OrdersService : IOrdersService
    {

        public OrderModel CancelOrder(Guid orderId)
        {
            throw new NotImplementedException();
        }

        public OrderModel CreateOrder(OrderModel order)
        {
            throw new NotImplementedException();
        }

        public List<OrderModel> GetOrdersByCustomerId(Guid customerId)
        {
            throw new NotImplementedException();
        }

        public OrderModel UpdateOrder(OrderModel order)
        {
            throw new NotImplementedException();
        }
    }
}
